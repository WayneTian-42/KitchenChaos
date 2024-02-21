using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounter;
    [SerializeField] GameObject stoveOnCounter;
    [SerializeField] GameObject particleGameOjbect;

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    /// <summary>
    /// 根据灶台状态开关特效
    /// </summary>
    /// <param name="sender">事件发布者：灶台</param>
    /// <param name="e">事件参数：灶台状态</param>
    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangeArgs e)
    {
        // 当煎炸状态时显示特效
        bool showVisual = (e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried);
        stoveOnCounter.SetActive(showVisual);
        particleGameOjbect.SetActive(showVisual);
    }
}
