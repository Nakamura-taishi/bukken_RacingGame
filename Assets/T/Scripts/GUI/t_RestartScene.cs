using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class t_RestartScene : MonoBehaviour
{
    void Start()
    {
        t_GoalJudgement.finished = false;
        SceneManager.LoadScene("Racing", LoadSceneMode.Additive);
    }
}
