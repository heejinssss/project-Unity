using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegBossShape : MonoBehaviour
{

    SpriteRenderer spriteRenderer;

    public Sprite clearSprite;

    public void ChangeShapeWhenClear()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = clearSprite;
    }
}
