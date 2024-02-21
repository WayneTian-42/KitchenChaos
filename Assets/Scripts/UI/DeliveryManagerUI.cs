using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    /// <summary>
    /// 订单显示区域
    /// </summary>
    [SerializeField] private Transform container;
    /// <summary>
    /// 订单模板
    /// </summary>
    [SerializeField] private Transform recipeTemplate;
    /// <summary>
    /// 订单字典，订单名作为key，订单的UI实现的队列为键，使用队列来保证先进先出
    /// </summary>
    private Dictionary<string, Queue<Transform>> recipeTransfomrDict;

    private void Awake()
    {
        recipeTransfomrDict = new Dictionary<string, Queue<Transform>>();
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
    }

    /// <summary>
    /// 生成菜单更新UI
    /// </summary>
    /// <param name="sender">事件发布者：Delivery Manager</param>
    /// <param name="e">事件参数：生成的菜单</param>
    private void DeliveryManager_OnRecipeSpawned(object sender, DeliveryManager.DeliveryRecipeArgs e)
    {
        string recipeName = e.recipeSO.recipeName; // 菜单名
        Transform recipeTransform = SpawnRecipeUI(e.recipeSO); // 生成UI
        if (recipeTransfomrDict.TryGetValue(recipeName, out _) == false) // 该菜单第一次生成
        {
            Queue<Transform> transformQueue = new Queue<Transform>(); // 初始化队列
            recipeTransfomrDict.Add(recipeName, transformQueue);
        }
        recipeTransfomrDict[recipeName].Enqueue(recipeTransform); // 将UI加入队列
    }

    /// <summary>
    /// 完成订单，更新UI
    /// </summary>
    /// <param name="sender">事件发布者：Delivery Manager</param>
    /// <param name="e">事件参数：完成的订单</param>
    private void DeliveryManager_OnRecipeCompleted(object sender, DeliveryManager.DeliveryRecipeArgs e)
    {
        string recipeName = e.recipeSO.recipeName; // 订单名
        // 将订单队列中第一个UI销毁
        Destroy(recipeTransfomrDict[recipeName].Dequeue().gameObject); 
    }

    /// <summary>
    /// 生成订单UI
    /// </summary>
    /// <param name="recipeSO">订单</param>
    /// <returns>UI的Transform</returns>
    private Transform SpawnRecipeUI(RecipeSO recipeSO)
    {
        Transform transform = Instantiate(recipeTemplate, container); // 生成UI
        transform.GetComponent<DeliveryManagerSingleRecipeUI>().SetRecipeIcons(recipeSO); // 设置订单名以及图标
        transform.gameObject.SetActive(true); // 显示UI
        return transform;
    }
}
