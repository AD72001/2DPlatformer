using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeText : MonoBehaviour
{
    [SerializeField] private string audioText; // Audio:
    [SerializeField] private string volumeName; // 0 - 100
    private TMP_Text txt;

    void Awake()
    {
        txt = GetComponent<TMP_Text>();
    }

    void Update()
    {
        UpdateVolumeText();
    }

    void UpdateVolumeText()
    {
        float volume = PlayerPrefs.GetFloat(volumeName)*100;
        txt.text = audioText + ":" + volume.ToString();
    }
}
