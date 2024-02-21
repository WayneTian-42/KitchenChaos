using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    /// <summary>
    /// 脚步音效最大间隔时间
    /// </summary>
    private const float MaxFootstepTime = 0.3f;
    /// <summary>
    /// 脚步音效计时器
    /// </summary>
    private float footstepTimer = MaxFootstepTime;

    private void Update()
    {
        footstepTimer += Time.deltaTime;
        if (footstepTimer >= MaxFootstepTime) // 计时器大于最大间隔，能够播放音效
        {
            footstepTimer = 0f;
            if (Player.Instance.IsWalking()) // 角色正在移动
            {
                float volume = 1f;
                SoundManager.Instance.PlayerFootstepSound(Player.Instance.transform.position, volume); // 播放音效
            }
        }
    }
}
