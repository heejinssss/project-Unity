using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    // ��ų ������
    public GameObject water; 
    public GameObject fire;
    public GameObject effect;
    public GameManager gameManager;

    Animator anim;

    public float launchSpeed = 10.0f; // ��ų ������ �ӵ� 
    public float delay = 0.5f;     // ��Ÿ ������ �ð� (��)

    private bool isCooldown = false;

    private void Start()
    {
        Transform playerTransform = this.transform;
        anim = playerTransform.Find("UnitRoot").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isCooldown)
        {
            CreateProjectile(fire);
            StartCoroutine(Cooldown()); // ��Ÿ ������ ����
            
        } else if(Input.GetKeyDown(KeyCode.X) && !isCooldown) {
            CreateProjectile(water);
            StartCoroutine(Cooldown()); // ��Ÿ ������ ����
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            Quaternion rotation = Quaternion.Euler(0, 0, 90);
            GameObject effect1 = Instantiate(effect, collision.gameObject.transform.position, rotation);
            StartCoroutine(DeleteEffect(effect1));
            Rigidbody2D objectRb = collision.gameObject.GetComponent<Rigidbody2D>();
            objectRb.AddForce(-transform.right * launchSpeed, ForceMode2D.Impulse);
        } else if (collision.gameObject.CompareTag("Item"))
        {
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);
            gameManager.HealthUp();
        }
    }

    IEnumerator DeleteEffect(GameObject gameObject)
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void CreateProjectile(GameObject projectilePrefab)
    {
        anim.SetTrigger("Skill");
        // ������Ÿ���� ���� ��ġ�� �÷��̾� ������ ���� �ű�
        Vector3 startPosition = transform.position - transform.right * 0.7f + transform.up * 0.5f;

        // �������κ��� ���ο� ������Ÿ�� ������Ʈ ����
        GameObject projectile = Instantiate(projectilePrefab, startPosition, transform.rotation);

        // ������Ÿ�Ϸκ��� ������ٵ� 2D ������Ʈ ������
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // ������Ÿ�� �߻�
        rb.AddForce(-transform.right * launchSpeed, ForceMode2D.Impulse);
    }

    IEnumerator Cooldown()
    {
        isCooldown = true; // ��Ÿ ������ ���� ��ٿ� Ȱ��ȭ
        yield return new WaitForSeconds(delay); // ��ٿ� �ð� ���� ��� (0.5�ʷ� ����)
        isCooldown = false; // ��ٿ� ��Ȱ��ȭ
    }
}
