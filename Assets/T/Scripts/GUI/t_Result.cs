using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

public class t_Result : MonoBehaviour
{
    public bool first_finish;
    public GameObject Panel;
    public GameObject FinishText;
    public TextMeshProUGUI GoalTime_Text;
    public Image image;
    public GameObject BGM;
    public bool once = false;
    string gasUrl = "https://script.google.com/macros/s/AKfycbyKSjQbq5R-QiswheEITRciPkoXo_BvltFbOLTClLqtXZsEITYaS4aq9tK-8xISBWW-/exec";
    // Start is called before the first frame update
    void Start()
    {
        first_finish = true;
        Panel.SetActive(false);
        FinishText.SetActive(false);
        once = true;
    }

    // Update is called once per frame
    void Update()
    {
        bool finished = t_GoalJudgement.finished;
        if(finished == true)
        {
            if(first_finish == true)
            {
                first_finish = false;
                FinishText.SetActive(true);
                BGM.SetActive(false);
            }
            Invoke(nameof(PanelActive), 3f);
        }
    }

    void PanelActive()
    {
        string GoalTime = t_TimeCounter.GoalTime;
        FinishText.SetActive(false);
        Panel.SetActive(true);
        image.enabled = false;
        GoalTime_Text.text = GoalTime;
        Time.timeScale = 0;
        if (once) { StartCoroutine(PostData());
            once = false;
        }
        
    }
    IEnumerator PostData()
    {
        //WWWForm型のインスタンスを生成
        WWWForm form = new WWWForm();

        //それぞれのInputFieldから情報を取得
        string nameText = "Player";
        string commentText = t_TimeCounter.GoalTime;

        //値が空の場合は処理を中断
        if (string.IsNullOrEmpty(nameText) || string.IsNullOrEmpty(commentText))
        {
            Debug.Log("empty!");
            yield break;
        }

        //それぞれの値をカンマ区切りでcombinedText変数に代入
        string combinedText = string.Join(",", nameText, commentText);

        //formにPostする情報をvalというキー、値はcombinedTextで追加する
        form.AddField("val", combinedText);

        //UnityWebRequestを使ってGoogle Apps Script用URLにform情報をPost送信する
        using (UnityWebRequest req = UnityWebRequest.Post(gasUrl, form))
        {
            //情報を送信
            yield return req.SendWebRequest();
            if (IsWebRequestSuccessful(req))
            {
                Debug.Log("success");
            }
            else
            {
                Debug.Log("error");
            }
        }
    }

    bool IsWebRequestSuccessful(UnityWebRequest req)
    {
        /*プロトコルエラーとコネクトエラーではない場合はtrueを返す*/
        return req.result != UnityWebRequest.Result.ProtocolError &&
               req.result != UnityWebRequest.Result.ConnectionError;
    }
}
