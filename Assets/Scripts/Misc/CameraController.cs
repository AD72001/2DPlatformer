using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // camera that changes with areas.
    [SerializeField] private float speed;
    private float currentPosition_X;
    private Vector3 velocity = Vector3.zero;

    // Camera that follow the player character.
    [SerializeField] private Transform playerCharacter;

    void Update()
    {
        // Camera that changes with areas.
        // transform.position = Vector3.SmoothDamp(transform.position, new 
        //     Vector3(currentPosition_X, transform.position.y, transform.position.z), 
        //     ref velocity, speed);

        // Camera that follows the player character.
        transform.position = new Vector3(playerCharacter.position.x, playerCharacter.position.y, transform.position.z);
    }

    public void MoveToNextArea(Transform _nextArea)
    {
        currentPosition_X = _nextArea.position.x;
    }
}
