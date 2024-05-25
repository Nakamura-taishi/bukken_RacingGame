using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class t_RestartButton : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("RestartScene");
    }
}
