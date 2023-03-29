using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script moves the attached object along the Y-axis with the defined speed
/// </summary>
public class DirectMoving : MonoBehaviour {

    [Tooltip("Moving speed on Y axis in local space")]
    public float speed;
    Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (CompareTag("PlayerBullet"))
        {
            rb2d.velocity += new Vector2(0, 1 * speed) * Time.deltaTime;
        }
        else
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
    }
}
