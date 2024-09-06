using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource effectSource;
    private AudioSource BGM;

    private void Awake()
    {
        instance = this;
        effectSource = GetComponent<AudioSource>();
        BGM = transform.GetChild(0).GetComponent<AudioSource>();

        // Keep instance from being destroyed
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // Avoid duplicate sound instance
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        //ChangeEffectVolume(0);
        //ChangeBGMVolume(0);
    }

    public void PlaySound(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }

    public void ChangeEffectVolume(float _value)
    {
        ChangeSoundVolume(1, "effectVolume", effectSource, _value);
    }

    public void ChangeBGMVolume(float _value)
    {
        ChangeSoundVolume(0.2f, "BGMVolume", BGM, _value);
    }

    public void ChangeSoundVolume(float maxVolume, string volumeName, AudioSource src, float _value)
    {
        float currentVolume = PlayerPrefs.GetFloat(volumeName);

        currentVolume += _value;

        if (currentVolume > 1)
            currentVolume = 0;
        else if (currentVolume < 0)
            currentVolume = 1;

        src.volume = currentVolume * maxVolume;

        PlayerPrefs.SetFloat(volumeName, currentVolume);
    }
}
