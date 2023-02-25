using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : FindGM
{
    [Header("剩余目标数量")]
    public int _TargetCount;
    public int[] time = new int[3]; //分钟，秒钟，毫秒
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
        //时间的计算
        _NowTime +=Time.deltaTime;
        time[0] = (int)Mathf.Floor(_NowTime/ 60f);
        time[1] = (int)Mathf.Floor(_NowTime-time[0]*60f) ;
        time[2] = (int)Mathf.Floor((_NowTime - (int)_NowTime) * 100);


        //重置时间，并重开
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

    //到达目标地点，需要判定是否完成任务数量
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
