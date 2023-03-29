using System.Collections;
using UnityEngine;

public class BossTypeC_Manager : MonoBehaviour
{
    Rigidbody2D rb2d;
    public int health;
    public GameObject destructionVFX;
    public Transform firePointCenter, firePointRight, firePointLeft;
    public FireballManager fireball;
    GameObject player;
    GameObject gameManager;
    bool isBossFire = true;
    float velocityX = 0;
    const float bossFightPos = 7;
    const float borderRight = 2.5f;
    const float borderLeft = -2.5f;

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

    private IEnumerator Fire()
    {
        int loopCount = 1;
        while (isBossFire)
        {
            if (loopCount == 1) yield return new WaitForSeconds(5f);
            else                yield return new WaitForSeconds(1f);

            if(player != null)
            {
                StartCoroutine(FireAtPosition(firePointCenter, player.transform.position, 10));
            }
            yield return new WaitForSeconds(2f);

            FireCircle(firePointCenter, 24);
            yield return new WaitForSeconds(0.5f);
            FireCircle(firePointRight, 15);
            FireCircle(firePointLeft, 15);

            yield return new WaitForSeconds(2f);
            StartCoroutine(ChangePosition(1.5f));
            StartCoroutine(FireSpin(firePointCenter,28));
            yield return new WaitForSeconds(3f);

            StartCoroutine(ChangePosition(1.5f));

            StartCoroutine(FireUnderFromRight(10));
            yield return new WaitForSeconds(1f);
            StartCoroutine(FireUnderFromLeft(10));
            yield return new WaitForSeconds(1f);
            StartCoroutine(FireUnderFromRight(10));
            StartCoroutine(FireUnderFromLeft(13));
            yield return new WaitForSeconds(2f);

            StartCoroutine(FireUnderFromRight(10));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(FireUnderFromLeft(13));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(FireUnderFromRight(15));
            yield return new WaitForSeconds(2f);

            StartCoroutine(ChangePosition(1.5f));
            yield return new WaitForSeconds(3f);

            if (player != null)
            {
                StartCoroutine(FireAtPosition(firePointLeft, player.transform.position, 10));
                yield return new WaitForSeconds(1f);
                StartCoroutine(FireAtPosition(firePointRight, player.transform.position, 10));
                yield return new WaitForSeconds(1f);
                StartCoroutine(FireAtPosition(firePointLeft, player.transform.position, 10));
                yield return new WaitForSeconds(1f);
                StartCoroutine(FireAtPosition(firePointRight, player.transform.position, 10));
            }

           if(loopCount == 0) loopCount++;
        }
    }

    private IEnumerator FireAtPosition(Transform point, Vector3 targetPosition, int bulletCount)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 diffPosition = targetPosition - point.transform.position;
            float angleP = Mathf.Atan2(diffPosition.y, diffPosition.x);
            FireballManager bullet = Instantiate(fireball, point.transform.position, Quaternion.identity);
            bullet.SetAngle(angleP, angleP);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator FireSpin(Transform point, int bulletCount)
    {
        float bulletAngleX;
        float bulletAngleY;
        for (int count = 0; count < bulletCount; count++)
        {
            if(point == firePointRight)
            {
                bulletAngleX = -2 * Mathf.PI * (bulletCount - count) / bulletCount;
            } 
            else
            {
                bulletAngleX = -2 * Mathf.PI * count / bulletCount;
            }
            bulletAngleY = -2 * Mathf.PI * count / bulletCount;

            FireballManager bullet = Instantiate(fireball, point.transform.position, Quaternion.identity);
            bullet.SetAngle(bulletAngleX, bulletAngleY);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void FireCircle(Transform point, int bulletCount)
    {
        for (int count = 0; count < bulletCount; count++)
        {
            float bulletAngleX = 2 * Mathf.PI * count / bulletCount;
            float bulletAngleY = 2 * Mathf.PI * count / bulletCount;
            FireballManager bullet = Instantiate(fireball, point.transform.position, Quaternion.identity);
            bullet.SetAngle(bulletAngleX, bulletAngleY);
        }
    }

    private IEnumerator FireUnderFromRight(int bulletCount)
    {
        for (int count = 0; count < bulletCount; count++)
        {
            float bulletAngleX = -Mathf.PI * count / bulletCount;
            float bulletAngleY = -Mathf.PI * count / bulletCount;
            FireballManager bullet = Instantiate(fireball, firePointCenter.transform.position, Quaternion.identity);
            bullet.SetAngle(bulletAngleX, bulletAngleY);
            yield return new WaitForSeconds(0.03f);
        }
    }

    private IEnumerator FireUnderFromLeft(int bulletCount)
    {
        for (int count = 0; count < bulletCount; count++)
        {
            float bulletAngleX = -Mathf.PI * (bulletCount - count) / bulletCount;
            float bulletAngleY = -Mathf.PI * count / bulletCount;
            FireballManager bullet = Instantiate(fireball, firePointCenter.transform.position, firePointCenter.transform.rotation);
            bullet.SetAngle(bulletAngleX, bulletAngleY);
            yield return new WaitForSeconds(0.03f);
        }
    }

    private IEnumerator ChangePosition(float time)
    {
        if (transform.position.x <= borderLeft + 1)
        {
            velocityX = Random.Range(1, 4);
        }
        else if (transform.position.x >= borderRight - 1)
        {
            velocityX = Random.Range(-3, 0);
        }
        else
        {
            velocityX = Random.Range(-3, 4);
        }
        yield return new WaitForSeconds(time);
        velocityX = 0;
    }

    public void GetDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Destruction();
    }

    void Destruction()
    {
        Instantiate(destructionVFX, transform.position, Quaternion.identity);
        SoundManager.instance.PlaySE(1);
        Destroy(gameObject);
        gameManager.GetComponent<GameManager>().Clear("3");
    }
}