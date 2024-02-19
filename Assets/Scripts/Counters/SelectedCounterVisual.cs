using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    // 柜台
    [SerializeField] private BaseCounter baseCounter;
    // 选中柜台后的视觉效果
    [SerializeField] private GameObject[] visualGameObjectArray;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectionCounterChanged;
    }

    /// <summary>
    /// 判断选中的计数器是否为当前计数器，更新视觉效果
    /// </summary>
    /// <param name="sender">事件发布者</param>
    /// <param name="eventArgs">选中的计数器</param>
    private void Player_OnSelectionCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs eventArgs)
    {
        if (baseCounter == eventArgs.selectedCounter)
        {
            ShowVisual();
        }
        else
        {
            HideVisual();
        }
    }

    /// <summary>
    /// 展示视觉效果
    /// </summary>
    private void ShowVisual()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
            visualGameObject.SetActive(true);
    }

    /// <summary>
    /// 隐藏视觉效果
    /// </summary>
    private void HideVisual()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
            visualGameObject.SetActive(false);
    }
}
