using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTypeB_Manager : MonoBehaviour
{
    Rigidbody2D rb2d;
    [Tooltip("Health points in integer")]
    public int health;

    [Tooltip("VFX prefab generating after destruction")]
    public GameObject destructionVFX;
    public Transform firePointCenter, firePointRight, firePointLeft;
    public GameObject bossBullet;
    public FireballManager fireball;
    GameObject gameManager;
    float velocityX = 0;
    const float bossFightPos = 7;
    const float borderRight = 2.5f;
    const float borderLeft = -2.5f;
    float bulletAngleX;
    float bulletAngleY;
  

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(0, -2);
        gameManager = GameObject.Find("GameManager");
        gameManager.GetComponent<GameManager>().ShowWarning();

        //bulletSpeed = bossBullet.GetComponent<DirectMoving>().speed;
        StartCoroutine(Fire());

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < bossFightPos)
        {
            rb2d.velocity = new Vector2(velocityX, 0);
        }
        if (transform.position.x > borderRight)
        {
            transform.position = new Vector3(borderRight, transform.position.y, 0);
        }
        else if (transform.position.x < borderLeft)
        {
            transform.position = new Vector3(borderLeft, transform.position.y, 0);
        }
    }

    private IEnumerator Fire()
    {
        float movingSec = 2;
        while (true)
        {
            yield return new WaitForSeconds(5f);
            Fire_circle_center(20);
            yield return new WaitForSeconds(3f);

            StartCoroutine(ChangePosition(movingSec));
            for (int i = 0; i < 4; i++)
            {
                Fire_circle_center(10);
                Fire_circle_left(13);
                Fire_circle_right(13);

                yield return new WaitForSeconds(0.8f);
            }

            yield return new WaitForSeconds(1f);
            Fire_Left();
            yield return new WaitForSeconds(0.5f);
            Fire_Right();
            yield return new WaitForSeconds(0.5f);
            Fire_Left();
            yield return new WaitForSeconds(0.5f);
            Fire_Right();

            yield return new WaitForSeconds(3f);
            StartCoroutine(ChangePosition(movingSec));
            for (int i = 0; i < 5; i++)
            {
                Fire_Center();
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(3f);

            Fire_circle_left(13);
            Fire_circle_right(13);
            yield return new WaitForSeconds(2f);

            Fire_circle_center(21);
            yield return new WaitForSeconds(2f);

            for (int i = 0; i < 6; i++)
            {
                Fire_circle_center(8+i);
                Fire_circle_left(10);
                Fire_circle_right(10);

                yield return new WaitForSeconds(0.6f);
            }

        }
    }

    //ボスの攻撃処理
    private void Fire_Center()
    {
        Instantiate(bossBullet, firePointCenter.transform.position, firePointCenter.transform.rotation);
    }
    private void Fire_Left()
    {
        Instantiate(bossBullet, firePointLeft.transform.position, firePointLeft.transform.rotation);
    }
    private void Fire_Right()
    {
        Instantiate(bossBullet, firePointRight.transform.position, firePointRight.transform.rotation);
    }

    private void Fire_circle_center(int bulletCount)
    {
        for (int count = 1; count <= bulletCount; count++)
        {
            bulletAngleX = -2 * Mathf.PI * count / bulletCount;
            bulletAngleY = -2 * Mathf.PI * count / bulletCount;
            FireballManager bullet = Instantiate(fireball, firePointCenter.transform.position, firePointCenter.transform.rotation);
            bullet.SetAngle(bulletAngleX, bulletAngleY);
        }
    }

    private void Fire_circle_left(int bulletCount)
    {
        for (int count = 1; count <= bulletCount; count++)
        {
            bulletAngleX = -2 * Mathf.PI * count / bulletCount;
            bulletAngleY = -2 * Mathf.PI * count / bulletCount;
            FireballManager bullet = Instantiate(fireball, firePointLeft.transform.position, Quaternion.identity);
            bullet.SetAngle(bulletAngleX, bulletAngleY);
        }
    }

    private void Fire_circle_right(int bulletCount)
    {
        for (int count = 1; count <= bulletCount; count++)
        {
            bulletAngleX = -2 * Mathf.PI * count / bulletCount;
            bulletAngleY = -2 * Mathf.PI * count / bulletCount;
            FireballManager bullet = Instantiate(fireball, firePointRight.transform.position, Quaternion.identity);
            bullet.SetAngle(bulletAngleX, bulletAngleY);
        }
    }


    private IEnumerator ChangePosition(float time)
    {
        if (transform.position.x <= borderLeft + 1) velocityX = Random.Range(1, 4);
        else if (transform.position.x >= borderRight - 1) velocityX = Random.Range(-3, 0);
        else velocityX = Random.Range(-3, 4);

        yield return new WaitForSeconds(time);
        velocityX = 0;

    }

    //method of getting damage for the 'Enemy'
    public void GetDamage(int damage)
    {
        health -= damage;           //reducing health for damage value, if health is less than 0, starting destruction procedure
        if (health <= 0)
            Destruction();
        else return;
    }

    //method of destroying the 'Enemy'
    void Destruction()
    {
        Instantiate(destructionVFX, transform.position, Quaternion.identity);
        SoundManager.instance.PlaySE(1);
        Destroy(gameObject);
        gameManager.GetComponent<GameManager>().Clear("2");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player.instance.GetDamage(1);
        }
    }
}
