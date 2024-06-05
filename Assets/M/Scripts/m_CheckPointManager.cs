using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m_CheckPointManager : MonoBehaviour
{
    public m_CheckPoint[] checkPoints;  // 全てのCheckPointをスタートから順に格納したもの
    public Dictionary<Collider, int> carLapCount = new();  // 車の周回数
    public int lastLap;
    public List<Collider> lastRanking = new();
    
    public List<Collider> GetCurrentRanking()
    {
        Dictionary<int, List<Collider>>[] pointRanking = new Dictionary<int, List<Collider>>[checkPoints.Length];
        for (int i = 0; i < checkPoints.Length; i++)
        {
            pointRanking[i] = checkPoints[i].GetCurrentRankingFromPoint();
        }
        
        List<Collider> result = new();
        for (int lap = lastLap; lap >= 0; lap--) {
            for (int i = checkPoints.Length - 1; i >= 0; i--)
            {
                if (!pointRanking[i].ContainsKey(lap)) continue;
                result.AddRange(pointRanking[i][lap]);
            }
        }
        return result;
    }
}
