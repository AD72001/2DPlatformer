using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioClip enterSound;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SoundManager.instance.PlaySound(enterSound);
            SceneManager.LoadScene("Level_1");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit(); // Quits the application

            # if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode(); // Quit Playmode
            # endif
        }
    }
}
