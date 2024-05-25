using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t_Gasoline : MonoBehaviour
{
    /// <summary>
    /// ガソリン補給のオブジェクト用のスクリプトです。
    /// アタッチしたオブジェクトをgusresにアタッチするとすぐに機能します。
    /// 車がこのオブジェクトに触れた場合車のガソリンを全回復して消失します。
    /// </summary>
    public GameObject gusres;
    private void OnCollisionEnter(Collision collision)
    {
        t_Runner gus = collision.gameObject.GetComponent<t_Runner>();
        gus.NowEngine = gus.MaxEngine;
        gusres.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
