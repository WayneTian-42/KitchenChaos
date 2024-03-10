using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveFlashingUI : MonoBehaviour
{
    /// <summary>
    /// 炉灶台
    /// </summary>
    [SerializeField] private StoveCounter stoveCounter;
    /// <summary>
    /// 进度条是否闪烁
    /// </summary>
    private const string IsFlashing = "IsFlashing";
    /// <summary>
    /// 进度条动画控件
    /// </summary>
    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;

        animator.SetBool(IsFlashing, false);
    }

    /// <summary>
    /// 炉灶台进度发生改变
    /// </summary>
    /// <param name="sender">事件发布者: StoveCounter</param>
    /// <param name="e">事件参数: 当前进度百分比</param>
    private void StoveCounter_OnProgressChanged(object sender, IHasProgressBar.OnProgressChangedEvnetArgs e)
    {
        float burnWarningShowAmount = 0.5f;
        // 当煎炸完毕后继续煎炸时间超过50%，进度条闪烁
        bool show = stoveCounter.IsFried() && e.progressNormalized >= burnWarningShowAmount;

        animator.SetBool(IsFlashing, show);
    }
}
