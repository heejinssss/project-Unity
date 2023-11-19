using UnityEngine;

//Just for demonstration, you can replace it with your own code logic.
public class AnimationEvent1 : MonoBehaviour
{

    public GameObject enemy;
    private bool isMoving;

    private int atkTimes = 0;

    void Start()
    {
        Animator enemyAnimator = enemy.GetComponent<Animator>();

        isMoving = false; // 처음에 이동하지 않음
        enemyAnimator = enemy.GetComponent<Animator>();

        // 처음 시작할 때 idle_2와 skill_4 애니메이션 실행
        // enemyAnimator.SetTrigger("idle_2");
        // enemyAnimator.SetTrigger("skill_4");
    }

    public void AttackStart()
    {
        Debug.Log("Attack Start");

        //Just for demonstration, you can replace it with your own code logic.
        atkTimes++;
        if (enemy && atkTimes <= 3)
        {
            Animator enemyAnimator = enemy.GetComponent<Animator>();
            if (atkTimes == 1)
            {
                // enemyAnimator.SetTrigger ("hit_1");
            }
            else if (atkTimes == 2)
            {
                // enemyAnimator.SetTrigger ("hit_2");
            }
            else if (atkTimes == 3)
            {
                // enemyAnimator.SetTrigger ("hit_2");
                // enemyAnimator.SetTrigger ("death");
            }
        }
    }

    public void AttackStartEffectObject()
    {
        Debug.Log("Fire Effect Object");
    }
}
