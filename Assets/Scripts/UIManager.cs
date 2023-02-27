using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UIManager : FindGM
{
    public TMP_Text _Timetext;

    public TMP_Text _Textname;

    public GameObject _BarEnemyState;
    public TMP_Text _DemonCounttext;

    [Header("角色信息")]
    public TMP_Text _PlayerStateText;
    public Image _HealthImage;

    [Header("武器可视化信息")]
    public TMP_Text _WeaponListText;
    public Image _ClipImage;
    public Image _WeaponIcon;
    public Image _WeaponCount;
    public TMP_Text _WeaponText;

    [Header("其余显示信息")]
    public Image _GiftImage;

    protected override void Awake()
    {
        base.Awake();
    }

    void Update()
    {
        _Timetext.text= string.Format("{0:D2}:{1:D2}:{2:D2}", _GM.time[0], _GM.time[1], _GM.time[2]);
        _DemonCounttext.text = _GM._TargetCount.ToString();

        //显示当前角色状态
        var h= _PS.Health.ToString();
        _PlayerStateText.text = "==Player State==\n[Health]  " + h;
        _HealthImage.fillAmount = _PS.Health / 3f;

        //显示当前武器卡
        var x = "==WeaponCard==\n";
        for (int i = 0; i < _SC._WeaponList.Count; i++)
        {
            var v = "";
            v = string.Format("[{0:N}] [Type]{1:N}   [Count]{2:N}\n",
                i.ToString(), 
                _SC._WeaponList[i].guntype.GunName,
                _SC._WeaponList[i].gunCount.ToString()
                );
            //若为当前使用武器，则高亮UI
            if (_SC._WeaponList[i].guntype.GunName==_SC.weapon.guntype.GunName)
            {
                v = string.Format("<color=yellow>{0:N}<color=yellow>", v);
            }
            else
            {
                v = string.Format("<color=white>{0:N}<color=white>", v);
            }
            x += v;

        }
        _WeaponListText.text = x;


        //显示所持武器
        var c = _SC.weapon.guntype;
        _ClipImage.fillAmount =1-((float)_SC.weapon.clipCount / (float)c.ClipCount);
        if (_SC._WeaponList.Count==0)
        {
            _WeaponText.text = "null Weapon";
            return;
        } 
        _WeaponText.text = string.Format("[Type]  {0:N}\n[Skill]  {1:N}\n[Clip]   {2:N}/{3:N}",
            c.GunName,
            c.SkillName,
            _SC.weapon.clipCount.ToString(),
            c.ClipCount.ToString());
        _WeaponIcon.sprite = _SC.weapon.guntype.GunIcon;
        _WeaponCount.color = _SC.weapon.guntype.GunColor;
        _WeaponCount.fillAmount = _SC.weapon.gunCount / 3f;

    }

    public void CompletedUI()
    {
        Time.timeScale = 0;
        var p = Resources.Load("Prefabs/UI/Panels/Panel_Completed") as GameObject;
        var panel_c = Instantiate(p, transform);
        panel_c.transform.SetParent(transform);
        panel_c.transform.GetChild(0).GetComponent<TMP_Text>().text = _Timetext.text;
        if (_GM.Gift)
        {
            panel_c.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void GameOverUI()
    {
        Time.timeScale = 0;
        var p = Resources.Load("Prefabs/UI/Panels/Panel_GameOver") as GameObject;
        var panel_c = Instantiate(p, transform);
        panel_c.transform.SetParent(transform);
    }


    public void EnemyStateDisplay(string _name,float _health,float _maxhealth)
    {
        if (_health <= 0) return;
        var a = _BarEnemyState.GetComponent<Animator>();
        if (a.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            a.SetTrigger("ISACTIVE");
        }

        _BarEnemyState.transform.GetChild(0).GetComponent<Image>().fillAmount = _health / _maxhealth;
        _BarEnemyState.transform.GetChild(1).GetComponent<TMP_Text>().text = _name;
    }


    public void GiftUI()
    {
        _GiftImage.gameObject.SetActive(true);
    }

}
