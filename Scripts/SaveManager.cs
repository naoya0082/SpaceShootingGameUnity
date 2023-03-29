using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    [HideInInspector] public string userNextStageData;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        userNextStageData = PlayerPrefs.GetString("NEXT_STAGE", "1");
        //PlayerPrefs.SetString("NEXT_STAGE", "1");
        //PlayerPrefs.Save();

        //Debug.Log(userNextStageData);
    }

    public void Save(string clearStage)
    {
        //次のステージ情報を更新
        int nextStage = int.Parse(clearStage) + 1;
        PlayerPrefs.SetString("NEXT_STAGE", nextStage.ToString());
        PlayerPrefs.Save();
    }
}
