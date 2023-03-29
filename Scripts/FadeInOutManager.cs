using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class FadeInOutManager : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public static FadeInOutManager instance;

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

    public void FadeOutToIn(TweenCallback callback)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, 2f)
            .OnComplete(() => {
                callback();
                FadeIn();
            });
    }

    private void FadeIn()
    {
        canvasGroup.DOFade(0, 1f)
            .OnComplete(() => canvasGroup.blocksRaycasts = false);
    }
}
