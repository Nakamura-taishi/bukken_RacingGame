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
        
    }
}
