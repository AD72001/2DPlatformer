using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeText : MonoBehaviour
{
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
        float volume = PlayerPrefs.GetFloat("soundVolume")*100;
        txt.text = "SOUND:" + volume.ToString();
    }
}
