using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int stageIndex;
    public int health;
    public int bossHealth;
    public int enemyNum;
    public bool gameClear;
    
    public PlayerDamaged player;
    public BossMove boss;
    public GameObject[] Stages;

    public Image[] UIhealth;
    public Image[] BossHealth;
    public GameObject RestartButton;
    public GameObject NextButton;

    
    public void BossHealthDown()
    {
        // ���� �ǰ�
        bossHealth--;
        if (bossHealth >= 0) BossHealth[bossHealth].enabled = false;

        // ���� ��� 
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

        // �������� ���� 
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
            Debug.Log("���� Ŭ����");
            gameClear = true;
            RestartButton.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HealthDown();

            // �÷��̾� ����ġ
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
            // �÷��̾� ��� 
            player.OnDie();
            // R
            Debug.Log("�׾����ϴ�.");
            // ����� ��ư Ȱ��ȭ
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

    // �÷��̾� ����ġ 
    void PlayerReposition()
    {
        player.transform.position = new Vector3(8.5f, 2, 0);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    // ó������ �����
    public void Restart()
    {
        if (gameClear)
        {
            Debug.Log("���� Ŭ����");
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
