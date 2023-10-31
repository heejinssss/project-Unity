using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class Follow : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 13;
    private bool isMoving = false; 
    public float startDelay = 3f;
    private Rigidbody2D enemy;
    private Vector2 movement;
    public int Speed;
    // private GameObject dust;
    

    void Start()
    {
        enemy = this.GetComponent<Rigidbody2D>();
        StartCoroutine(StartMoving());
        // dust = enemy.GetComponentInChildren<GameObject>();
    }

    IEnumerator StartMoving()
    {
        yield return new WaitForSeconds(startDelay); // 처음 3초 대기
        isMoving = true;
        
    }

    void Update()
    {

        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan(direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        movement = direction;
                
        // enemy.SetDestination(GameManager.instance.player.transform.position);
    }
    void FixedUpdate()
    {
        if (isMoving)
        {
            moveCharacter(movement);
            Animator enemyAnimator = enemy.GetComponent<Animator>();
            enemyAnimator.SetTrigger("run");
            // enemy.dust.GameObject.SetActive(true);
        }
    }

    void moveCharacter(Vector2 direction)
    {
        enemy.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

}
