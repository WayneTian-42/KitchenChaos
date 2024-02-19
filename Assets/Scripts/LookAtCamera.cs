using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    // 物体朝向摄像头的方式
    [SerializeField] private ShowMode showMode;
    /// <summary>
    /// 物体朝向摄像头的方式
    /// </summary>
    private enum ShowMode
    {
        LookAt, // 看向摄像头
        LookAtInverted, // 沿着摄像头朝前看
        CameraForward, // 和摄像头朝向一直
        CameraForwardInverted // 和摄像头朝向相反
    }

    private void LateUpdate()
    {
        switch (showMode)
        {
            case ShowMode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case ShowMode.LookAtInverted:
                transform.LookAt(transform.position - Camera.main.transform.position);
                break;
            case ShowMode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case ShowMode.CameraForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}
