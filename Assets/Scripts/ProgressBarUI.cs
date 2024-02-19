using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    // 切菜台
    [SerializeField] private CuttingCounter cuttingCounter;
    // 进度条
    [SerializeField] private Image progressBarImage;

    private void Start()
    {
        // 要在gameObject.SetActive(false)之前订阅事件，否则会订阅失败
        cuttingCounter.OnCutProgressChanged += CuttingCounter_OncutProgressChanged;
        progressBarImage.fillAmount = 0f;
        HideProgressBar();
    }

    /// <summary>
    /// 更新切割进度条
    /// </summary>
    /// <param name="sender">事件发布者</param>
    /// <param name="e">事件参数：切割进度</param>
    private void CuttingCounter_OncutProgressChanged(object sender, CuttingCounter.OnCutProgressChangedEvnetArgs e)
    {
        progressBarImage.fillAmount = e.progressNormalized;
        if (progressBarImage.fillAmount != 0 && progressBarImage.fillAmount != 1)
        {
            ShowProgressBar();
        }
        else
        {
            HideProgressBar();
        }
    }

    /// <summary>
    /// 展示进度条UI
    /// </summary>
    private void ShowProgressBar()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 隐藏进度条UI
    /// </summary>
    private void HideProgressBar()
    {
        gameObject.SetActive(false);
    }
}
