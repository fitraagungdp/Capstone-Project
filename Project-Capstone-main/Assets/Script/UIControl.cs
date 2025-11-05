using UnityEngine;
using TMPro;

public class UIControl : MonoBehaviour
{
    public MotorcycleControl motorcycleControl;
    public static UIControl Instance;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI limitText;
    public TextMeshProUGUI scoreText;


    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        // Update speed display
        float speed = Mathf.Abs(motorcycleControl.currentSpeed);
        speedText.text = "Speed: " + Mathf.Round(speed) * 5 + " km/h";

        // Example code to update the timer text based on the GameManager's remaining time
        float remainingTime = QuestManager.Instance.GetRemainingTime();
        UpdateTimerText(remainingTime);

        UpdateScoreText();
    }

    // Update the timer text
    private void UpdateTimerText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        // Format the time into a string in the desired format
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);

        timerText.text = timeString;
    }

    public void GetSpeedLimitText()
    {
        limitText.enabled = true;
    }
    public void RemoveSpeedLimitText()
    {
        limitText.enabled = false;
    }

    private void UpdateScoreText()
    {
        scoreText.text = GameManager.Instance.GetScore().ToString();
    }
}