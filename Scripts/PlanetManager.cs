using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlanetManager : MonoBehaviour
{
    Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(0, -1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -15)
        {
            Destroy(gameObject);
        }
    }
}
