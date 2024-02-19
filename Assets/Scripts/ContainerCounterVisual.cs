using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    // 容器柜台
    [SerializeField] private ContainerCounter containerCounter;
    // 动画参数
    private const string OpenClose = "OpenClose";
    // 动画控件
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
    }

    /// <summary>
    /// 角色拿取材料，触发事件
    /// </summary>
    /// <param name="sender">事件发布者</param>
    /// <param name="e">事件参数</param>
    private void ContainerCounter_OnPlayerGrabbedObject(object sender, EventArgs e)
    {
        animator.SetTrigger(OpenClose);
    }
}
