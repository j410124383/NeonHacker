using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{

    public static PlayerState instance;

    public int Health=3;


    private GameManager _GM;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _GM = GameManager.instance;
    }


    private void Update()
    {
        if (Health<=0)
        {
            GetComponent<Animator>().SetBool("IsDie",true);
            _GM.GameOver();
        }
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Target")
        {
            //print("Completed");
            _GM.GameCompleted();
        }
        else if (col.gameObject.tag == "Prop")
        {
            var g = col.gameObject.GetComponent<Prop>().Fallgun;
            ShootController.instance.GunAdd(g);
            Destroy(col.gameObject);
        }
        else if (col.gameObject.tag == "Gift")
        {
            _GM.GetGift();
            Destroy(col.gameObject);
        }
    }






}
