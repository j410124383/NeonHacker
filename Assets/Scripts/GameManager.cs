using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : FindGM
{
    [Header("ʣ��Ŀ������")]
    public int _TargetCount;
    public int[] time = new int[3]; //���ӣ����ӣ�����
    [HideInInspector]public float _NowTime;



    public string TimeToString(int result)
    {
        int hour = (int)result / 3600;
        int minute = ((int)result - hour * 3600) / 60;
        int second = (int)result - hour * 3600 - minute * 60;
        string data = string.Format("{0:D2}:{1:D2}:{2:D2}", hour, minute, second);
        return data;
    }

    private void Update()
    {
        //ʱ��ļ���
        _NowTime +=Time.deltaTime;
        time[0] = (int)Mathf.Floor(_NowTime/ 60f);
        time[1] = (int)Mathf.Floor(_NowTime-time[0]*60f) ;
        time[2] = (int)Mathf.Floor((_NowTime - (int)_NowTime) * 100);


        //����ʱ�䣬���ؿ�
        if (Input.GetKeyDown(KeyCode.F))
        {
            Restart();
        }


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
        _UIM.CompletedUI();

    }

    public void GameOver()
    {
        _UIM.GameOverUI();
    }


}
