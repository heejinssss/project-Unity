using I18N.Common;
using System.Collections;
using UnityEngine;

public class PlayerAction5 : MonoBehaviour
{
    // 싱글톤 인스턴스 추가
    public static PlayerAction5 Instance { get; private set; }
    public GameManager5 manager;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Awake()
    {
        // 싱글톤 인스턴스 초기화
        Instance = this;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !manager.isDialogueEnded) // 대화가 끝나지 않았을 때만 Action 실행
            manager.Action();
    }

    // 10초간 특별 능력 활성화
    public void EnableSpecialAbilityFor10Seconds()
    {
        AttackonTitan();
        StartCoroutine(ResetAbilitiesAfter10Seconds());
    }

    private IEnumerator ResetAbilitiesAfter10Seconds()
    {
        yield return new WaitForSeconds(10);
        ThreeMatchProcess();
    }

    void AttackonTitan()
    {
        anim.SetBool("isCompleted", true);
        anim.SetBool("isSkilled", true);
    }
    void ThreeMatchProcess()
    {
        anim.SetBool("isCompleted", false);
        anim.SetBool("isSkilled", false);
    }
}
