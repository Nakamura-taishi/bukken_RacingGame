using TMPro;
using UnityEngine;

public class t_LapDisplay : MonoBehaviour
{
    public TextMeshProUGUI laptext;//ラップ数を表示するテキストボックス
    public t_LapCounter lap_counter;//外部のラップ数の変数

    int now_lapnum;//今のラップ数.
    int goal_lapnum;//目標ラップ数
    string display_text;

    void Update()
    {
        now_lapnum = lap_counter.lap;//今のラップ数を代入
        goal_lapnum = 3;//目標ラップ数を代入(3は仮)
        display_text = now_lapnum.ToString() + "/" + goal_lapnum.ToString();
        laptext.text = display_text;
    }
}
