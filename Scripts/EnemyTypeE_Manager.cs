using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeE_Manager : MonoBehaviour
{
    Rigidbody2D rb2d;
    [Tooltip("Health points in integer")]
    public int health;

    public FireballManager expBullet;

    [Tooltip("VFX prefab generating after destruction")]
    public GameObject destructionVFX;
    public GameObject hitEffect;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(0, -5);
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

    //method of destroying the 'Enemy'
    void Destruction()
    {
        Instantiate(destructionVFX, transform.position, Quaternion.identity);
        SoundManager.instance.PlaySE(2);
        ExpBullet(Random.Range(10,20));
        Destroy(gameObject);
    }

    void ExpBullet(int bulletCount)
    {
        for (int count = 1; count <= bulletCount; count++)
        {
            float bulletAngleX = -2 * Mathf.PI * count / bulletCount;
            float bulletAngleY = -2 * Mathf.PI * count / bulletCount;
            FireballManager bullet = Instantiate(expBullet, transform.position, Quaternion.identity);
            bullet.SetAngle(bulletAngleX, bulletAngleY);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ObjBreaker"))
        {
            Destroy(gameObject);
        }

        if (collision.tag == "Player")
        {
            Player.instance.GetDamage(5);
        }
    }
}
