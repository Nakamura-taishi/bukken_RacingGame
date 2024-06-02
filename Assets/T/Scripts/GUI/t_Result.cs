using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class t_Result : MonoBehaviour
{
    public bool first_finish;
    public GameObject Panel;
    public GameObject FinishText;
    public TextMeshProUGUI GoalTime_Text;
    // Start is called before the first frame update
    void Start()
    {
        first_finish = true;
        Panel.SetActive(false);
        FinishText.SetActive(false);
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
            }
            Invoke(nameof(PanelActive), 3f);
        }
    }

    void PanelActive()
    {
        string GoalTime = t_TimeCounter.GoalTime;
        FinishText.SetActive(false);
        Panel.SetActive(true);
        GoalTime_Text.text = GoalTime;
        Time.timeScale = 0;
    }
}
