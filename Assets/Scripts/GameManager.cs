using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : FindGM
{
    [Header("ʣ��Ŀ������")]
    public int _TargetCount;
    public int[] time =new int[3]  ; //���ӣ����ӣ�����
    [HideInInspector]public float _NowTime;

    public bool Gift;

    private void Update()
    {
        //ʱ��ļ���
        _NowTime +=Time.deltaTime;
        time = TimeToString(_NowTime);


        //����ʱ�䣬���ؿ�
        if (Input.GetKeyDown(KeyCode.F))
        {
            Restart();
        }


    }
    public int[] TimeToString(float t)
    {
        int[] x = new int[3];
        x[0]= (int)Mathf.Floor(t / 60f);
        x[1]=(int)Mathf.Floor(t - x[0] * 60f);
        x[2]=(int)Mathf.Floor((t - (int)t) * 100);

        return x;
    }
    public void Restart()
    {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //����Ŀ��ص㣬��Ҫ�ж��Ƿ������������
    public void GameCompleted()
    {
        if (_TargetCount > 0) return;
        //�Ƚϴ洢���������
        var x = PlayerPrefs.GetFloat("fasttime");
        print("�������ֵΪ" + x);
        if (x==0 || _NowTime < x)
        {
            PlayerPrefs.SetFloat("fasttime", _NowTime);
            print("������ٶ���¼��"+_NowTime);
        }


        _UIM.CompletedUI();

    }

    public void GameOver()
    {
        _UIM.GameOverUI();
    }

    public void GetGift()
    {
        //��ò�ݮ
        Gift = true;
        _UIM.GiftUI();
    }

}
