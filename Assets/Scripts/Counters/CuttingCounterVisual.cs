using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    // 切菜台
    [SerializeField] private CuttingCounter cuttingCounter;
    // 动画参数
    private const string Cut = "Cut";
    // 动画控件
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_OnCut;
    }

    /// <summary>
    /// 角色切菜，触发事件
    /// </summary>
    /// <param name="sender">事件发布者</param>
    /// <param name="e">事件参数</param>
    private void CuttingCounter_OnCut(object sender, EventArgs e)
    {
        animator.SetTrigger(Cut);
    }
}
