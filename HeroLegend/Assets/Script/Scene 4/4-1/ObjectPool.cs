using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectInfo
{
    public GameObject goPrefab;
    public int count;
    public Transform tfPoolParent;

}

public class ObjectPool : MonoBehaviour
{
    [SerializeField] ObjectInfo[] objectInfo = null;

    // ObjectPool�� ���� ��𼭵� ���� ������ ���� �ݳ��� ���� �־����
    // ���� �̸� ���� �ڿ����� ������
    // �����ڿ� instance�� ���� ��𼭵� public ����, �Լ��� ������ ����!
    // ObjectPool.instance�� public�̶�� �� �͵� ���� �� �� �ִ�
    public static ObjectPool instance;

    // noteQueue�� �ϳ��� Pool�� �ɰ�
    // ���⼭ ���� ��Ʈ�� ������ ���°�
    public Queue<GameObject> noteQueue = new Queue<GameObject>();

    void Start()
    {
        // �� ���·θ� �θ� ���� ���� ���´ϱ�, �ڱ� �ڽ����� ����
        instance = this;

        // ���Ͻ�Ų ���� noteQueue�� �־��ٰ�
        // noteQueue�� �� ������Ʈ�� �迭 ù��°�� �־�����
        noteQueue = InsertQueue(objectInfo[0]);

        // ���� �ı��� ���� �̷����� ��ü�� �ִ�?
        // => �׷��� ù��° �迭, �ι�° �迭�� �� ���� ���� �ٸ� ť�� �־��ָ� �ȴ�
    }
    
    Queue<GameObject> InsertQueue(ObjectInfo p_objectInfo)
    {
        Queue<GameObject> t_queue = new Queue<GameObject>();
        for (int i = 0; i < p_objectInfo.count; i++)
        {
            // ��Ʈ��(��������) �������������
            // ������ ��ġ�� �־��ָ� �ɵ�, ������ ��Ȱ��ȭ ��ų�Ŷ� ���ӿ��� ������ ������
            GameObject t_clone = Instantiate(p_objectInfo.goPrefab, transform.position, Quaternion.identity);
            t_clone.SetActive(false); // ���������� �ٷ� ��Ȱ��ȭ

            // �θ� �������ٰ�
            // ������ �����ߴ� ��Ʈ ���� ��쿡�� ��Ʈ �Ŵ��� ��ũ��Ʈ�� �پ��ִ� ��ü�� �θ𿴴�
            // �׷��� �θ� ��ü�� �����Ѵٸ�, �� ��ü�� �θ�� �������
            if (p_objectInfo.tfPoolParent != null)
                t_clone.transform.SetParent(p_objectInfo.tfPoolParent);
            else
                t_clone.transform.SetParent(this.transform); // �θ� ��ü�� null ���̶��, �� ��ũ��Ʈ�� �پ��ִ� ��ü�� �θ��

            // �ݺ����� ������ ť���� ī��Ʈ ���� ��ŭ�� ��ü�� ���������
            t_queue.Enqueue(t_clone);
        }
        return t_queue;
    }
}
