using UnityEngine;
using UnityEngine.UI;

public class InputName : MonoBehaviour
{
    /* UI 구성요소 */
    public GameObject NameView;
    public InputField InputField_Name;

    /* 닉네임 입력 버튼 클릭 이벤트 */
    public void NameButtonClick()
    {
        if (string.IsNullOrEmpty(InputField_Name.text)) return;

        string name = InputField_Name.text;
        Debug.Log("닉네임 입력 버튼 클릭 이벤트 발생 :: " + name);

        /* DB 저장 */
        if (DBManager.Instance.InputNickname(name))
        {
            NameView.SetActive(false);

            /*
             * TO DO :: SWITCH SCENE
             */

        }
        else
        {
            /*
             * TO DO :: "다시 시도해주세요" ALERT
             */
        }
    }

    /* 취소 버튼 클릭 이벤트 */
    public void CancelButtonClick()
    {
        InputField_Name.text = "";
    }
}