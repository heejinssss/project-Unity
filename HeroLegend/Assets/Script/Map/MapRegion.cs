using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapRegion : MonoBehaviour
{
    public Button[] regions;
    int[] clearStatus = new int[5];

    void Start()
    {
        // Ŭ���� ���� �ҷ�����
        GetClearStatus();

        bool didClearAll = true;
        int numOfRegions = regions.Length;

        for (int i = 0; i < numOfRegions; i++)
        {
            // RGBA���� Alpha ���� 0.5�̻��� �κи� Ŭ�� ������ �ǵ��� ����
            regions[i].GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;

            // ������ ����(����)�� �ƴϰ�, Ŭ�������� ���, �����Ǿ� �ִ� �ʷϻ� Disabled Color�� �ߵ���
            // + �ٽ� �� ������ ������ �� ����
            if (i < numOfRegions - 1)
            {
                if (clearStatus[i] == 1)
                {
                    regions[i].interactable = false;
                }
                else
                {
                    // �ϳ��� �� �� ���������� �ִ� ���
                    didClearAll = false;
                }
            }
        }

        // ������ ������ ���� 4 ���������� ��� Ŭ���� �ؾ� ��������
        if (didClearAll)
        {
            regions[numOfRegions - 1].interactable = true;
        }
        else
        {
            regions[numOfRegions - 1].interactable = false;
        }
    }

    void GetClearStatus()
    {
        // Ŭ���� �ߴ��� ���� �ҷ�����
        // �� �κ��� ������ �ٲ��� �մϴ�
        clearStatus[0] = 0;
        clearStatus[1] = 0;
        clearStatus[2] = 0;
        clearStatus[3] = 0;
        clearStatus[4] = 0;
    }

    public void MoveScene(string sceneName)
    {
        // �ʿ��ϴٸ� ��ũ��Ʈ �߰� ����
        SceneManager.LoadScene(sceneName);
    }

    // regions[i].interactable = false; �� ��ư�� disabled ���·� ���� �� �ִ� => Disabled Color�� ����
}
