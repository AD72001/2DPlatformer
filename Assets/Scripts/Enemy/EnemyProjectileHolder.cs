using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileHolder : MonoBehaviour
{
    [SerializeField] private Transform enemy;

    void Update()
    {
        transform.localScale = new Vector3(-enemy.localScale.x, 1, 1);
    }
}
