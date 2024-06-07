using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m_CheckPoint : MonoBehaviour
{
    [SerializeField] private m_CheckPointManager checkPointManager;
    [SerializeField] private string carTag;
    private int pointIndex;
    private m_CheckPoint checkPointBefore;
    private m_CheckPoint checkPointAfter;
    public List<Collider> ranking = new();
    public Transform thisTransform;

    void Start()
    {
        thisTransform = gameObject.transform;
        
        pointIndex = Array.IndexOf(checkPointManager.checkPoints, this);

        if (pointIndex >= 1)
        {
            checkPointBefore = checkPointManager.checkPoints[pointIndex - 1];
        }
        else
        {
            checkPointBefore = checkPointManager.checkPoints[^1];
        }

        if (pointIndex <= checkPointManager.checkPoints.Length - 2)
        {
            checkPointAfter = checkPointManager.checkPoints[pointIndex + 1];
        }
        else
        {
            checkPointAfter = checkPointManager.checkPoints[0];
        }
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag(carTag))
        {
            if (checkPointBefore.ranking.IndexOf(collision) >= 0)
            {
                checkPointBefore.ranking.Remove(collision);
                ranking.Add(collision);
                if (pointIndex == 0)
                {
                    if (!checkPointManager.carLapCount.ContainsKey(collision)) checkPointManager.carLapCount.Add(collision, 0);
                    checkPointManager.carLapCount[collision] += 1;
                    if (checkPointManager.carLapCount[collision] == checkPointManager.lastLap)
                    {
                        checkPointManager.lastRanking.Add(collision);
                    }
                }
            }
            else if (checkPointAfter.ranking.IndexOf(collision) >= 0)
            {
                checkPointAfter.ranking.Remove(collision);
                ranking.Add(collision);
            }
            else if (ranking.IndexOf(collision) >= 0)
            {
                // 何もしない
            }
            else
            {
                if (!checkPointManager.addedCollider.ContainsKey(collision))
                {
                    ranking.Add(collision);
                    checkPointManager.addedCollider.Add(collision, true);
                    if (!checkPointManager.carLapCount.ContainsKey(collision)) checkPointManager.carLapCount.Add(collision, 0);
                    if (pointIndex != 0) checkPointManager.carLapCount[collision]--;
                }
            }
        }
    }

    public Dictionary<int, List<Collider>> GetCurrentRankingFromPoint()
    {
        ranking.Sort((a, b) => Mathf.RoundToInt(Vector3.SqrMagnitude(b.transform.position - thisTransform.position) - Vector3.SqrMagnitude(a.transform.position - thisTransform.position)));  // 降順にソート
        Dictionary<int, List<Collider>> result = new();
        for (int i = 0; i < ranking.Count; i++)
        {
            if (!checkPointManager.carLapCount.ContainsKey(ranking[i])) checkPointManager.carLapCount.Add(ranking[i], 0);
            int lapCount = checkPointManager.carLapCount[ranking[i]];
            if (!result.ContainsKey(lapCount))
            {
                result.Add(lapCount, new List<Collider>());
            }
            result[lapCount].Add(ranking[i]);
        }
        return result;
    }
}
