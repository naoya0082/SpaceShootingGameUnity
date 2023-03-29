using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnemyTypeC_Manager : MonoBehaviour
{
    public int health = 1;
    public GameObject projectilePrefab;
    public GameObject destructionVFX;
    public GameObject hitEffect;
    float speed = 3f;
    float delay = 0f;
    float shotTimeMin = 5f;
    float shotTimeMax = 8f;

    private void Start()
    {
        

        string scene = SceneManager.GetActiveScene().name;
        switch (scene)
        {
            case "Stage_3":
                var path = new Vector3[]
                {
                    new Vector3(Random.Range(1, 4), 6, 0),
                    new Vector3(Random.Range(-4, 0), 0, 0),
                    new Vector3(Random.Range(1, 4), -6, 0),
                    new Vector3(Random.Range(-4, 0), -12, 0)
                };

                iTween.MoveTo(gameObject, iTween.Hash(
                    "path", path,
                    "speed", speed,
                    "delay", delay,
                    "easetype", iTween.EaseType.easeInOutSine,
                    "looptype", iTween.LoopType.loop
                ));

                shotTimeMin = 3f;
                shotTimeMax = 6f;
                break;
            case "Stage_4":
                shotTimeMin = 2f;
                shotTimeMax = 5f;
                break;
            case "Stage_5":
                path = new Vector3[]
                {
                    new Vector3(4, 7, 0),
                    new Vector3(0, 0, 0),
                    new Vector3(4, 0, 0),
                    new Vector3(10, -7, 0),

                };

                iTween.MoveTo(gameObject, iTween.Hash(
                    "path", path,
                    "speed", speed,
                    "delay", delay,
                    "easetype", iTween.EaseType.easeInOutSine,
                    "looptype", iTween.LoopType.loop
                ));
                shotTimeMin = 1f;
                shotTimeMax = 4f;
                break;
            default:
                shotTimeMin = 4f;
                shotTimeMax = 7f;
                break;
        }

        InvokeRepeating("Shoot", 3f, Random.Range(shotTimeMin, shotTimeMax));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            var projectile = projectilePrefab.GetComponent<Projectile>();
            Player.instance.GetDamage(projectile ? projectile.damage : 1);
        }
    }

    public void GetDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Instantiate(destructionVFX, transform.position, Quaternion.identity);
            SoundManager.instance.PlaySE(0);
            Destroy(gameObject);
        }
        else
        {
            SoundManager.instance.PlaySE(3);
            Instantiate(hitEffect, transform.position, Quaternion.identity, transform);
        }
    }

    private void Shoot()
    {     
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);   
    }
}