using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    /// <summary>
    /// 灶台
    /// </summary>
    [SerializeField] private StoveCounter stoveCounter;
    /// <summary>
    /// 音源
    /// </summary>
    private AudioSource audioSource;
    /// <summary>
    /// 是否播放警告音效
    /// </summary>
    private bool playWarningSound;
    /// <summary>
    /// 警告音效播放间隔
    /// </summary>
    private const float MaxWarningSoundTimer = 0.2f;
    /// <summary>
    /// 警告音效播放计时器
    /// </summary>
    private float warningSoundTimer = MaxWarningSoundTimer;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnstateChanged; // 订阅灶台状态切换事件
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged; // 订阅灶台状态切换事件
    }

    private void Update()
    {
        if (playWarningSound)
        {
            warningSoundTimer += Time.deltaTime;
            if (warningSoundTimer >= MaxWarningSoundTimer)
            {
                warningSoundTimer = 0f;

                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
    }

    /// <summary>
    /// 煎炸进度发生变化时，判断是否播放警告音效
    /// </summary>
    /// <param name="sender">事件发布者: StoveCounter</param>
    /// <param name="e">事件参数: 煎炸进度百分比</param>
    private void StoveCounter_OnProgressChanged(object sender, IHasProgressBar.OnProgressChangedEvnetArgs e)
    {
        float burnWarningShowAmount = 0.5f;
        // 当煎炸完毕后继续煎炸时间超过50%，播放警告音效
        playWarningSound = stoveCounter.IsFried() && e.progressNormalized >= burnWarningShowAmount;
    }

    /// <summary>
    /// 根据灶台状态播放音效
    /// </summary>
    /// <param name="sender">事件发布者：灶台</param>
    /// <param name="e">事件参数：灶台状态</param>
    private void StoveCounter_OnstateChanged(object sender, StoveCounter.OnStateChangeArgs e)
    {
        if (e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried) // 在煎炸
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}
