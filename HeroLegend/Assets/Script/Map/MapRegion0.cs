using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapRegion0 : MonoBehaviour
{
    public Button[] regions;
    int[] clearStatus = new int[5];

    AudioSource regionClickAudioSource;

    private void Awake()
    {
        regionClickAudioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        // 클리어 정보 불러오기
        GetClearStatus();

        bool didClearAll = true;
        int numOfRegions = regions.Length;

        for (int i = 0; i < numOfRegions; i++)
        {
            // RGBA에서 Alpha 값이 0.5이상인 부분만 클릭 영역이 되도록 설정
            regions[i].GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;

            // 마지막 지역(보스)이 아니고, 클리어했을 경우, 설정되어 있는 초록색 Disabled Color가 뜨도록
            // + 다시 그 지역을 선택할 수 없게
            if (i < numOfRegions - 1)
            {
                if (clearStatus[i] == 1)
                {
                    regions[i].interactable = false;
                }
                else
                {
                    // 하나라도 안 깬 스테이지가 있는 경우
                    didClearAll = false;
                }
            }
        }

        // 마지막 보스는 이전 4 스테이지를 모두 클리어 해야 열리도록
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
        // 클리어 했는지 여부 불러오기
        // 이 부분이 적절히 바뀌어야 합니다
        clearStatus[0] = 0;
        clearStatus[1] = 0;
        clearStatus[2] = 0;
        clearStatus[3] = 0;
        clearStatus[4] = 0;
    }

    public void MoveScene(string sceneName)
    {
        // 필요하다면 스크립트 추가 예정
        regionClickAudioSource.Play();
        SceneManager.LoadScene(sceneName);
    }

    // regions[i].interactable = false; 로 버튼을 disabled 상태로 만들 수 있다 => Disabled Color로 나옴
}
