using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindGM : MonoBehaviour
{

    protected GameManager _GM;
    protected UIManager _UIM;
    protected GameObject _P;
    protected PlayerController _PC;
    protected PlayerState _PS;
    protected ShootController _SC;
    protected SkillManager _SM;
    protected GameObject _CM;

    protected virtual void Awake()
    {

       _GM = GameObject.FindWithTag("GM").GetComponent<GameManager>();
        _UIM = GameObject.FindWithTag("UIM").GetComponent<UIManager>();
        _P = GameObject.FindWithTag("Player");
        _SC = _P.GetComponent<ShootController>();
        _PC = _P.GetComponent<PlayerController>();
        _SM = _P.GetComponent<SkillManager>();
        _PS = _P.GetComponent<PlayerState>();
        _CM = GameObject.FindWithTag("CM");
    }



}
