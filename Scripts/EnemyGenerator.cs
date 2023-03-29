using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyGenerator : MonoBehaviour
{
    string scene;
    public GameObject enemyTypeA, enemyTypeB, enemyTypeC, enemyTypeD, enemyTypeE, boss;
    Vector3 enemySpownPosition;
    //bool isGenerate = true;
    int spownCount;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene().name;
        StartCoroutine(CreateEnemy(scene));
    }

    IEnumerator CreateEnemy(string scene)
    {
        switch (scene)
        {
            //ステージ1
            case "Stage_1":
                bool isSpown = true;
                spownCount = 2;
                for (int i = 0; i < spownCount; i++)
                {
                    enemySpownPosition = new Vector3(Random.Range(-3, 4), 12, 0);
                    Instantiate(enemyTypeA, enemySpownPosition, Quaternion.identity);
                    yield return new WaitForSeconds(2f);
                }
                yield return new WaitForSeconds(3f);

                enemySpownPosition = new Vector3(Random.Range(-3, 4), 12, 0);
                Instantiate(enemyTypeB, enemySpownPosition, Quaternion.identity);
                yield return new WaitForSeconds(1f);

                spownCount = 5;
                for (int i = 0; i < spownCount; i++)
                {
                    enemySpownPosition = new Vector3(Random.Range(-4, 5), 12, 0);
                    Instantiate(enemyTypeA, enemySpownPosition, Quaternion.identity);
                    yield return new WaitForSeconds(2f);
                }
                yield return new WaitForSeconds(2f);

                spownCount = 10;
                for (int i = 0; i < spownCount; i++)
                {
                    enemySpownPosition = new Vector3(Random.Range(-4, 5), 12, 0);
                    Instantiate(enemyTypeB, enemySpownPosition, Quaternion.identity);
                    yield return new WaitForSeconds(0.5f);
                }
                yield return new WaitForSeconds(5f);

                for (int i = 0; i < spownCount; i++)
                {
                    enemySpownPosition = new Vector3(Random.Range(-4, 5), 12, 0);
                    Instantiate(enemyTypeA, enemySpownPosition, Quaternion.identity);
                    yield return new WaitForSeconds(1.5f);
                }

                yield return new WaitForSeconds(5f);

                spownCount = 8;
                for (int i = 0; i < spownCount; i++)
                {
                    enemySpownPosition = new Vector3(-4+i, 12, 0);
                    Instantiate(enemyTypeB, enemySpownPosition, Quaternion.identity);
                    //yield return new WaitForSeconds(0.5f);
                }

                BossSpown();
                yield return new WaitForSeconds(20f);

                while (isSpown)
                {
                    spownCount = 10;
                    for (int i = 0; i < spownCount; i++)
                    {
                        enemySpownPosition = new Vector3(Random.Range(-4, 5), 12, 0);
                        Instantiate(enemyTypeB, enemySpownPosition, Quaternion.identity);
                        yield return new WaitForSeconds(1f);
                    }
                    yield return new WaitForSeconds(20f);
                }
                break;

            //ステージ2
            case "Stage_2":
                isSpown = true;
                spownCount = 8;

                yield return new WaitForSeconds(5f);

                StartCoroutine(EnemyBLineSpownFromLeft(0.1f));
                yield return new WaitForSeconds(3f);

                StartCoroutine(EnemyBLineSpownFromRight(0.1f));
                yield return new WaitForSeconds(5f);

                StartCoroutine(EnemyBLineSpownFromLeft(0.05f));
                yield return new WaitForSeconds(0.4f);
                StartCoroutine(EnemyBLineSpownFromRight(0.05f));
                yield return new WaitForSeconds(0.4f);
                StartCoroutine(EnemyBLineSpownFromLeft(0.05f));
                yield return new WaitForSeconds(0.4f);
                StartCoroutine(EnemyBLineSpownFromRight(0.05f));
                yield return new WaitForSeconds(5f);

                StartCoroutine(EnemyBGroupSpown(5, 0));
                yield return new WaitForSeconds(5f);

                spownCount = 10;
                for (int i = 0; i < spownCount; i++)
                {
                    enemySpownPosition = new Vector3(Random.Range(-4, 5), 12, 0);
                    Instantiate(enemyTypeA, enemySpownPosition, Quaternion.identity);
                    yield return new WaitForSeconds(2f);
                }
                yield return new WaitForSeconds(5f);

                StartCoroutine(EnemyBLineSpownFromLeft(0));
                yield return new WaitForSeconds(0.2f);
                StartCoroutine(EnemyBLineSpownFromLeft(0));
                yield return new WaitForSeconds(0.2f);
                StartCoroutine(EnemyBLineSpownFromLeft(0));
                yield return new WaitForSeconds(0.2f);
                StartCoroutine(EnemyBLineSpownFromLeft(0));
                yield return new WaitForSeconds(0.2f);
                StartCoroutine(EnemyBLineSpownFromLeft(0));

                yield return new WaitForSeconds(5f);
                BossSpown();
                yield return new WaitForSeconds(10f);

                while (isSpown)
                {
                    spownCount = 10;
                    for (int i = 0; i < spownCount; i++)
                    {
                        enemySpownPosition = new Vector3(Random.Range(-4, 5), 12, 0);
                        Instantiate(enemyTypeB, enemySpownPosition, Quaternion.identity);
                        yield return new WaitForSeconds(1f);
                    }
                    yield return new WaitForSeconds(20f);
                }
                break;

            //ステージ3
            case "Stage_3":

                isSpown = true;
                for (int i = 0; i < 3; i++)
                {
                    enemySpownPosition = new Vector3(Random.Range(-1, 2), 12, 0);
                    Instantiate(enemyTypeC, enemySpownPosition, Quaternion.identity);
                    yield return new WaitForSeconds(1f);
                }

                StartCoroutine(EnemyAGroupSpown(1, -1));
                yield return new WaitForSeconds(5f);
                StartCoroutine(EnemyAGroupSpown(2, 1));
                yield return new WaitForSeconds(5f);
                StartCoroutine(EnemyAGroupSpown(3, 0));
                yield return new WaitForSeconds(3f);

                for (int i = 0; i < 5; i++)
                {
                    enemySpownPosition = new Vector3(Random.Range(-1, 2), 12, 0);
                    Instantiate(enemyTypeC, enemySpownPosition, Quaternion.identity);
                    yield return new WaitForSeconds(2f);
                }
                yield return new WaitForSeconds(2f);
                StartCoroutine(EnemyAGroupSpown(5, 0));

                yield return new WaitForSeconds(20f);
                BossSpown();

                yield return new WaitForSeconds(3f);

                while (isSpown)
                {
                    enemySpownPosition = new Vector3(Random.Range(-1, 2), 12, 0);
                    Instantiate(enemyTypeC, enemySpownPosition, Quaternion.identity);
                    yield return new WaitForSeconds(10f);
                }
                break;

            //ステージ4
            case "Stage_4":
                enemySpownPosition = new Vector3(7, -2, 0);
                for (int i = 0; i < 5; i++)
                {
                    Instantiate(enemyTypeA, enemySpownPosition, Quaternion.identity);
                    yield return new WaitForSeconds(1f);
                }
                yield return new WaitForSeconds(5f);

                enemySpownPosition = new Vector3(7, 7, 0);
                Instantiate(enemyTypeD, enemySpownPosition, Quaternion.identity);

                yield return new WaitForSeconds(10f);
                enemySpownPosition = new Vector3(-6, -2, 0);
                for (int i = 0; i < 10; i++)
                {
                    Instantiate(enemyTypeA, enemySpownPosition, Quaternion.identity);
                    yield return new WaitForSeconds(1f);
                }
                yield return new WaitForSeconds(2f);

                enemySpownPosition = new Vector3(6, -6, 0);
                for (int i = 0; i < 7; i++)
                {
                    Instantiate(enemyTypeA, enemySpownPosition, Quaternion.identity);
                    yield return new WaitForSeconds(0.8f);
                }

                enemySpownPosition = new Vector3(-7, 7, 0);
                Instantiate(enemyTypeD, enemySpownPosition, Quaternion.identity);
                yield return new WaitForSeconds(5f);

               

                yield return new WaitForSeconds(10f);
                enemySpownPosition = new Vector3(-7, -1, 0);
                for (int i = 0; i < 5; i++)
                {
                    Instantiate(enemyTypeA, enemySpownPosition, Quaternion.identity);
                    yield return new WaitForSeconds(1f);
                }

                enemySpownPosition = new Vector3(6, 0, 0);
                for (int i = 0; i < 8; i++)
                {
                    Instantiate(enemyTypeA, enemySpownPosition, Quaternion.identity);
                    yield return new WaitForSeconds(1f);
                }

                BossSpown();

                break;

            //ステージ5
            case "Stage_5":
                yield return new WaitForSeconds(5f);
                for (int i = 0; i < 2; i++)
                {
                    enemySpownPosition = new Vector3(Random.Range(-3, 4), 12, 0);
                    Instantiate(enemyTypeE, enemySpownPosition, Quaternion.identity);
                    yield return new WaitForSeconds(2f);
                }
                for (int i = 0; i < 10; i++)
                {
                    enemySpownPosition = new Vector3(4, 12, 0);
                    Instantiate(enemyTypeA, enemySpownPosition, Quaternion.identity);
                    yield return new WaitForSeconds(0.5f);
                }
                for (int i = 0; i < 10; i++)
                {
                    enemySpownPosition = new Vector3(7, -2, 0);
                    Instantiate(enemyTypeA, enemySpownPosition, Quaternion.identity);
                    yield return new WaitForSeconds(0.5f);
                }
                yield return new WaitForSeconds(2f);

                for (int i = 0; i < 3; i++)
                {
                    enemySpownPosition = new Vector3(Random.Range(-3, 4), 12, 0);
                    Instantiate(enemyTypeE, enemySpownPosition, Quaternion.identity);
                    yield return new WaitForSeconds(1f);
                }
                yield return new WaitForSeconds(2f);

                enemySpownPosition = new Vector3(-6, 0, 0);
                for (int i = 0; i < 7; i++)
                {
                    Instantiate(enemyTypeC, enemySpownPosition, Quaternion.identity);
                    yield return new WaitForSeconds(2f);
                }

                enemySpownPosition = new Vector3(6, -6, 0);
                for (int i = 0; i < 10; i++)
                {
                    Instantiate(enemyTypeA, enemySpownPosition, Quaternion.identity);
                    yield return new WaitForSeconds(0.5f);
                }

                
                enemySpownPosition = new Vector3(7, 5, 0);
                for (int i = 0; i < 3; i++)
                {
                    Instantiate(enemyTypeC, enemySpownPosition, Quaternion.identity);
                    yield return new WaitForSeconds(2f);
                }
                yield return new WaitForSeconds(5f);

                BossSpown();
                break;

        }
    }

    void BossSpown()
    {
        enemySpownPosition = new Vector3(0, 12, 0);
        Instantiate(boss, enemySpownPosition, Quaternion.identity);
    }

    // 敵のグループを生成するコルーチン
    IEnumerator EnemyAGroupSpown(int maxLineCount, int centerPos)
    {
        spownCount = 0;
        // ラインの数だけ繰り返す
        for (int i = 0; i < maxLineCount; i++)
        {
            // 最初のラインは敵を1体生成する
            if (i == 0) spownCount = 1;
            // それ以外のラインは、前のラインの敵の数+2体生成する
            else spownCount += 2;
            // ラインの中心に一番近い敵の生成位置を計算する
            for (int j = 0; j < spownCount; j++)
            {
                enemySpownPosition = new Vector3(-i + centerPos + j, 12, 0);
                // 敵の生成
                Instantiate(enemyTypeA, enemySpownPosition, Quaternion.identity);
            }
            // 次のラインの生成までのウェイト
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator EnemyBGroupSpown(int maxLineCount, int centerPos)
    {
        spownCount = 0;
        // ラインの数だけ繰り返す
        for (int i = 0; i < maxLineCount; i++)
        {
            // 最初のラインは敵を1体生成する
            if (i == 0) spownCount = 1;
            // それ以外のラインは、前のラインの敵の数+2体生成する
            else spownCount += 2;
            // ラインの中心に一番近い敵の生成位置を計算する
            for (int j = 0; j < spownCount; j++)
            {
                enemySpownPosition = new Vector3(-i + centerPos + j, 12, 0);
                // 敵の生成
                Instantiate(enemyTypeB, enemySpownPosition, Quaternion.identity);
            }
            // 次のラインの生成までのウェイト
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator EnemyBLineSpownFromLeft(float interval)
    {
        for (int i = 0; i < spownCount; i++)
        {
            enemySpownPosition = new Vector3(4 - i, 12, 0);
            Instantiate(enemyTypeB, enemySpownPosition, Quaternion.identity);
            yield return new WaitForSeconds(interval);
        }
    }

    IEnumerator EnemyBLineSpownFromRight(float interval)
    {
        for (int i = 0; i < spownCount; i++)
        {
            enemySpownPosition = new Vector3(-4 + i, 12, 0);
            Instantiate(enemyTypeB, enemySpownPosition, Quaternion.identity);
            yield return new WaitForSeconds(interval);
        }
    }

}
