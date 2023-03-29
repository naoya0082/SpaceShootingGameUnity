using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileManager : MonoBehaviour
{
    Rigidbody2D rb2d;
    public GameObject ExpEffect;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.velocity += new Vector2(0, 15f) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") ||
            collision.CompareTag("Boss"))
        {
            SoundManager.instance.PlaySE(6);
            Instantiate(ExpEffect, collision.transform.position, collision.transform.rotation);
        }
        else if (collision.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
        }


    }
}
