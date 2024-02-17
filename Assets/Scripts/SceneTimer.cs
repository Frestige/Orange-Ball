using UnityEngine;
using TMPro;

public class SceneTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float currentTime;

    void Update()
    {
        currentTime += Time.deltaTime;
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        int hours = Mathf.FloorToInt(currentTime / 3600);
        int minutes = Mathf.FloorToInt((currentTime % 3600) / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
}