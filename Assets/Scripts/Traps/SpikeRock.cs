using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeRock : EnemyDamage
{
    // Trap Status
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkingDelay;

    // Trap Movement
    private float checkingTime;
    private Vector3 destination;
    private bool isAttacking;
    private Vector3[] directions = new Vector3[4];

    // Audio
    [SerializeField] private AudioClip rockSound;

    private void OnEnable() 
    {
        Stop();
    }

    private void Update()
    {
        if (isAttacking)
            transform.Translate(destination * Time.deltaTime * speed);
        else
        {
            checkingTime += Time.deltaTime;
            if (checkingTime >= checkingDelay)
            {
                CheckForPlayer();
            }
        }
    }

    private void CheckForPlayer()
    {
        CheckInAllDirections();

        for (int i = 0; i < directions.Length; i++)
        {
            RaycastHit2D hitPlayer = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hitPlayer.collider != null && !isAttacking)
            {
                isAttacking = true;
                destination = directions[i];
                checkingTime = 0;
            }
        }
    }

    private void CheckInAllDirections()
    {
        directions[0] = transform.right * range; // Check Right
        directions[1] = -transform.right * range; // Check Left
        directions[2] = transform.up * range; // Check Up
        directions[3] = -transform.up * range; // Check Down
    }

    private void Stop()
    {
        destination = transform.position;
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        SoundManager.instance.PlaySound(rockSound);
        base.OnTriggerEnter2D(collider);
        Stop();
    }
}
