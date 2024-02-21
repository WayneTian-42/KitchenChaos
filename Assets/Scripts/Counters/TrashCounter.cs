using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    /// <summary>
    /// 事件：角色扔进垃圾桶
    /// </summary>
    public static event EventHandler OnAnyObjectTrashed;
    /// <summary>
    /// 角色扔掉物品
    /// </summary>
    /// <param name="player">角色</param>
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject()) // 角色手中含有物品
        {
            player.GetKitchenObject().DestroySelf(); // 销毁该物品

            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty); // 委托事件，触发音效
        }
    }
}
