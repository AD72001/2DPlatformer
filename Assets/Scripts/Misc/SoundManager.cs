using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource source;
    private AudioSource BGM;

    private void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
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
    }

    public void PlaySound(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    public void ChangeVolume(float _value)
    {
        float currentVolume = PlayerPrefs.GetFloat("soundVolume");

        currentVolume += _value;

        if (currentVolume > 1)
            currentVolume = 0;
        else if (currentVolume < 0)
            currentVolume = 1;

        source.volume = currentVolume;
        BGM.volume = currentVolume;

        PlayerPrefs.SetFloat("soundVolume", currentVolume);
    }
}
