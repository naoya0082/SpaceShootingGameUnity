using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjBreaker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile") || collision.CompareTag("Bonus") || collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }
}
