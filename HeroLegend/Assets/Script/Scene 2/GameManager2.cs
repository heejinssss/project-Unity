using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{
    public int stageIndex;
    public int health;
    public int bossHealth;
    public int enemyNum;
    public bool gameClear;
    
    public PlayerDamaged2 player;
    public BossMove2 boss;
    public GameObject[] Stages;

    public Image[] UIhealth;
    public Image[] BossHealth;
    public GameObject RestartButton;
    public GameObject NextButton;

    
    public void BossHealthDown()
    {
        // 보스 피격
        bossHealth--;
        if (bossHealth >= 0) BossHealth[bossHealth].enabled = false;

        // 보스 사망 
        if (bossHealth <= 0)
        {
            boss.OnDie();
            NextStage();
        }
    }

    public void EnemyCountDown()
    {
        if (stageIndex < Stages.Length)
        {
            enemyNum--;
            
            if (enemyNum <= 0)
            {
                if (stageIndex < Stages.Length - 1) enemyNum = 6;
                NextButton.SetActive(true);
            }
        }
    }

    public void NextStage()
    {

        // 스테이지 변경 
        if (stageIndex < Stages.Length - 1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();
        }
        else
        {
            // Time.timeScale = 0;
            Debug.Log("게임 클리어");
            gameClear = true;
            RestartButton.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HealthDown();

            // 플레이어 원위치
            if (health >= 1)
            {
                PlayerReposition() ;
            }
        }
    }

    public void HealthDown()
    {
        health--;
        Debug.Log(health);
        if (health >= 0) UIhealth[health].color = new Color(1, 1, 1, 0.2f);
        
        if (health <= 0)
        {
            UIhealth[health].color = new Color(1, 1, 1, 0.2f);
            // 플레이어 사망 
            player.OnDie();
            // R
            Debug.Log("죽었습니다.");
            // 재시작 버튼 활성화
            RestartButton.SetActive(true);
        }
    }

    public void HealthUp()
    {
        if (health < 3)
        {
            UIhealth[health].color = new Color(1, 1, 1, 1);
            health++;
        }
    }

    // 플레이어 원위치 
    void PlayerReposition()
    {
        player.transform.position = new Vector3(8.5f, 2, 0);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    // 처음부터 재시작
    public void Restart()
    {
        if (gameClear)
        {
            Debug.Log("게임 클리어");
            SceneManager.LoadScene("Scene 2");
        } else
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Scene 2");
        }
    }

    public void GoNext()
    {
        NextStage();
        NextButton.SetActive(false);
    }
}
