using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnemyTypeA_Manager : MonoBehaviour
{
    #region FIELDS
    [Tooltip("Health points in integer")]
    public int health;

    [Tooltip("Enemy's projectile prefab")]
    public GameObject Projectile;

    [Tooltip("VFX prefab generating after destruction")]
    public GameObject destructionVFX;
    public GameObject hitEffect;
    float shootingInterval;
    string scene;

    [HideInInspector] public int shotChance; //probability of 'Enemy's' shooting during tha path
    [HideInInspector] public float shotTimeMin, shotTimeMax; //max and min time for shooting from the beginning of the path
    Rigidbody2D rb2d;
    #endregion

    private void Start()
    {
        scene = SceneManager.GetActiveScene().name;
        switch (scene)
        {
            case "Stage_1":
                shootingInterval = Random.Range(5, 8);
                rb2d = GetComponent<Rigidbody2D>();
                rb2d.velocity = new Vector2(0, -1);
                break;
            case "Stage_2":
                shootingInterval = Random.Range(4, 7);
                rb2d = GetComponent<Rigidbody2D>();
                rb2d.velocity = new Vector2(0, -1);

                float speed = 3f;
                float delay = 0f;
                shootingInterval = Random.Range(2, 5);
                var path = new Vector3[]
                {
                    new Vector3(-2, -3, 0),
                    new Vector3(0, 6, 0),
                    new Vector3(2, 0, 0),
                    new Vector3(3, 4, 0),
                    new Vector3(10, 8, 0)
                };

                iTween.MoveTo(gameObject, iTween.Hash(
                    "path", path,
                    "speed", speed,
                    "delay", delay,
                    "easetype", iTween.EaseType.easeInOutSine,
                    "looptype", iTween.LoopType.none
                ));
                break;
            case "Stage_3":
                shootingInterval = Random.Range(3, 6);
                rb2d = GetComponent<Rigidbody2D>();
                rb2d.velocity = new Vector2(0, -1);
                break;
            case "Stage_4":
                speed = 5f;
                delay = 0f;
                shootingInterval = Random.Range(2, 5);

                path = new Vector3[]
                {
                    new Vector3(-4, -1, 0),
                    new Vector3(-2, 0, 0),
                    new Vector3(0, 2, 0),
                    new Vector3(3, 4, 0),
                    new Vector3(10, 8, 0)
                };

                iTween.MoveTo(gameObject, iTween.Hash(
                    "path", path,
                    "speed", speed,
                    "delay", delay,
                    "easetype", iTween.EaseType.easeInOutSine,
                    "looptype", iTween.LoopType.none
                ));
                break;
            case "Stage_5":
                speed = 7f;
                delay = 0f;
                shootingInterval = Random.Range(2, 5);

                path = new Vector3[]
                {
                    new Vector3(-4, 5, 0),
                    new Vector3(-2, 0, 0),
                    new Vector3(0, 2, 0),
                    new Vector3(3, 4, 0),
                    new Vector3(0, -7, 0),

                    new Vector3(10, -5, 0)
                };

                iTween.MoveTo(gameObject, iTween.Hash(
                    "path", path,
                    "speed", speed,
                    "delay", delay,
                    "easetype", iTween.EaseType.easeInOutSine,
                    "looptype", iTween.LoopType.none
                ));
               
                break;
            default:
                shootingInterval = Random.Range(5, 8);
                break;
        }
        
        InvokeRepeating("ActivateShooting", Random.Range(3, 8), shootingInterval);
    }

    //coroutine making a shot
    void ActivateShooting()
    {
            Instantiate(Projectile, gameObject.transform.position, Quaternion.identity);
    }

    //method of getting damage for the 'Enemy'
    public void GetDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Destruction();
        else
        {
            SoundManager.instance.PlaySE(3);
            Instantiate(hitEffect, transform.position, Quaternion.identity, transform);
        }
    }

    //if 'Enemy' collides 'Player', 'Player' gets the damage equal to projectile's damage value
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Projectile.GetComponent<Projectile>() != null)
                Player.instance.GetDamage(Projectile.GetComponent<Projectile>().damage);
            else
                Player.instance.GetDamage(1);
        }
    }

    //method of destroying the 'Enemy'
    void Destruction()
    {
        Instantiate(destructionVFX, transform.position, Quaternion.identity);
        SoundManager.instance.PlaySE(0);
        Destroy(gameObject);
    }
}

