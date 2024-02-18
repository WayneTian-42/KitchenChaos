using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    // 厨房预制体scriptable object
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    // 该object所属的父对象
    // 修改该属性使得不同类都可以通过接口调用从而成为物品的父对象
    private IKitchenObjectParent kitchenObjectParent;

    /// <summary>
    /// 生成物品并设置父对象
    /// </summary>
    /// <param name="newKitchenObjectSO">物品对应的脚本对象</param>
    /// <param name="kitchenObjectParent">物品父对象</param>
    public static void SpawnKitchenObject(KitchenObjectSO newKitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        // 创建一个物品
        Transform kitchenObjectTransform = Instantiate(newKitchenObjectSO.GetObjectPrefab());
        // 获取物品对象
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        // 直接更新物品父对象
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
    }

    /// <summary>
    /// 获取prefab对应的scriptable object
    /// </summary>
    /// <returns>厨房scirpitable object</returns>
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    /// <summary>
    /// 设置物品对应的父对象并修改逻辑以及视觉效果
    /// </summary>
    /// <param name="newKitchenObjectParent">物品的父对象</param>
    public void SetKitchenObjectParent(IKitchenObjectParent newKitchenObjectParent)
    {
        // 修改父对象和物品的绑定逻辑
        // 清空当前对象
        if (kitchenObjectParent != null)
        {
            kitchenObjectParent.ClearKitchenObject();
        }

        // 如果新的父对象已经具有物品，则报错
        if (newKitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("Parent already had a kitchen object!");
        }

        // 更新父对象
        kitchenObjectParent = newKitchenObjectParent;
        // 更新父对象的物品
        kitchenObjectParent.SetKitchenObject(this);

        // 直接修改物品的父级对象，相当于同时移动在Hierarchy中的逻辑结构以及视觉效果上的物理位置
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    /// <summary>
    /// 获取物品对应的父对象
    /// </summary>
    /// <returns>物品对应的父对象</returns>
    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    /// <summary>
    /// 销毁物品
    /// </summary>
    public void DestroySelf()
    {
        // 清除物品父对象对该物品的绑定
        GetKitchenObjectParent().ClearKitchenObject();
        Destroy(gameObject);
    }
}
