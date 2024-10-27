using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class t_TitleButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnClick()
    {
        t_GoalJudgement.finished = false;
        SceneManager.LoadScene("Title");
    }
}
