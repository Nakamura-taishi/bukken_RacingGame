using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class t_NotstackPlayer : MonoBehaviour
{
    public GameObject CAR;
    private Rigidbody runner;
    public Vector3 NowCARPos;
    public Vector3 PastCARPos;
    public Quaternion NowCARAng;
    public Quaternion PastCARAng;
    // Start is called before the first frame update
    void Start()
    {
        PastCARPos = CAR.transform.position;
        PastCARAng = CAR.transform .rotation;
        StartCoroutine("chase");
        runner = CAR.GetComponent<Rigidbody>();
    }

    IEnumerator chase() {
        PastCARPos = CAR.transform.position;
        PastCARAng = CAR.transform.rotation;

        yield return new WaitForSeconds(10);
        StartCoroutine("chase");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            CAR.transform.position = PastCARPos;
            CAR.transform.rotation = PastCARAng;
            runner.velocity = Vector3.zero;
        }
        NowCARPos = CAR.transform.position;
        PastCARAng = CAR.transform.rotation;
    }
}
