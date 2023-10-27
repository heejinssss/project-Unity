using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShakeCamera : MonoBehaviour
{
    public static ShakeCamera Instance;

    private Animator CameraShakeAnim;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        CameraShakeAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CameraShake()
    {
       CameraShakeAnim.SetTrigger("Shake");
    }

    public void ShakeCameraOne()
    {
        CameraShakeAnim.SetTrigger("Shake_1");
    }
    public void ShakeCameraTwo()
    {
        CameraShakeAnim.SetTrigger("Shake_2");
    }
    public void ShakeCameraThree()
    {
        CameraShakeAnim.SetTrigger("Shake_3");
    }

}