using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [SerializeField] private GameObject item;
    private void OnDisable() {
        if(!this.gameObject.scene.isLoaded) return;
        
        GameObject this_item = Instantiate(item, transform.position + Vector3.up*1.2f, Quaternion.identity);
        this_item.SetActive(true);
    }
}
