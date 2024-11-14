using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Animator anima;

    private void Awake()
    {
        anima = GetComponent<Animator>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            anima.SetBool("IsMove",!anima.GetBool("IsMove"));
        }
    }

}
