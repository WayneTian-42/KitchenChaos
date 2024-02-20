using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    /// <summary>
    /// 盘子柜台
    /// </summary>
    [SerializeField] private PlatesCounter platesCounter;
    /// <summary>
    /// 盘子预制体
    /// </summary>
    [SerializeField] private Transform platePrefab;
    /// <summary>
    /// 柜台顶点
    /// </summary>
    [SerializeField] private Transform counterTopPoint;
    /// <summary>
    /// 柜台已生成的盘子列表
    /// </summary>
    private List<GameObject> paltesGameObjectList;

    private void Awake()
    {
        paltesGameObjectList = new List<GameObject>();

        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    /// <summary>
    /// 生成盘子动画
    /// </summary>
    /// <param name="sender">事件发布者：盘子柜台</param>
    /// <param name="e">事件参数</param>
    private void PlatesCounter_OnPlateSpawned(object sender, EventArgs e)
    {
        // 给新盘子添加一点y轴偏移
        int platesNum = paltesGameObjectList.Count;
        float plateOffsetY = 0.1f;
        // 生成一个盘子
        Transform platesTranform = Instantiate(platePrefab, counterTopPoint);
        platesTranform.localPosition = new Vector3(0, plateOffsetY * platesNum, 0);
        // 加入盘子列表中
        paltesGameObjectList.Add(platesTranform.gameObject);
    }

    /// <summary>
    /// 移除盘子动画
    /// </summary>
    /// <param name="sender">事件发布者：盘子柜台</param>
    /// <param name="e">事件参数</param>
    private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e)
    {
        // 获取最后一个盘子
        GameObject topPlate = paltesGameObjectList[^1];
        // 移除最后一个盘子
        paltesGameObjectList.RemoveAt(paltesGameObjectList.Count - 1);
        Destroy(topPlate);
    }
}
