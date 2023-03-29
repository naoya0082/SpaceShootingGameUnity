using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTypeA_Manager : MonoBehaviour
{
    Rigidbody2D rb2d;
    [Tooltip("Health points in integer")]
    public int health;

    [Tooltip("VFX prefab generating after destruction")]
    public GameObject destructionVFX;
    //public GameObject hitEffect;
    public Transform firePointCenter, firePointRight, firePointLeft;
    public GameObject bossBullet;
    GameObject gameManager;
    float velocityX = 0;
    const float bossFightPos = 7;
    const float borderRight = 2.5f;
    const float borderLeft = -2.5f;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(0, -2);
        gameManager = GameObject.Find("GameManager");
        gameManager.GetComponent<GameManager>().ShowWarning();


        StartCoroutine(Fire());
    }

    private void Update()
    {
        if(transform.position.y < bossFightPos)
        {
            rb2d.velocity = new Vector2(velocityX, 0);
        }
        if (transform.position.x > borderRight)
        {
            transform.position = new Vector3(borderRight, transform.position.y,0);
        } else if (transform.position.x < borderLeft)
        {
            transform.position = new Vector3(borderLeft, transform.position.y, 0);
        }

    }

    private IEnumerator Fire()
    {
        int fireCount;
        while (true)
        {
            yield return new WaitForSeconds(5f);
            FireCenter();
            yield return new WaitForSeconds(1f);
            FireLeft();
            yield return new WaitForSeconds(0.3f);
            FireRight();
            yield return new WaitForSeconds(5f);

            fireCount = Random.Range(2, 6);
            for (int i = 0; i < fireCount; i++)
            {
                FireCenter();
                FireLeft();
                FireRight();
                yield return new WaitForSeconds(0.3f);
            }
            yield return new WaitForSeconds(3f);
           
            FireLeft();
            yield return new WaitForSeconds(0.3f);
            FireRight();
            yield return new WaitForSeconds(0.3f);
            FireLeft();
            yield return new WaitForSeconds(0.3f);
            FireRight();
            yield return new WaitForSeconds(5f);
            
            fireCount = 10;
            for (int i = 0; i < fireCount; i++)
            {
                FireCenter();
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    //ボスの攻撃処理
    private void FireCenter()
    {     
        Instantiate(bossBullet, firePointCenter.transform.position, firePointCenter.transform.rotation);
    }
    private void FireLeft()
    {    
        Instantiate(bossBullet, firePointLeft.transform.position, firePointLeft.transform.rotation);
    }
    private void FireRight()
    {
        Instantiate(bossBullet, firePointRight.transform.position, firePointRight.transform.rotation);
    }

    //ボスの攻撃処理ここまで

    //method of getting damage for the 'Enemy'
    public void GetDamage(int damage)
    {
        health -= damage;           
        if (health <= 0) Destruction();
        else return;
            
    }

    //method of destroying the 'Enemy'
    void Destruction()
    {
        Instantiate(destructionVFX, transform.position, Quaternion.identity);
        SoundManager.instance.PlaySE(1);
        Destroy(gameObject);
        
        gameManager.GetComponent<GameManager>().Clear("1");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player.instance.GetDamage(1);
        }
    }

   
}
