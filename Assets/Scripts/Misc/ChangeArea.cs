using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeArea : MonoBehaviour
{
    private GameObject boss;

    private void Update() {
        boss = GameObject.FindGameObjectWithTag("FinalBoss");
        Debug.Log(boss ==  null);
    }   

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (boss == null || !boss.activeInHierarchy)
            UIManager.instance.Victory();
    }
}
