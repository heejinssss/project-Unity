using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Reposition : MonoBehaviour
{
    public UnityEvent onMove;

    void LateUpdate() // Update, FIxedUpdate ���� ���� -> ��ó��
    {
        // position : ���� ��ǥ, localPosition : ��� ��ǥ
        if (transform.position.x > -25.5)
            return;

        // �ǵ��ư���
        transform.Translate(49.5f, 0, 0, Space.Self); // Space.Self -> ��� ��ǥ
        onMove.Invoke();
    }
}

