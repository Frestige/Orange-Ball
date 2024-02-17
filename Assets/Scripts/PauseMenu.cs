using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject PauseButton;

    public void Pause()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
        PausePanel.SetActive(true);
        Time.timeScale = 0;
        PauseButton.SetActive(false);
    }

    public void Continue()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        PauseButton.SetActive(true);
    }

    public void Quit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
