using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : FindGM
{
    [Header("剩余目标数量")]
    public int _TargetCount;
    public int[] time =new int[3]  ; //分钟，秒钟，毫秒
    [HideInInspector]public float _NowTime;

    public bool Gift;

    private void Update()
    {
        //时间的计算
        _NowTime +=Time.deltaTime;
        time = TimeToString(_NowTime);


        //重置时间，并重开
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

    //到达目标地点，需要判定是否完成任务数量
    public void GameCompleted()
    {
        if (_TargetCount > 0) return;
        //比较存储的最快数据
        var x = PlayerPrefs.GetFloat("fasttime");
        print("以往最快值为" + x);
        if (x==0 || _NowTime < x)
        {
            PlayerPrefs.SetFloat("fasttime", _NowTime);
            print("新最快速度已录入"+_NowTime);
        }


        _UIM.CompletedUI();

    }

    public void GameOver()
    {
        _UIM.GameOverUI();
    }

    public void GetGift()
    {
        //获得草莓
        Gift = true;
        _UIM.GiftUI();
    }

}
