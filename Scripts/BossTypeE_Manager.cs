using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTypeE_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb2d;
    public int health;
    public GameObject destructionVFX;
    public Transform firePointCenter, firePointRight, firePointLeft;
    public FireballManager fireball;
    GameObject player;
    GameObject gameManager;
    float velocityX = 0;
    const float bossFightPos = 7;
    const float borderRight = 2.5f;
    const float borderLeft = -2.5f;
    float speed = 3f;
    float delay = 0f;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameManager.GetComponent<GameManager>().ShowWarning();

        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(0, -2);
        player = GameObject.Find("Player");
        StartCoroutine(Fire());
    }

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

    IEnumerator Fire()
    {
        Transform[] firePoints = { firePointCenter, firePointLeft, firePointRight };
        yield return new WaitForSeconds(3f);
        while (true)
        {

            for (int i = 0; i < 20; i++)
            {
                if (player) FireAtPosition(firePointCenter, player.transform.position);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(3f);

            for (int i = 0; i < 5; i++)
            {
                if (player) StartCoroutine(FireAtPositionGatling(firePointCenter, player.transform.position, 10, 0.05f));
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(1f);
            MovePosition();
            yield return new WaitForSeconds(3f);

            FireCircle(firePointCenter, 30);
            yield return new WaitForSeconds(1f);
            FireCircle(firePointCenter, 20);
            yield return new WaitForSeconds(2f);

            if (player) StartCoroutine(FireAtPosition5Way(firePointCenter, player.transform.position, 0.2f, 3));
            yield return new WaitForSeconds(2f);

            if (player) StartCoroutine(FireAtPosition5Way(firePointLeft, player.transform.position, 0.2f, 3));
            yield return new WaitForSeconds(2f);

            if (player) StartCoroutine(FireAtPosition5Way(firePointRight, player.transform.position, 0.2f, 3));
            yield return new WaitForSeconds(2f);

            foreach (Transform firePoint in firePoints)
            {
                if (player) StartCoroutine(FireAtPosition5Way(firePoint, player.transform.position, 0.1f, 10));
            }
            yield return new WaitForSeconds(3f);
            FireCircle(firePointCenter, 32);

            yield return new WaitForSeconds(2f);
            foreach (Transform firePoint in firePoints)
            {
                if (player) StartCoroutine(FireAtPosition5Way(firePoint, player.transform.position, 0.1f, 3));
            }
            yield return new WaitForSeconds(1f);
            foreach (Transform firePoint in firePoints)
            {
                if (player) StartCoroutine(FireAtPosition5Way(firePoint, player.transform.position, 0.1f, 5));
            }
            yield return new WaitForSeconds(1f);
            foreach (Transform firePoint in firePoints)
            {
                if (player) StartCoroutine(FireAtPosition5Way(firePoint, player.transform.position, 0.1f, 12));
            }
            yield return new WaitForSeconds(2f);
        }

    }

    private void FireAtPosition(Transform point, Vector3 targetPosition)
    {
        Vector3 diffPosition = targetPosition - point.transform.position;
        float angleP = Mathf.Atan2(diffPosition.y, diffPosition.x);
        FireballManager bullet = Instantiate(fireball, point.transform.position, Quaternion.identity);
        bullet.SetAngle(angleP, angleP); 
    }

    private IEnumerator FireAtPositionGatling(Transform point, Vector3 targetPosition, int bulletCount, float interval)
    {
        for(int i = 0; i < bulletCount; i++)
        {
            Vector3 diffPosition = targetPosition - point.transform.position;
            float angleP = Mathf.Atan2(diffPosition.y, diffPosition.x);
            FireballManager bullet = Instantiate(fireball, point.transform.position, Quaternion.identity);
            bullet.SetAngle(angleP, angleP);
            yield return new WaitForSeconds(interval);
        }
    }


    private IEnumerator FireAtPosition5Way(Transform point, Vector3 targetPosition, float interval, int loopCount)
    {
        for (int i = 0; i < loopCount; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                Vector3 diffPosition = targetPosition - point.transform.position;
                float angleP = Mathf.Atan2(diffPosition.y, diffPosition.x);
                angleP += (j - 2) * 0.07f * Mathf.PI; // 弾の角度を調整
                FireballManager bullet = Instantiate(fireball, point.transform.position, Quaternion.identity);
                bullet.SetAngle(angleP, angleP);
            }
            yield return new WaitForSeconds(interval);
        }
    }

    

    private void FireCircle(Transform point, int bulletCount)
    {
        for (int i = 1; i <= bulletCount; i++)
        {
            float angleX = -2 * Mathf.PI * i / bulletCount;
            float angleY = -2 * Mathf.PI * i / bulletCount;
            FireballManager bullet = Instantiate(fireball, point.transform.position, firePointCenter.transform.rotation);
            bullet.SetAngle(angleX, angleY);
        }
    }

    private void MovePosition()
    {
        var path = new List<Vector3>();

        path.Add(new Vector3(0, bossFightPos, 0));

        for (int i = 0; i < 23; i++)
        {
            path.Add(new Vector3(Random.Range(-4, 5), Random.Range(5, 8), 0));
        }

        path.Add(new Vector3(0, bossFightPos, 0));

        iTween.MoveTo(gameObject, iTween.Hash(
            "path", path.ToArray(),
            "speed", speed,
            "delay", delay,
            "easetype", iTween.EaseType.linear,
            "looptype", iTween.LoopType.none
        ));
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
        gameManager.GetComponent<GameManager>().Clear("5");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player.instance.GetDamage(3);
        }
    }
}
