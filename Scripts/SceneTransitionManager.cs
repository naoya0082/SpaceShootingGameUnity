using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    string activeScene;
    private void Start()
    {
        activeScene = SceneManager.GetActiveScene().name;
    }

    public void LoadToScene(string sceneName)
    {
        if (Time.timeScale != 1) Time.timeScale = 1;
        
        SoundManager.instance.PlaySE(8);
        FadeInOutManager.instance.FadeOutToIn(() => Load(sceneName));   
    }

    private void Load(string sceneName)
    {   
        SoundManager.instance.PlayBGM(sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
