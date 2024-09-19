using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private GameObject item;
    private void OnDisable() {
        GameObject this_item = Instantiate(item, transform.position, Quaternion.identity);
        this_item.SetActive(true);
    }
}
