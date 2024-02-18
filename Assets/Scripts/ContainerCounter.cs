using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent
{
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
            // 在counterTopPoint位置复制一个scriptable object出来
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.GetObjectPrefab(), GetKitchenObjectFollowTransform());

            // 直接更新物品父对象为角色，同时实现逻辑操作以及视觉更改
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            
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
