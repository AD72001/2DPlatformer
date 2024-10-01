using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIHPBar : MonoBehaviour
{
    // Stat on display
    [SerializeField] private Image totalHPBar;
    [SerializeField] private Image currentHPBar;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text projectileText;
    [SerializeField] private TMP_Text keyText;

    // Component
    [SerializeField] private GameObject player;

    void Start()
    {
        totalHPBar.fillAmount = player.GetComponent<HP>().currentHP / 10;
    }

    // Update is called once per frame
    void Update()
    {
        totalHPBar.fillAmount = player.GetComponent<HP>().startingHP / 10;
        currentHPBar.fillAmount = player.GetComponent<HP>().currentHP / 10;
        livesText.text = player.GetComponent<HP>().currentLives.ToString();
        projectileText.text = player.GetComponent<PlayerAttack>().projectileTotal.ToString();
        keyText.text = player.GetComponent<PlayerMovement>().keyTotal.ToString();
    }
}
