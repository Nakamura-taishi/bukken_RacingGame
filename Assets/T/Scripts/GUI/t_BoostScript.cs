using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class t_BoostScript : MonoBehaviour
{
    float boost;
    [SerializeField] TextMeshProUGUI boostText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool finished = t_GoalJudgement.finished;
        if(finished == false)
        {
            t_Runner car = GameObject.Find("t_playerCAR").GetComponent<t_Runner>();
            boost = car.NowEngine;
            boostText.text = boost.ToString();
        }
    }
}
