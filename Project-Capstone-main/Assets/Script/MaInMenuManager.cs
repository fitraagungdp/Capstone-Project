using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MaInMenuManager : MonoBehaviour
{
    [Header("Main Menu Panel List")]
    public GameObject MainMenu;
    public GameObject OptionPanel;
    public GameObject AboutPanel;

// Start is called before the first frame update
    void Start()
    {
        MainMenu.SetActive(true);
        OptionPanel.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        Debug.Log(volume);
    }

    public void AboutBtn()
    {
        AboutPanel.SetActive(true);
        SoundManager.instance.UIClickSfx();
    }

    public void OptionBtn()
    {
        OptionPanel.SetActive(true);
        SoundManager.instance.UIClickSfx();
    }

    public void BackBtn()
    {
        if (OptionPanel.activeSelf)
        {
            OptionPanel.SetActive(false);
        }
        
        else if (AboutPanel.activeSelf)
        {
            AboutPanel.SetActive(false);
        }
        SoundManager.instance.UIClickSfx();
    }

    public void StartBtn()
    {
        SceneManager.LoadScene("Gameplay");
        SoundManager.instance.UIClickSfx();
    }

    public void TutorialBtn()
    {
        SceneManager.LoadScene("Prologue");
        SoundManager.instance.UIClickSfx();
    }

    public void ExitBtn()
    {
        Application.Quit();
    }

    public void SkipPrologueBtn()
    {
        SceneManager.LoadScene("Tutorial");
        SoundManager.instance.UIClickSfx();
    }
}
