using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireballManager : MonoBehaviour
{
    Rigidbody2D rb2d;
    float angleX;
    float angleY;
    string scene;
    public float bulletSpeed;

    float radius = 0;
    float angularSpeed = 1;

    private Vector2 center;
    private float angle;


    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene().name;
        center = transform.position;
        rb2d = GetComponent<Rigidbody2D>();

        if(scene == "Stage_1" || scene == "Stage_2" || scene == "Stage_3" || scene == "Stage_5")
        {
            rb2d.velocity = new Vector2(angleX, angleY) * bulletSpeed;
        }
    }

    private void Update()
    {
        if(scene == "Stage_4")
        {
            angle += angularSpeed * Time.deltaTime;
            radius += 0.5f*Time.deltaTime;

            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            transform.position = center + new Vector2(x, y);
        }
    }

    public void SetAngle(float X, float Y)
    {
        angleX = Mathf.Cos(X);
        angleY = Mathf.Sin(Y);
    }
}
