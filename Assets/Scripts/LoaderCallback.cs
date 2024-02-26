using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private void Update()
    {
        Loader.LoaderCallback(); // 加载实际游戏场景
    }
}
