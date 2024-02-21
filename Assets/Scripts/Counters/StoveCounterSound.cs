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

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnstateChanged; // 订阅灶台状态切换事件
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
