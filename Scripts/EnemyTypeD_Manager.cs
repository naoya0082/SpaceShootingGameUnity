using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeD_Manager : MonoBehaviour
{
    public int health = 1;
    public GameObject projectilePrefab;
    public GameObject destructionVFX;
    public GameObject hitEffect;
   
    float speed = 3f;
    float delay = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        var path = new Vector3[]
       {
            new Vector3(Random.Range(-4,4), Random.Range(0,6), 0),
            new Vector3(Random.Range(-4,4), Random.Range(0,6), 0),
            new Vector3(Random.Range(-4,4), Random.Range(0,6), 0),
            new Vector3(Random.Range(-4,4), Random.Range(0,6), 0),
            new Vector3(Random.Range(-4,4), Random.Range(0,6), 0),
            new Vector3(Random.Range(-4,4), Random.Range(0,6), 0),
            new Vector3(Random.Range(-4,4), Random.Range(0,6), 0),
            new Vector3(Random.Range(-4,4), Random.Range(0,6), 0),
            new Vector3(Random.Range(-4,4), Random.Range(0,6), 0),
            new Vector3(Random.Range(-4,4), Random.Range(0,6), 0),
            new Vector3(Random.Range(-4,4), Random.Range(0,6), 0),
            new Vector3(Random.Range(-4,4), Random.Range(0,6), 0),
            new Vector3(Random.Range(-4,4), Random.Range(0,6), 0),
            new Vector3(Random.Range(-4,4), Random.Range(0,6), 0),
            new Vector3(Random.Range(-4,4), Random.Range(0,6), 0),
            new Vector3(Random.Range(-4,4), Random.Range(0,6), 0),
            new Vector3(Random.Range(-4,4), Random.Range(0,6), 0),
            new Vector3(Random.Range(-4,4), Random.Range(0,6), 0),
            new Vector3(Random.Range(-4,4), Random.Range(0,6), 0),
            new Vector3(Random.Range(-4,4), Random.Range(0,6), 0),
            new Vector3(0, 7, 0),
            new Vector3(7, 7, 0)
       };

        iTween.MoveTo(gameObject, iTween.Hash(
            "path", path,
            "speed", speed,
            "delay", delay,
            "easetype", iTween.EaseType.easeInOutSine,
            "looptype", iTween.LoopType.loop
        ));
        StartCoroutine(Shoot());

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
            SoundManager.instance.PlaySE(2);
            Destroy(gameObject);
        }
        else
        {
            SoundManager.instance.PlaySE(3);
            Instantiate(hitEffect, transform.position, Quaternion.identity, transform);
        }
    }

    private IEnumerator Shoot()
    {
        int ShootCount; 
        while (true)
        {
            ShootCount = Random.Range(3,8);
            for (int j = 0; j < ShootCount; j++)
            {
                Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(5f);

            ShootCount = 10;
            for (int j = 0; j < ShootCount; j++)
            {
                Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.05f);
            }

            yield return new WaitForSeconds(5f);

            ShootCount = Random.Range(10, 15);
            for (int j = 0; j < ShootCount; j++)
            {
                Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(5f);

        }
    }
}
