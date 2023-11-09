using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPlayer : MonoBehaviour
{
    public GameManager gameManager;


    void Update()
    {
        if (Input.GetButtonDown("Jump"))
            gameManager.Action();
    }
}
