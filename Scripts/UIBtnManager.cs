using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBtnManager : MonoBehaviour
{
    string scene;
    [SerializeField] GameObject btnUI;
    [SerializeField] GameObject panel;

    private void Start()
    {
        scene = SceneManager.GetActiveScene().name;
    }
    public void OnClickRetryBtn()
    {
        SoundManager.instance.BGMControl("off");
        SceneManager.LoadScene(scene);
        SoundManager.instance.BGMControl("on");
    }

    public void OnClickBackBtn()
    {
        panel.SetActive(false);

    }

    public void OnClickPauseBtn()
    {
        panel.SetActive(true);
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
            btnUI.SetActive(true);
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            btnUI.SetActive(false);
        }
    }

    public void OnClickResumeBtn()
    {
        Time.timeScale = 1;
        btnUI.SetActive(false);
        panel.SetActive(false);
    }
}
