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

    [Header("��ɫ��Ϣ")]
    public TMP_Text _PlayerStateText;
    public Image _HealthImage;

    [Header("�������ӻ���Ϣ")]
    public TMP_Text _WeaponListText;
    public Image _ClipImage;
    public TMP_Text _WeaponText;

    [Header("������ʾ��Ϣ")]
    public Image _GiftImage;

    protected override void Awake()
    {
        base.Awake();
    }

    void Update()
    {
        _Timetext.text= string.Format("{0:D2}:{1:D2}:{2:D2}", _GM.time[0], _GM.time[1], _GM.time[2]);
        _DemonCounttext.text = _GM._TargetCount.ToString();

        //��ʾ��ǰ��ɫ״̬
        var h= _PS.Health.ToString();
        _PlayerStateText.text = "==Player State==\n[Health]  " + h;
        _HealthImage.fillAmount = _PS.Health / 3f;

        //��ʾ��ǰ������
        var x = "==WeaponCard==\n";
        for (int i = 0; i < _SC._WeaponList.Count; i++)
        {
            var v = "";
            v = string.Format("[{0:N}] [Type]{1:N}   [Count]{2:N}\n",
                i.ToString(), 
                _SC._WeaponList[i].guntype.GunName,
                _SC._WeaponList[i].gunCount.ToString()
                );
            //��Ϊ��ǰʹ�������������UI
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

        //��ʾ��������
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
