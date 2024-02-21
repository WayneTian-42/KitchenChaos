using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefSO : ScriptableObject
{
    /// <summary>
    /// 切菜
    /// </summary>
    public AudioClip[] chop;
    /// <summary>
    /// 交付失败
    /// </summary>
    public AudioClip[] deliveryFail;
    /// <summary>
    /// 交付成功
    /// </summary>
    public AudioClip[] deliverySuccess;
    /// <summary>
    /// 走路
    /// </summary>
    public AudioClip[] footstep;
    /// <summary>
    /// 放下物品
    /// </summary>
    public AudioClip[] objectDrop;
    /// <summary>
    /// 捡起物品
    /// </summary>
    public AudioClip[] objectPickup;
    /// <summary>
    /// 灶台
    /// </summary>
    public AudioClip stoveSizzle;
    /// <summary>
    /// 垃圾桶
    /// </summary>
    public AudioClip[] trash;
    /// <summary>
    /// 警报
    /// </summary>
    public AudioClip[] warning;
}
