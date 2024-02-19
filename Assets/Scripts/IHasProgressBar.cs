using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasProgressBar
{
    /// <summary>
    /// 事件：动作进度发生变化
    /// </summary>
    public event EventHandler<OnProgressChangedEvnetArgs> OnProgressChanged;
    /// <summary>
    /// 动作事件参数：动作进度百分比
    /// </summary>
    public class OnProgressChangedEvnetArgs : EventArgs
    {
        // 动作进度百分比
        public float progressNormalized;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public OnProgressChangedEvnetArgs() 
        {
            progressNormalized = 0f;
        }
        /// <summary>
        /// 设定当前动作进度
        /// </summary>
        /// <param name="_progressNormalized">当前动作进度，取值[0, 1]</param>
        public OnProgressChangedEvnetArgs(float _progressNormalized)
        {
            progressNormalized = _progressNormalized;
        }
    }
}
