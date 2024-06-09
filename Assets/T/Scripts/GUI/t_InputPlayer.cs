using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class t_InputPlayer : MonoBehaviour
{
    public static string Name;
    public InputField PlayerName;
    public TextMeshProUGUI ERROR;

    public void OnclickSTART() 
    {
        Name = PlayerName.text;
        if (string.IsNullOrEmpty(PlayerName.text))
        {
            ERROR.text = "EMPTY!!";
        }
        else 
        {
            SceneManager.LoadSceneAsync("Racing");
        }
    }
    public void OnclickBACK() 
    {
        SceneManager.LoadScene("Title");
    }
}
