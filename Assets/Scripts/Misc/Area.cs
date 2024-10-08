using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    private Vector3[] enemyInitialPosition;

    private void Awake()
    {
        enemyInitialPosition = new Vector3[enemies.Length];

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
                enemyInitialPosition[i] = enemies[i].transform.position;
        }
    }

    public void ActivateArea(bool _status)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].SetActive(_status);
                enemies[i].transform.position = enemyInitialPosition[i];
                if (enemies[i].GetComponent<HP>() != null)
                    enemies[i].GetComponent<HP>().AddHP(enemies[i].GetComponent<HP>().startingHP);
            }
        }
    }
}
