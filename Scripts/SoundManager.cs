using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSourceBGM; // BGMスピーカー
    public AudioClip[] audioClipsBGM;  // BGM音源
    public AudioSource audioSourceSE;  // SEスピーカー
    public AudioClip[] audioClipsSE;   // SE音源
    //シーン間でのデータ共有
    public static SoundManager instance;

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

    public void PlayBGM(string sceneName)
    {
        BGMControl("off");
        switch(sceneName)
        {
            case "Title":
                audioSourceBGM.clip = audioClipsBGM[1];
                break;
            case "Select":
                audioSourceBGM.clip = audioClipsBGM[2];
                break;
            case "Stage_1":
                audioSourceBGM.clip = audioClipsBGM[0];
                break;
            case "Stage_2":
                audioSourceBGM.clip = audioClipsBGM[0];
                break;
            case "Stage_3":
                audioSourceBGM.clip = audioClipsBGM[0];
                break;
            case "Stage_4":
                audioSourceBGM.clip = audioClipsBGM[0];
                break;
            case "Stage_5":
                audioSourceBGM.clip = audioClipsBGM[0];
                break;
            default:
                break;
        }
        BGMControl("on");
    }

    public void BGMControl(string switchBGM)
    {
        if (switchBGM == "on")      audioSourceBGM.Play();
        else if(switchBGM == "off") audioSourceBGM.Stop();
    }

    public void PlaySE(int index)
    {
        audioSourceSE.PlayOneShot(audioClipsSE[index]);
    }

    public void SEControl(string switchSE)
    {
        if (switchSE == "on")       audioSourceSE.Play();
        else if (switchSE == "off") audioSourceSE.Stop();
    }
}
