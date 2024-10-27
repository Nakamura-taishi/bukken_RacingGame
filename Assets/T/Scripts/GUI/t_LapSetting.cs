using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class t_LapSetting : MonoBehaviour
{
    static public int laps = 3;
    public Button Up,Down;
    public TextMeshProUGUI NowLap;
    // Start is called before the first frame update
    void Start()
    {
        NowLap.text = laps.ToString();
        if (laps == 5) Up.interactable = false;
        else if(laps == 1)Down.interactable = false;
    }
    public void UpButton()
    {
        laps++;
        if (laps == 5)
        {
            Up.interactable = false;
        }
        Down.interactable = true;
        NowLap.text = laps.ToString();

    }
    public void DownButton()
    {
        laps--;
        if (laps == 1)
        {
            Down.interactable = false;
        }
        Up.interactable = true;
        NowLap.text = laps.ToString();
    }
}
