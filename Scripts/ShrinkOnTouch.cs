using UnityEngine;
using UnityEngine.UI;

public class ShrinkOnTouch : MonoBehaviour
{
    private Vector3 originalScale;  // 元のスケールを保持するための変数

    private void Start()
    {
        originalScale = transform.localScale;  // スタート時に元のスケールを保持する
    }

    public void OnPointerDown()
    {
        transform.localScale = originalScale * 0.95f;  // タッチしたときにスケールを0.95倍にする
    }

    public void OnPointerUp()
    {
        transform.localScale = originalScale;  // タッチを離したときに元のスケールに戻す
    }
}