using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : FindGM
{
    public TMP_Text _Timetext;

    public TMP_Text _Textname;
    public Image _healthImage;

    public TMP_Text _DemonCounttext;

    [Header("角色信息")]
    public TMP_Text _PlayerStateText;

    [Header("武器可视化信息")]
    public TMP_Text _WeaponListText;
    public Image _ClipImage;
    public TMP_Text _WeaponText;


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
        _ClipImage.fillAmount = (float)_SC.weapon.clipCount / (float)_SC.weapon.guntype.ClipCount;

        //显示所持武器
        if (_SC._WeaponList.Count==0)
        {
            _WeaponText.text = "null Weapon";
            return;
        } 
        _WeaponText.text = string.Format("==Hold Weapon==\n[Type]  {0:N}\n[Skill]  {1:N}\n[Clip]   {2:N}/{3:N}",
            _SC.weapon.guntype.GunName,
            _SC.weapon.guntype.SkillName,
            _SC.weapon.clipCount.ToString(),
            _SC.weapon.guntype.ClipCount.ToString());
    }

    public void CompletedUI()
    {
        Time.timeScale = 0;
        var p = Resources.Load("Prefabs/UI/Panels/Panel_Completed") as GameObject;
        var panel_c = Instantiate(p, transform);
        panel_c.transform.SetParent(transform);
        panel_c.transform.GetChild(0).GetComponent<TMP_Text>().text = _Timetext.text;
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
        _healthImage.fillAmount = _health / _maxhealth;
        _Textname.text = _name;
    }




}
