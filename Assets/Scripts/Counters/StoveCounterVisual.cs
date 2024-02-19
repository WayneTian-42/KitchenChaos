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

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangeArgs e)
    {
        // 当煎炸状态时显示特效
        bool showVisual = (e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried);
        stoveOnCounter.SetActive(showVisual);
        particleGameOjbect.SetActive(showVisual);
    }
}
