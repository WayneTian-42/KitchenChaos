using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    /// <summary>
    /// 角色扔掉物品
    /// </summary>
    /// <param name="player">角色</param>
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject()) // 角色手中含有物品
        {
            player.GetKitchenObject().DestroySelf(); // 销毁该物品
        }
    }
}
