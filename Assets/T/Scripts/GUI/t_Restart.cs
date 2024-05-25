using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class t_Restart : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("RestartScene", LoadSceneMode.Additive);
    }
}
