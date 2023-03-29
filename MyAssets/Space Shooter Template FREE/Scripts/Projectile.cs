using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines the damage and defines whether the projectile belongs to the ‘Enemy’ or to the ‘Player’, whether the projectile is destroyed in the collision, or not and amount of damage.
/// </summary>

public class Projectile : MonoBehaviour {

    [Tooltip("Damage which a projectile deals to another object. Integer")]
    public int damage;

    [Tooltip("Whether the projectile belongs to the ‘Enemy’ or to the ‘Player’")]
    public bool enemyBullet;

    [Tooltip("Whether the projectile is destroyed in the collision, or not")]
    public bool destroyedByCollision;

    public GameObject hitEffect;

    private void OnTriggerEnter2D(Collider2D collision) //when a projectile collides with another object
    {
        string collisionTag = collision.tag;
        switch (collisionTag)
        {
            case "Player":
                if (enemyBullet)
                {
                    Player.instance.GetDamage(damage);
                    if (destroyedByCollision) Destruction();
                }
                break;
            case "Enemy":
                if (!enemyBullet &&
                    collision.GetComponent<EnemyTypeA_Manager>() &&
                    collision.transform.position.y < 10)
                {
                    collision.GetComponent<EnemyTypeA_Manager>().GetDamage(damage);
                    if (destroyedByCollision) Destruction();   
                } else if (!enemyBullet &&
                    collision.GetComponent<EnemyTypeB_Manager>() &&
                    collision.transform.position.y < 10)
                {
                    collision.GetComponent<EnemyTypeB_Manager>().GetDamage(damage);
                    if (destroyedByCollision) Destruction();
                } else if (!enemyBullet &&
                    collision.GetComponent<EnemyTypeC_Manager>() &&
                    collision.transform.position.y < 10)
                {
                    collision.GetComponent<EnemyTypeC_Manager>().GetDamage(damage);
                    if (destroyedByCollision) Destruction();
                } else if (!enemyBullet &&
                   collision.GetComponent<EnemyTypeD_Manager>() &&
                   collision.transform.position.y < 10)
                {
                    collision.GetComponent<EnemyTypeD_Manager>().GetDamage(damage);
                    if (destroyedByCollision) Destruction();
                } else if (!enemyBullet &&
                  collision.GetComponent<EnemyTypeE_Manager>() &&
                  collision.transform.position.y < 10)
                {
                    collision.GetComponent<EnemyTypeE_Manager>().GetDamage(damage);
                    if (destroyedByCollision) Destruction();
                }
                break;
            case "Boss":
                if (!enemyBullet &&
                    collision.GetComponent<BossTypeA_Manager>() &&
                    collision.transform.position.y < 10)
                {
                    collision.GetComponent<BossTypeA_Manager>().GetDamage(damage);
                    Instantiate(hitEffect, transform.position, transform.rotation);
                    if (destroyedByCollision) Destruction();
                }
                else if (!enemyBullet &&
                    collision.GetComponent<BossTypeB_Manager>() &&
                    collision.transform.position.y < 10)
                {
                    collision.GetComponent<BossTypeB_Manager>().GetDamage(damage);
                    Instantiate(hitEffect, transform.position, transform.rotation);
                    if (destroyedByCollision) Destruction();
                }
                else if (!enemyBullet &&
                    collision.GetComponent<BossTypeC_Manager>() &&
                    collision.transform.position.y < 10)
                {
                    collision.GetComponent<BossTypeC_Manager>().GetDamage(damage);
                    Instantiate(hitEffect, transform.position, transform.rotation);
                    if (destroyedByCollision) Destruction();
                }
                else if (!enemyBullet &&
                    collision.GetComponent<BossTypeD_Manager>() &&
                    collision.transform.position.y < 10)
                {
                    collision.GetComponent<BossTypeD_Manager>().GetDamage(damage);
                    Instantiate(hitEffect, transform.position, transform.rotation);
                    if (destroyedByCollision) Destruction();
                }
                else if (!enemyBullet &&
                    collision.GetComponent<BossTypeE_Manager>() &&
                    collision.transform.position.y < 10)
                {
                    collision.GetComponent<BossTypeE_Manager>().GetDamage(damage);
                    Instantiate(hitEffect, transform.position, transform.rotation);
                    if (destroyedByCollision) Destruction();
                }
                break;
            default:
                return;
        }
    }

    void Destruction() 
    {
        Destroy(gameObject);
    }
}


