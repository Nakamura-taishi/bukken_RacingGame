using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class t_GoalJudgement : MonoBehaviour
{
    public static bool finished;
    void Start()
    {
        finished = false;//�S�[������O��false
    }
    
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "CAR")
        {
            Debug.Log("CAR");
            finished = true;//�S�[���������true
            t_Countdown.playing = false;
        }
    }
}
