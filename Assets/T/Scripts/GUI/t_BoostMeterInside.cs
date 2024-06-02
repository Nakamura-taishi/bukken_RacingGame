using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t_BoostMeterInside : MonoBehaviour
{
    private float boost;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool finished = t_GoalJudgement.finished;
        if (finished == false)
        {
            t_Runner car = GameObject.Find("t_playerCAR").GetComponent<t_Runner>();
            boost = car.NowEngine;
            this.transform.localScale = new Vector3(1, (boost / 1000), 0);
        }
    }
}
