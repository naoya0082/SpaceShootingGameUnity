using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileGenerator : MonoBehaviour
{
    public GameObject missile; 

    public IEnumerator CreateMissile()
    {
        for(int i = 0; i < 3; i ++)
        {
            for (int j = -2; j < 3; j++)
            {
                Vector3 missilePosition = new Vector3(j + j, -13, 0);
                Instantiate(missile, missilePosition, Quaternion.identity);
                yield return new WaitForSeconds(0.4f);
            }
        }
    }
}
