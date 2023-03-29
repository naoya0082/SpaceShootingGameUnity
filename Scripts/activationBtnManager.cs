using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class activationBtnManager : MonoBehaviour
{
    [SerializeField] Button btn;
    [SerializeField] Text text;
    [SerializeField] int stageNum;
    [SerializeField] bool isActivation;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
        if(stageNum <= int.Parse(SaveManager.instance.userNextStageData))
        {
            isActivation = true;
            text.color = Color.white;
        }
        switch (isActivation)
        {
            case true:
                btn.interactable = true;
                break;
            case false:
                btn.interactable = false;
                break;
        }
    }
}
