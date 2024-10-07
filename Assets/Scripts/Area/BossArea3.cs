using System.Collections;
using UnityEngine;

public class BossArea3 : MonoBehaviour
{
    // Boss Component
    [SerializeField] private BOD_Boss boss;

    // Spells 
    [SerializeField] private Boss_Spell[] spells;
    [SerializeField] private Transform[] points;
    [SerializeField] private float spellCD;
    public float spellTimer;
    private bool switchPosition;

    private void Awake() 
    {
        boss.gameObject.SetActive(false);
        spellTimer = 0.0f;
    }

    private void Update() {
        if (boss.phase == 2)
        {
            if(spellTimer > spellCD)
            {
                spellTimer = 0.0f;
                // Activate lighting at points
                for (int i=0; i < spells.Length; i++)
                {
                    if (switchPosition)
                    {
                        if (i%2 == 0)
                        {
                            spells[i].gameObject.SetActive(true);
                        }
                    }
                    else {
                        if (i%2 == 1)
                        {
                            spells[i].gameObject.SetActive(true);
                        }
                    }
                }

                SwitchSpellPosition();
            }
            else 
            {
                spellTimer += Time.deltaTime;
            }
        }
    }

    private void SwitchSpellPosition()
    {
        switchPosition = !switchPosition;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            boss.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Player") && boss.phase == 1)
        {
            boss.isChasing = true;
        }
    }
}
