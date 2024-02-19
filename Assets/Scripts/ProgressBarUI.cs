using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    /// <summary>
    /// 柜台物体
    /// </summary>
    [SerializeField] private GameObject hasProgressBarGameObject;
    /// <summary>
    /// 进度条
    /// </summary>
    [SerializeField] private Image progressBarImage;

    private void Start()
    {
        // 获取接口
        IHasProgressBar hasProgressBar = hasProgressBarGameObject.GetComponent<IHasProgressBar>();
        // 如果该物体没有实现接口则报错
        if (hasProgressBar == null)
        {
            Debug.LogError("GameObject " + hasProgressBarGameObject + " don't implement interface IHasProgressBar!");
        }
        // 要在gameObject.SetActive(false)之前订阅事件，否则会订阅失败
        hasProgressBar.OnProgressChanged += CuttingCounter_OncutProgressChanged;
        progressBarImage.fillAmount = 0f;
        HideProgressBar();
    }

    /// <summary>
    /// 更新进度条
    /// </summary>
    /// <param name="sender">事件发布者</param>
    /// <param name="e">事件参数：进度</param>
    private void CuttingCounter_OncutProgressChanged(object sender, IHasProgressBar.OnProgressChangedEvnetArgs e)
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
