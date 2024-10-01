using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BossArea2 : MonoBehaviour
{
    private bool _active = false;

    //Spawn Items and Positions
    [SerializeField] private GameObject[] itemList;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private float spawnCD;
    private float spawnTimer = 0;
    private int maxItem = 3;
    private List<GameObject> activeItems = new List<GameObject>();
    private GameObject item = null;

    // Invisible Door
    [SerializeField] private GameObject door;

    private void Awake() {
        door.GetComponent<Collider2D>().enabled = false;
    }

    private void Update() 
    {
        spawnTimer += Time.deltaTime;

        if (activeItems.Count >= maxItem && (spawnTimer >= spawnCD))
        {
            activeItems.Clear();
        }

        if (_active && (spawnTimer >= spawnCD) && activeItems.Count < maxItem)
        {
            spawnTimer = 0;
            // Spawn Item at points
            foreach (Transform _spawnPosition in spawnPositions)
            {
                item = Instantiate(itemList[Random.Range(0, itemList.Length)], _spawnPosition.position, Quaternion.identity);
                item.SetActive(true);
                activeItems.Add(item);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            _active = true;
            door.GetComponent<Collider2D>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            _active = false;
        }
    }
}
