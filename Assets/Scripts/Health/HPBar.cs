using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private HP playerHP;
    [SerializeField] private Image totalHPBar;
    [SerializeField] private Image currentHPBar;

    void Start()
    {
        totalHPBar.fillAmount = playerHP.currentHP / 10;
    }

    // Update is called once per frame
    void Update()
    {
        currentHPBar.fillAmount = playerHP.currentHP / 10;
    }
}
