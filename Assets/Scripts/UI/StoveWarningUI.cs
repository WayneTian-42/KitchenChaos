using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveWarningUI : MonoBehaviour
{
    /// <summary>
    /// 炉灶台
    /// </summary>
    [SerializeField] private StoveCounter stoveCounter;

    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;

        Hide();
    }

    /// <summary>
    /// 炉灶台进度发生改变
    /// </summary>
    /// <param name="sender">事件发布者: StoveCounter</param>
    /// <param name="e">事件参数: 当前进度百分比</param>
    private void StoveCounter_OnProgressChanged(object sender, IHasProgressBar.OnProgressChangedEvnetArgs e)
    {
        float burnWarningShowAmount = 0.5f;
        // 当煎炸完毕后继续煎炸时间超过50%，显示警告图标
        bool show = stoveCounter.IsFried() && e.progressNormalized >= burnWarningShowAmount;

        if (show)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    /// <summary>
    /// 展示UI
    /// </summary>
    private void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 隐藏UI
    /// </summary>
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
