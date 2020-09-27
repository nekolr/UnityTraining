using UnityEngine;

/// <summary>
/// 视差控制
/// </summary>
public class Parallax : MonoBehaviour
{
    /// <summary>
    /// 主摄像机
    /// </summary>
    public Transform mainCamera;

    /// <summary>
    /// 移动速率，如果设置为 1，则物体会跟着镜头同速移动，这样也就没有视差效果了
    /// </summary>
    [Range(0, 1)] public float moveRate;

    /// <summary>
    /// 物体起始位置
    /// </summary>
    private float _startX;

    void Start()
    {
        _startX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(_startX + mainCamera.position.x * moveRate, transform.position.y);
    }
}