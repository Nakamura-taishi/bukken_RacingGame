using Microsoft.Win32.SafeHandles;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class t_TimeCounter : MonoBehaviour
{
    string zero = "";
    int minutes = 0;
    float seconds = 0f;
    public TextMeshProUGUI timetext;//時間を表示するテキストボックス
    public string display_text;
    public static int finished_minutes;
    public static float finished_seconds;
    public static string GoalTime;
    void Update()
    {
        bool finished = t_GoalJudgement.finished;
        if(finished == false && t_Countdown.playing == true)
        {
            seconds += Time.deltaTime;
            if (seconds > 60f)
            {
                minutes += 1;
                seconds -= 60f;
            }
            if (seconds < 10f)
            {
                zero = "0";
            }
            else
            {
                zero = "";
            }
            display_text = minutes.ToString() + "'" + zero + seconds.ToString("f3");
            timetext.text = display_text;
        }
        else
        {
            finished_minutes = minutes;
            finished_seconds = seconds;
            GoalTime = display_text;
        }
    }
}