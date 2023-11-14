using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastBackground4 : MonoBehaviour
{
    Animator pastBackgroundAnim;

    private void Awake()
    {
        pastBackgroundAnim = GetComponent<Animator>();
    }

    public void PastBackgroun4FadeIn()
    {
        pastBackgroundAnim.SetTrigger("FadeIn");
    }

    public void PastBackgroun4FadeOut()
    {
        pastBackgroundAnim.SetTrigger("FadeOut");
    }
}
