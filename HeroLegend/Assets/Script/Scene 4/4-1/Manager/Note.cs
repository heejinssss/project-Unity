using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float noteSpeed = 800;

    UnityEngine.UI.Image noteImage;

    // ��ü�� Ȱ��ȭ �� ������ ȣ��Ǵ� �Լ�
    private void OnEnable()
    {
        // �̹��� ������Ʈ�� ��� ��������, �� �� ���� �ൿ�� �ݺ��ϴ°�
        // null���϶��� �������� �����
        if (noteImage == null)
            noteImage = GetComponent<UnityEngine.UI.Image>();

        // ť�� ���ٺ��� ������ �����̽� ���缭 �Ʒ��� false �Ǿ� �ִ� ��� �����ϱ�
        noteImage.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += Vector3.right * noteSpeed * Time.deltaTime;
    }

    public void HideNote()
    {
        noteImage.enabled = false;
    }

    public bool GetNoteFlag()
    {
        return noteImage.enabled;
    }
}
