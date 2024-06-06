using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class t_NotstackNPC : MonoBehaviour
{
    public GameObject CAR;
    public Vector3 NowCARPos;
    public Vector3 PastCARPos;
    public Vector3 SpawnCARPos;
    public Quaternion NowCARAng;
    public Quaternion PastCARAng;
    public Quaternion SpawnCARAng;
    // Start is called before the first frame update
    void Start()
    {
        
        SpawnCARPos = CAR.transform.position;
        SpawnCARAng = CAR.transform.rotation;
        PastCARPos = CAR.transform.position;
        PastCARAng = CAR.transform .rotation;
        StartCoroutine("chase");
    }

    IEnumerator chase() {
        PastCARPos = CAR.transform.position;
        PastCARAng = CAR.transform.rotation;

        yield return new WaitForSeconds(3);

        if (Math.Abs(NowCARPos.x - PastCARPos.x) < 3 && Math.Abs(NowCARPos.y - PastCARPos.y) < 3 && Math.Abs(NowCARPos.z - PastCARPos.z) < 3) {
            CAR.transform.position = SpawnCARPos;
            CAR.transform.rotation = SpawnCARAng;
        }
        SpawnCARPos = PastCARPos;
        SpawnCARAng = PastCARAng;
        StartCoroutine("chase");
    }
    void Update()
    {
        NowCARPos = CAR.transform.position;
        PastCARAng = CAR.transform.rotation;
    }
}
