using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class t_StartButton : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadSceneAsync("Racing");
    }
}
