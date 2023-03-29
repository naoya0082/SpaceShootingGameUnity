using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// This script defines which sprite the 'Player" uses and its health.
/// </summary>

public class Player : MonoBehaviour
{
    public GameObject destructionFX;
    public GameObject hitEffect;
    public int health;
    public GameObject life_1, life_2, life_3, life_4, life_5;
    public static Player instance;

    private void Awake()
    {
        if (instance == null) 
            instance = this;
    }

    //method for damage proceccing by 'Player'
    public void GetDamage(int damage)   
    {
        SoundManager.instance.PlaySE(10);
        Instantiate(hitEffect, transform.position, Quaternion.identity);
        transform.DOShakePosition(0.1f, 0.5f, 10);
        health -= damage;

        if (health < 5) life_5.SetActive(false);
        if (health < 4) life_4.SetActive(false);
        if (health < 3) life_3.SetActive(false);
        if (health < 2) life_2.SetActive(false);
        if (health < 1) life_1.SetActive(false);

        if (health <= 0)
        {
            Destruction();
        }
    }

    public void Recovery(int addLife)
    {
        health += addLife;
        if (health > 5) health = 5;
        if (health >= 5) life_5.SetActive(true);
        if (health >= 4) life_4.SetActive(true);
        if (health >= 3) life_3.SetActive(true);
        if (health >= 2) life_2.SetActive(true);
        if (health >= 1) life_1.SetActive(true);
    }

    //'Player's' destruction procedure
    void Destruction()
    {
        Instantiate(destructionFX, transform.position, Quaternion.identity); //generating destruction visual effect and destroying the 'Player' object
        SoundManager.instance.PlaySE(9);
        Destroy(gameObject);
    }
}
















