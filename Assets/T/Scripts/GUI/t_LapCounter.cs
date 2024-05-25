using System.Collections;
using UnityEngine;

public class t_LapCounter : MonoBehaviour
{
    public int lap;
    void Start()
    {
        lap = 0;
        StartCoroutine(LapCount());
    }

    private IEnumerator LapCount()
    {
        while (true)
        {
            lap += 1;
            yield return new WaitForSeconds(1f);
        }
    }
}
