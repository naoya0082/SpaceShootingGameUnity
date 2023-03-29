using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script defines the borders of ‘Player’s’ movement. Depending on the chosen handling type, it moves the ‘Player’ together with the pointer.
/// </summary>

[System.Serializable]
public class Borders
{
    [Tooltip("offset from viewport borders for player's movement")]
    public float minXOffset = 1.5f, maxXOffset = 1.5f, minYOffset = 1.5f, maxYOffset = 1.5f;
    [HideInInspector] public float minX, maxX, minY, maxY;
}

public class PlayerMoving : MonoBehaviour {

    private Vector3 startPosition = new Vector3(0,0,0); // クリック開始位置
    private Vector3 diffPosition; // playerとクリック開始位置との差



    [Tooltip("offset from viewport borders for player's movement")]
    public Borders borders;
    Camera mainCamera;
    bool controlIsActive = true; 

    public static PlayerMoving instance; //unique instance of the script for easy access to the script

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // プレイヤーの移動可能範囲を制御
    private void Start()
    {
        mainCamera = Camera.main;
        ResizeBorders();                //setting 'Player's' moving borders deending on Viewport's size
    }

    // プレイヤーの移動
    private void Update()
    {
        if (controlIsActive)
        {
#if UNITY_STANDALONE || UNITY_EDITOR    //if the current platform is not mobile, setting mouse handling

            if (Input.GetMouseButtonDown(0)) // マウスの左クリックが押されたら
            {
                startPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition); // クリック開始位置を保存
                startPosition.z = transform.position.z;

                //プレイヤーとタッチ場所との差分を計算
                diffPosition = transform.position - startPosition;
            }

            if (Input.GetMouseButton(0)) //if mouse button was pressed       
            {
                Vector3 playerPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition) + diffPosition; //calculating mouse position in the worldspace
                playerPosition.z = transform.position.z;

                transform.position = Vector3.MoveTowards(transform.position, playerPosition, 30 * Time.deltaTime);
            }

#endif

#if UNITY_IOS || UNITY_ANDROID //if current platform is mobile, 

            if (Input.touchCount > 0) // タッチされている場合
            {
                Touch touch = Input.GetTouch(0); // 最初に検出されたタッチを取得

                if (touch.phase == TouchPhase.Began) // タッチ開始時
                {
                    startPosition = mainCamera.ScreenToWorldPoint(touch.position); // タッチ開始位置を保存
                    startPosition.z = transform.position.z;

                    //プレイヤーとタッチ場所との差分を計算
                    diffPosition = transform.position - startPosition;
                }

                if (touch.phase == TouchPhase.Moved) // タッチ中
                {
                    Vector3 playerPosition = mainCamera.ScreenToWorldPoint(touch.position) + diffPosition; // タッチ位置をワールド座標に変換し、プレイヤーとタッチ場所との差分を加算
                    playerPosition.z = transform.position.z;

                    transform.position = Vector3.MoveTowards(transform.position, playerPosition, 30 * Time.deltaTime);
                }
            }
#endif
            transform.position = new Vector3    //if 'Player' crossed the movement borders, returning him back 
                (
                Mathf.Clamp(transform.position.x, borders.minX, borders.maxX),
                Mathf.Clamp(transform.position.y, borders.minY, borders.maxY),
                0
                );
        }
    }

    //setting 'Player's' movement borders according to Viewport size and defined offset
    void ResizeBorders() 
    {
        borders.minX = mainCamera.ViewportToWorldPoint(Vector2.zero).x + 1;
        borders.minY = mainCamera.ViewportToWorldPoint(Vector2.zero).y + 2.5f;
        borders.maxX = mainCamera.ViewportToWorldPoint(Vector2.right).x - 1;
        borders.maxY = mainCamera.ViewportToWorldPoint(Vector2.up).y - 2.5f;
    }
}
