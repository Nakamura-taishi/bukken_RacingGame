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
    public GameObject PlayerCAR;
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
        //WWWForm�^�̃C���X�^���X�𐶐�
        WWWForm form = new WWWForm();

        //���ꂼ���InputField��������擾
        string nameText = t_InputPlayer.Name;
        string commentText = t_TimeCounter.GoalTime;
        string Engine = PlayerCAR.GetComponent<t_Runner>().NowEngine.ToString();

        //���ꂼ��̒l���J���}��؂��combinedText�ϐ��ɑ��
        string combinedText = string.Join(",", nameText, commentText,Engine);

        //form��Post�������val�Ƃ����L�[�A�l��combinedText�Œǉ�����
        form.AddField("val", combinedText);

        //UnityWebRequest���g����Google Apps Script�pURL��form����Post���M����
        using (UnityWebRequest req = UnityWebRequest.Post(gasUrl, form))
        {
            //���𑗐M
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
        /*�v���g�R���G���[�ƃR�l�N�g�G���[�ł͂Ȃ��ꍇ��true��Ԃ�*/
        return req.result != UnityWebRequest.Result.ProtocolError &&
               req.result != UnityWebRequest.Result.ConnectionError;
    }
}
