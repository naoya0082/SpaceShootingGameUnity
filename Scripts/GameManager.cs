using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject ClearImage;
    [SerializeField] SceneTransitionManager sceneTransitionManager;
    [SerializeField] GameObject warning;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject missileBtn;

    // Update is called once per frame
    void Update()
    {
        if (!player)
        {
            GameOver();
        }   
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        panel.SetActive(true);
        missileBtn.SetActive(false);
    }

    public void ShowWarning()
    {
        StartCoroutine(LoopShowWarning());
    }

    public void Clear(string clearStageNum)
    {
        if(int.Parse(clearStageNum) >= int.Parse(SaveManager.instance.userNextStageData))
        {
            SaveManager.instance.Save(clearStageNum);
            //次のステージ情報を更新
            int nextStage = int.Parse(clearStageNum) + 1;
            SaveManager.instance.userNextStageData = nextStage.ToString();
        } 

        ClearImage.SetActive(true);
        SoundManager.instance.PlaySE(7);
        StartCoroutine(SendScene("Select"));
    }

    private IEnumerator LoopShowWarning()
    {
        for (int i = 0; i < 3; i++)
        {
            warning.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            warning.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator SendScene(string scene)
    {
        yield return new WaitForSeconds(5f);
        sceneTransitionManager.LoadToScene(scene);
    }
}
