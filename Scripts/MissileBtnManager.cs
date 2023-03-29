using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MissileBtnManager : MonoBehaviour
{
    GameObject missileGenerator;
    public Button btn;
 
    private void Start()
    {
        missileGenerator = GameObject.Find("MissileGenerator");
    }
    public void OnClickMissileBtn()
    {
        StartCoroutine(missileGenerator.GetComponent<MissileGenerator>().CreateMissile());
        btn.interactable = false;

        StartCoroutine(SetMissileInterval());
    }

    IEnumerator SetMissileInterval()
    {
        yield return new WaitForSeconds(30f);
        btn.interactable = true;
    }
}
