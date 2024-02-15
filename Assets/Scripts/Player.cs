using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;

    private void Update()
    {
        Vector2 inputVector = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y -= 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y += 1;
        }

        // 输入向量归一化
        inputVector = inputVector.normalized;

        Vector3 moveVector = new Vector3(inputVector.x, 0, inputVector.y);
        // 注意运算顺序
        transform.position += moveSpeed * Time.deltaTime * moveVector;
    }
}
