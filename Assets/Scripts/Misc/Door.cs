using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousArea;
    [SerializeField] private Transform nextArea;
    [SerializeField] private CameraController cameraController;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            if (collider.transform.position.x < transform.position.x)
            {
                cameraController.MoveToNextArea(nextArea);
                nextArea.GetComponent<Area>().ActivateArea(true);
                previousArea.GetComponent<Area>().ActivateArea(false);
            }
            else
            {
                cameraController.MoveToNextArea(previousArea);
                nextArea.GetComponent<Area>().ActivateArea(false);
                previousArea.GetComponent<Area>().ActivateArea(true);
            }
        }
    }
}

