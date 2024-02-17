using UnityEngine;

public class StartButton : MonoBehaviour
{
    private bool gameStarted = false;

    void Update()
    {
        if (!gameStarted)
        {
            // If the game has not started, stop time
            Time.timeScale = 0f;
        }
    }

    public void StartGame()
    {
        // Start time
        Time.timeScale = 1f;
        gameStarted = true;
    }
}
