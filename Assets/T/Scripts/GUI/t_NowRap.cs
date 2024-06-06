using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class t_NowRap : MonoBehaviour
{
    public m_CheckPointManager CheckPointManager;
    public Collider Playercol;
    public TextMeshProUGUI Raptext;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        if(CheckPointManager.carLapCount[Playercol] + 1 < CheckPointManager.lastLap)
        Raptext.text = (CheckPointManager.carLapCount[Playercol]+1).ToString();
    }
}
