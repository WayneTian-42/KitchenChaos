using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    // 厨房预制体scriptable object
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    // 柜台顶部中心位置
    [SerializeField] private Transform counterTopPoint;

    /// <summary>
    /// 交互函数，用于生成厨房中指定的object
    /// </summary>
    public void Interact()
    {
        // Debug.Log("Interact");
        // 在counterTopPoint位置复制一个scriptable object出来
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.GetObjectPrefab(), counterTopPoint);
        // 修改局部位置为全零，防止出现误差？
        kitchenObjectTransform.localPosition = Vector3.zero;

        // 获取生成物体的KitchenObject类，再获取其对应的scriptable object，得到生成物体的名字
        Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().GetObjectName());
    }
}
