using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class t_GoalJudgement : MonoBehaviour
{
    public static bool finished;
    void Start()
    {
        finished = false;//ÉSÅ[ÉãÇ∑ÇÈëOÇÕfalse
    }
    
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "CAR")
        {
            Debug.Log("CAR");
            finished = true;//ÉSÅ[ÉãÇµÇΩå„ÇÕtrue
            t_Countdown.playing = false;
        }
    }
}
