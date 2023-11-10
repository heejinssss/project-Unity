using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAnim1 : MonoBehaviour
{
    private Animator animator;
    private string[] attacks = { "skill_1", "skill_2", "skill_3", "evade_1" };
    float timer;
    float attackInterval = 5.0f;


    void Start()
    {
        animator = this.GetComponent<Animator> ();

    }

    // Update is called once per frame
    void Update()
    {
       timer += Time.deltaTime;
        // level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 100f), spawnData.Length - 1);
        if (timer >= attackInterval)
        {
            timer = 0;
            int randomAttackIndex = Random.Range(0, attacks.Length);
            Attack(randomAttackIndex);
        }
        // if (this.health < maxhealth){} 
    }
    void Attack(int randomAttackIndex)
    {
        // int randomAttackIndex = Random.Range(0, attacks.Length);
        if (attacks[randomAttackIndex] == "skill_1")
        {
            animator.SetTrigger("skill_1");
        }
        if (attacks[randomAttackIndex] == "skill_2")
        {
            animator.SetTrigger("skill_2");
        }
        if (attacks[randomAttackIndex] == "skill_3")
        {
            animator.SetTrigger("skill_3");
        }
        if (attacks[randomAttackIndex] == "evade_1")
        {
            animator.SetTrigger("evade_1");
        }
    
    }
}
