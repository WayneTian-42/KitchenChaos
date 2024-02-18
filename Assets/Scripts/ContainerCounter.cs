using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent
{
    // 角色拿取物品事件，用于播放动画
    public event EventHandler OnPlayerGrabbedObject;
    // 厨房预制体scriptable object
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    /// <summary>
    /// 角色和容器柜台交互，将原材料置于角色手中
    /// </summary>
    /// <param name="player">角色</param>
    public override void Interact(Player player)
    {
        // Debug.Log("Interact");
        // 只有当角色物品为空时才能交互，保证最多只存在一个物品
        if (!player.HasKitchenObject())
        {
            // 生成物品
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

            // 发布事件，用于播放动画
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            
            // 获取生成物体的KitchenObject类，再获取其对应的scriptable object，得到生成物体的名字
            // Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().GetObjectName());
        }
        // else
        // {
        //     // Debug.Log(kitchenObject);
        //     // kitchenObject.SetKitchenObjectParent(player);
        // }
    }
}
