using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualCounter;

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
        if (clearCounter == eventArgs.selectedCounter)
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
        visualCounter.SetActive(true);
    }

    /// <summary>
    /// 隐藏视觉效果
    /// </summary>
    private void HideVisual()
    {
        visualCounter.SetActive(false);
    }
}
