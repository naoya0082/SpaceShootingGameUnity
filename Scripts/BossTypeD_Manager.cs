using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTypeD_Manager : MonoBehaviour
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
    const float bossFightPos = 6;
    const float borderRight = 2.5f;
    const float borderLeft = -2.5f;
    float speed = 3f;
    float delay = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(0, -2);
        gameManager = GameObject.Find("GameManager");
        gameManager.GetComponent<GameManager>().ShowWarning();

        Invoke("MovePosition", 6);
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
        yield return new WaitForSeconds(6f);

        while (true)
        {
            StartCoroutine(FireTornado(firePointCenter, 25));
            yield return new WaitForSeconds(7f);

            StartCoroutine(FireTornado(firePointLeft, 35));
            StartCoroutine(FireTornado(firePointRight, 35));

            yield return new WaitForSeconds(7f);
            StartCoroutine(FireTornado(firePointCenter, 55));
            yield return new WaitForSeconds(15f);
        }

    }

    private void MovePosition()
    {
        var path = new Vector3[]
       {
            new Vector3(0, bossFightPos, 0),
            new Vector3(Random.Range(-4,5), Random.Range(5,8), 0),
            new Vector3(Random.Range(-4,5), Random.Range(5,8), 0),
            new Vector3(Random.Range(-4,5), Random.Range(5,8), 0),
            new Vector3(Random.Range(-4,5), Random.Range(5,8), 0),
            new Vector3(Random.Range(-4,5), Random.Range(5,8), 0),
            new Vector3(Random.Range(-4,5), Random.Range(5,8), 0),
            new Vector3(Random.Range(-4,5), Random.Range(5,8), 0),
            new Vector3(Random.Range(-4,5), Random.Range(5,8), 0),
            new Vector3(Random.Range(-4,5), Random.Range(5,8), 0),
            new Vector3(Random.Range(-4,5), 8, 0),
            new Vector3(Random.Range(-3,4), -8, 0),
            new Vector3(Random.Range(-3,4), 8, 0),
            new Vector3(Random.Range(-3,4), -8, 0),
            new Vector3(Random.Range(-3,4), 8, 0),
            new Vector3(0, -8, 0),
            new Vector3(Random.Range(-4,5), Random.Range(5,8), 0),
            new Vector3(Random.Range(-4,5), Random.Range(5,8), 0),
            new Vector3(0, bossFightPos, 0),

       };

        iTween.MoveTo(gameObject, iTween.Hash(
            "path", path,
            "speed", speed,
            "delay", delay,
            "easetype", iTween.EaseType.easeInOutSine,
            "looptype", iTween.LoopType.loop
        ));
    }

    //ボスの攻撃設定
    IEnumerator FireTornado(Transform firePos, int bulletCount)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            Instantiate(bossBullet, firePos.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.08f);
        }
    }

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
        gameManager.GetComponent<GameManager>().Clear("4");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player.instance.GetDamage(3);
        }
    }
}
