using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    // 厨房预制体scriptable object
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    /// <summary>
    /// 获取prefab对应的scriptable object
    /// </summary>
    /// <returns>厨房scirpitable object</returns>
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
}
