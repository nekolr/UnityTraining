using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 相机跟随玩家移动
/// </summary>
public class CameraControl : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        var position = player.transform.position;
        // 相机的 y 轴不跟随玩家变化
        transform.position = new Vector3(position.x, 0, -10f);
    }
}