using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class t_Rank : MonoBehaviour
{
    public m_CheckPointManager CheckPointManager;
    public List<Collider> Rank;
    public GameObject ImageUI;
    public Collider playercol;
    public Collider npccol;
    public Sprite Rank1;
    public Sprite Rank2;
    public TextMeshProUGUI result;
    private Image image;

    void Awake()
    {
        CheckPointManager.carLapCount[playercol] = 0;
        CheckPointManager.carLapCount[npccol] = 0;
    }
    void Start()
    {

        image = ImageUI.GetComponent<Image>();    
    }
    void FixedUpdate()
    {
        if (CheckPointManager.carLapCount.ContainsKey(playercol) && CheckPointManager.carLapCount.ContainsKey(npccol))
        { Rank = CheckPointManager.GetCurrentRanking(); }
        if (Rank.Count == 2)
        {
            
            if (Rank[0] == playercol)
            {
                image.sprite = Rank1;
            }
            else
            {
                image.sprite = Rank2;
            }
        }
        for(int i = 0;i < CheckPointManager.lastRanking.Count;i++) {
            if (CheckPointManager.lastRanking[i] == playercol) {
                t_GoalJudgement.finished = true;
                if (i == 0)
                {
                    result.text = "Win!";
                }
                else 
                {
                    result.text = "Lose...";
                }
            }
        }
    }
}
