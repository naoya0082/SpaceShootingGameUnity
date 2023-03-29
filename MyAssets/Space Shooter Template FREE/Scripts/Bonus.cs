using UnityEngine;

public class Bonus : MonoBehaviour {
    public GameObject levelUPEffect;

    //when colliding with another object, if another objct is 'Player', sending command to the 'Player'
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.tag == "Player") 
        {
            if (PlayerShooting.instance.weaponPower <= PlayerShooting.instance.maxweaponPower)
            {
                PlayerShooting.instance.weaponPower++;
                SoundManager.instance.PlaySE(4);
                Instantiate(levelUPEffect, collision.transform.position, collision.transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
