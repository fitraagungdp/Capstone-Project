using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("Panels")]
    public GameObject PausePanel;
    public GameObject PlayerPanel;
    public GameObject TutorialDonePanel;
    public GameObject TutorialOverPanel;
    public GameObject GuidePanel;
    public bool isBookRotating = false;
    public TextMeshProUGUI finalScoreLose;
    public TextMeshProUGUI finalScoreMid;
    public TextMeshProUGUI finalScoreWin;
    public int playerScore = 0;

    public GameObject GameOverPanelLose;
    public GameObject GameOverPanelMid;
    public GameObject GameOverPanelWin;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        PlayerPanel.SetActive(true);
        PausePanel.SetActive(false);

        float volume = PlayerPrefs.GetFloat("Volume", 1.0f);
        AudioListener.volume = volume;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel(); // Panggil metode ini saat tombol "Esc" ditekan.
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            OpenGuide();
        }
    }

    public void ResumeGame()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        PlayerPanel.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SoundManager.instance.UIClickSfx();
    }

    public void pausePanel()
    {
        bool isPaused = !PausePanel.activeSelf;

        if (isPaused)
        {
            // Game is paused
            PlayerPanel.SetActive(false);
        }
        else
        {
            // Game is resumed
            PlayerPanel.SetActive(true);
        }

        PausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
        Cursor.visible = isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        SoundManager.instance.UIClickSfx();
    }


    public bool IsBookRotating
    {
        get { return isBookRotating; }
        set { isBookRotating = value; }
    }

    public void OpenGuide()
    {
        PlayerPanel.SetActive(true);

        bool isPaused = !PausePanel.activeSelf;

        GuidePanel.SetActive(true);

        Time.timeScale = isPaused ? 0 : 1;

        Cursor.visible = isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void CloseBtn()
    {
        if (isBookRotating)
        {
            return;
        }

        GuidePanel.SetActive(false);
        Time.timeScale = 1;
        PlayerPanel.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        SoundManager.instance.UIClickSfx();
    }

    public void MainMenuBtn()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;

        SoundManager.instance.UIClickSfx();
    }

    public void Exit()
    {
        Application.Quit();

    }

    public void GameOver()
    {
        // Show the Game Over Panel and pause the game
        //GameOverPanel.SetActive(true);

        int currentScore = GetScore();

        if (currentScore <= 10000)
        {
            GameOverPanelLose.SetActive(true);
            GameOverPanelMid.SetActive(false);
            GameOverPanelWin.SetActive(false);
            finalScoreLose.text = "Jumlah Uang yang didapatkan: " + GetScore();
        }
        else if (currentScore > 10000 && currentScore < 50000)
        {
            GameOverPanelLose.SetActive(false);
            GameOverPanelMid.SetActive(true);
            GameOverPanelWin.SetActive(false);
            finalScoreMid.text = "Jumlah Uang yang didapatkan: " + GetScore();
        }
        else if (currentScore >= 50000)
        {
            GameOverPanelLose.SetActive(false);
            GameOverPanelMid.SetActive(false);
            GameOverPanelWin.SetActive(true);
            finalScoreWin.text = "Jumlah Uang yang didapatkan: " + GetScore();
        }

        Time.timeScale = 0;

        // Make the cursor visible and unlock it
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SoundManager.instance.GameOverSfx();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;

        SoundManager.instance.UIClickSfx();
    }

    public int GetScore()
    {
        return playerScore;
    }

    public void AddScore(int amount)
    {
        playerScore += amount;
    }

    public void SubtractScore(int amount)
    {
        playerScore -= amount;
    }

    public void ResetScore()
    {
        playerScore = 0;
    }

    public void TutorialDone()
    {
        // Show the Tutorial Over Panel and pause the game
        TutorialDonePanel.SetActive(true);
        Time.timeScale = 0;

        // Make the cursor visible and unlock it
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void TutorialOver()
    {
        TutorialOverPanel.SetActive(true);
        Time.timeScale = 0;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
