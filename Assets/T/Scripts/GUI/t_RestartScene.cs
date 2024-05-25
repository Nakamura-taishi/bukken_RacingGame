using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class t_RestartScene : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("tatsuya1111tt2++", LoadSceneMode.Additive);
        SceneManager.LoadScene("GUIt+", LoadSceneMode.Additive);
        SceneManager.LoadScene("SampleSceneq", LoadSceneMode.Additive);
    }
}
