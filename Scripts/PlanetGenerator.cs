using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGenerator : MonoBehaviour
{
    public GameObject planetA;
    public GameObject planetB;
    public GameObject planetC;
    Vector3 generatPosition;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreatePlanet());
    }

    IEnumerator CreatePlanet()
    {
        while (true)
        {
            generatPosition = new Vector3(Random.Range(-7, 7), transform.position.y);
            Instantiate(planetA, generatPosition, transform.rotation);
            yield return new WaitForSeconds(20f);
            generatPosition = new Vector3(Random.Range(-7, 7), transform.position.y);
            Instantiate(planetB, generatPosition, transform.rotation);
            yield return new WaitForSeconds(20f);
            generatPosition = new Vector3(Random.Range(-7, 7), transform.position.y);
            Instantiate(planetC, generatPosition, transform.rotation);
            yield return new WaitForSeconds(20f);

        }
    }
}
