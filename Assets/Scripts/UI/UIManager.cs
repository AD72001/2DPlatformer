using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance {get ; private set;}
    // Game Over
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    // Pause Game
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private AudioClip pauseSound;

    // Clear Stage
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private AudioClip victorySound;
    int buildIndex;

    void Awake()
    {
        instance = this;
        Time.timeScale = 1;
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        victoryScreen.SetActive(false);
    }

    void Update()
    {
        // Pause and Resume when player press ESCAPE
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseScreen.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
        }
    }

    #region Game Over Screen
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    public void Restart()
    {
        Time.timeScale = 1; // In case of restarting the game after pausing
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit(); // Quits the application

        # if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode(); // Quit Playmode
        # endif
    }
    #endregion

    #region Pause Screen
    private void PauseGame(bool status)
    {
        pauseScreen.SetActive(status);

        // Pause the game if status is true
        if (status)
            Time.timeScale = 0;
        else 
            Time.timeScale = 1;
    }

    public void EffectVolume()
    {
        SoundManager.instance.ChangeEffectVolume(0.2f);
    }

    public void BGMVolume()
    {
        SoundManager.instance.ChangeBGMVolume(0.2f);
    }

    #endregion

    #region Victory Screen
    public void Victory()
    {
        Time.timeScale = 0;
        victoryScreen.SetActive(true);
        SoundManager.instance.PlaySound(victorySound);
    }

    public void NextLevel(int level)
    {
        buildIndex = SceneUtility.GetBuildIndexByScenePath($"Level_{level}");
        if (buildIndex == -1)
            MainMenu();
        else
            SceneManager.LoadScene($"Level_{level}");
    }
    #endregion
}
