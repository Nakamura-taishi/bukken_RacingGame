using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t_CarFreeze : MonoBehaviour
{
    public GameObject car;
    void Update()
    {
        bool finished = t_GoalJudgement.finished;
        car.GetComponent<t_Runner>().enabled = !finished;
    }
}
