using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeArea : MonoBehaviour
{
    [SerializeField] private GameObject boss;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!boss.activeInHierarchy)
            UIManager.instance.Victory();
    }
}
