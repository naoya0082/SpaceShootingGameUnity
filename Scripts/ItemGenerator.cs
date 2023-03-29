using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemGenerator : MonoBehaviour
{
    public GameObject powerUpItem;
    public GameObject RecoveryItem;

    void Start()
    {
        StartCoroutine(CreateBonusItem());
        InvokeRepeating("CreateRecoveryItem", 60, 60);
    }

    private IEnumerator CreateBonusItem()
    {
        Vector3 createPosition = new Vector3(Random.Range(-3, 4), transform.position.y);
        switch(SceneManager.GetActiveScene().name)
        {
            case "Stage_2":
                for (int i = 0; i < 8; i++)
                {
                    Instantiate(powerUpItem, createPosition, Quaternion.identity);
                    if (i < 2) yield return new WaitForSeconds(5f);
                    else yield return new WaitForSeconds(20f);
                }
                break;
            default:
                for (int i = 0; i < 8; i++)
                {
                    Instantiate(powerUpItem, createPosition, Quaternion.identity);
                    yield return new WaitForSeconds(20f);
                }
                break;
        }
    }

    private void CreateRecoveryItem()
    {
        Vector3 createPosition = new Vector3(Random.Range(-3, 4), transform.position.y);
        Instantiate(RecoveryItem, createPosition, Quaternion.identity);
    }
}