using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class S_E_Shooter : FindGM
{
    //�����Χ���м�⵽�����ʼ����


    public bool _IsOnWarning;
    public float Aimingtime;
    public Transform _ShootTrans;
    [Header("���ؼ���")]
    public LayerMask GroundLayerMask; //������νӴ���ļ���
    [Header("����UI")]
    public Image fill;
    public Vector2 target;

    private EnemyDisplay e;
    protected override void Awake()
    {
        base.Awake();
        e = GetComponent<EnemyDisplay>();
    }


    private void FixedUpdate()
    {
        _IsOnWarning = OnWarning();

        if (_IsOnWarning)
        {
            //��ʱ���������
            Aimingtime+=Time.timeScale;
            _ShootTrans.transform.LookAt(_P.transform.position);
            _ShootTrans.transform.Rotate(new Vector3(0, -90, 0));

        }
        else if(!_IsOnWarning&&Aimingtime>0)
        {
            Aimingtime -= Time.timeScale;
        }


        if (Aimingtime>=e._EnemyType.AimingTime)
        {

            Shoot();
       
        }
        fill.fillAmount = Aimingtime / e._EnemyType.AimingTime;
    }
    private bool OnWarning()
    {
     
        //�����ж���
        //�������������ġ���С���Ƕȡ���ɸѡ��
        Collider2D Coll = Physics2D.OverlapCircle(transform.position, e._EnemyType.AimingRange, GroundLayerMask);
        if (Coll != null)
        {
            return true;
        }
        return false;
    }

    public void Shoot()
    {
        target = _P.transform.position;
        target -= (Vector2)_ShootTrans.GetChild(0).position;
        target.Normalize();

        var bullet = Instantiate(e._EnemyType.BulletObj, _ShootTrans.GetChild(0));
        bullet.transform.SetParent(_GM.transform);
        var b = bullet.GetComponent<Bullet>();
        //��������
        b.bulletType = BulletType.�з�;
        b.FlySpeed = e._EnemyType.BulletSpeed;
        b.Damage = e._EnemyType.BulletDanmage;
        //b.transform.GetChild(0).GetComponent<SpriteRenderer>().color = e._EnemyType.guntype.GunColor;
        b.LiveTime = e._EnemyType.BulletLiveTime;
        b.target = target;
        Aimingtime = 0;

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, GetComponent<EnemyDisplay>()._EnemyType.AimingRange);


    }

}
