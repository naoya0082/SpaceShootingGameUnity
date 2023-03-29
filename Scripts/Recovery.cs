using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recovery : MonoBehaviour
{
    public GameObject RecoveryEffect;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Player.instance.health < 5)
            {
                Player.instance.Recovery(4);
                SoundManager.instance.PlaySE(5);
                Instantiate(RecoveryEffect, collision.transform.position, collision.transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
