using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 車の仕様
/// 基本操作: W or↑/S or↓ 前進/後退,A or←/D or→ タイヤを左/右に曲げる,C ブレーキ
/// Space ガソリンを使用して加速(無い場合は無効),Escape 車をその場で復帰させる(詰み防止用、落下中無効)
/// Vドリフト
/// 落下タイマーについてはfalltimerが落下している時間
/// finalfalltimerについては詰み防止の為に実装したものです(falltimerで復帰出来なかったときに実行)。
/// 基本すべてfalllimitが管理しているのでfalllimitを変更してください。
/// 車に加速をつける為に通常のwheelcolliderの操作の他に車自身にも力を加えてます。
/// ドリフト時のみステアリングとモーターの役割が後輪に移行してます。
/// angularDrag(車の曲がりにくさ)についてはドリフト時または氷の時１，通常３で調整してます。(変えてもいい)
/// Dragはそもそもの進みやすさを表しています。
/// その他についてはほかのコメントアウトを見てください。
/// 基本的に操作していて不快がないように作るのが目標です。
/// できれば演出も作りたいと考えています。
/// </summary>

public class t_Runner : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public GameObject wheels;
    public GameObject cartrail;
    //最大の速度(基本)
    public float maxMotorTorque;
    //タイヤを曲げれる角度
    public float SteeringAngle;
    //加速倍率
    public float boostspeed;
    //ブレーキの馬力
    public float breaking;
    //仮の値(参照用)
    public float steering1 = 0 ;
    public float motor1 = 0;
    //加速前の速度
    public Vector3 prospeed;
    //加速の制限用(ガソリン)
    public float MaxEngine;
    public float NowEngine;
    //減速処理
    public float friction;
    //落下制限時間
    public float falllimit;
    //落下タイマー
    public float falltimer;
    public float falltime;
    public float finalfalltimer;
    public float finalfalltimer2;
    //落下判定
    public bool fall;
    public bool finalfall;
    //復活位置
    public Vector3 fallposition;
    public Vector3 fallangle;
    public Vector3 finalfallposition;
    public Quaternion finalfallangle;
    //一回制限
    bool once1 = false;
    //速度上限
    public int cap = 60;
    //ガソリン無限モード
    public bool infinitygasoline = false;
    public void Start()
    {
        NowEngine = MaxEngine;
        finalfallposition = transform.position;
        finalfallangle = transform.rotation;
        if (wheels == null)
        {
            wheels = transform.Find("Wheels").gameObject;
        }
        if (cartrail == null)
        {
            cartrail = wheels.transform.Find("particles").gameObject;
        }
        
    }
    public void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            infinitygasoline = true;
        }
        Rigidbody rg = this.GetComponent<Rigidbody>(); 
        float steering = SteeringAngle * Input.GetAxis("Horizontal");
        //角度調整
        if (Input.GetAxis("Horizontal") > 0)
        {
                steering1 = steering;

                
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
                steering1 = steering;


        }else
        {
                steering1 = 0;
        }
    }
    public void FixedUpdate()
    {
        Rigidbody rg = this.GetComponent<Rigidbody>();
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        //超過防止
        if (NowEngine > MaxEngine)
        {
            NowEngine = MaxEngine;
        }
        //ドリフト走行
        if (Input.GetKey(KeyCode.V) && falltimer == 0)
        {
            foreach (AxleInfo axleinfo in axleInfos)
            {
                if (axleinfo.steering && axleinfo.drift == false)
                {
                    axleinfo.leftWheel.steerAngle = 0;
                    axleinfo.rightWheel.steerAngle = 0;
                    axleinfo.leftWheel.motorTorque = 0;
                    axleinfo.rightWheel.motorTorque = 0;
                    axleinfo.steering = false;
                    axleinfo.motor = true;
                    axleinfo.drift = true;
                }
                if (axleinfo.motor && axleinfo.drift == false)
                {
                    axleinfo.leftWheel.motorTorque = 0;
                    axleinfo.rightWheel.motorTorque = 0;
                    axleinfo.steering = true;
                    axleinfo.motor = false;
                    axleinfo.drift = true;
                    
                }
            }
            cartrail.SetActive(true);
        }
        else
        {
            foreach (AxleInfo axleinfo in axleInfos)
            {
                if (axleinfo.drift == true && axleinfo.motor && !(steering1 == 0))
                {
                    axleinfo.leftWheel.steerAngle = 0;
                    axleinfo.rightWheel.steerAngle = 0;
                    axleinfo.leftWheel.motorTorque = 0;
                    axleinfo.rightWheel.motorTorque = 0;
                    axleinfo.steering = true;
                    axleinfo.motor = false;
                    axleinfo.drift = false;
                    rg.angularDrag = 3;
                }
                if (axleinfo.drift == true && axleinfo.motor == false)
                {
                    axleinfo.leftWheel.steerAngle = 0;
                    axleinfo.rightWheel.steerAngle = 0;
                    axleinfo.leftWheel.motorTorque = 0;
                    axleinfo.rightWheel.motorTorque = 0;
                    axleinfo.steering = false;
                    axleinfo.motor = true;
                    axleinfo.drift = false;
                }
            }
            cartrail.SetActive(false);
        }



        foreach (AxleInfo axleInfo in axleInfos)
        {
            //角度設定
            if (axleInfo.steering && !Input.GetKey(KeyCode.C))
            {
                if (motor > 0 || motor < 0 && falltimer == 0)
                {
                    rg.AddForce(transform.right * 200 * steering1);
                }

                axleInfo.leftWheel.steerAngle = steering1;
                axleInfo.rightWheel.steerAngle = steering1;
                rg.angularDrag = 3;
            }
            ////弱ブレーキ
            //else if (axleInfo.steering && Input.GetKey(KeyCode.C))
            //{
            //    if (motor > 0 || motor < 0 && falltimer == 0)
            //    {
            //        rg.AddForce(transform.right * 300 * steering1);
            //    }

            //    axleInfo.leftWheel.steerAngle = steering1 * 2f;
            //    axleInfo.rightWheel.steerAngle = steering1 * 2f;
            //    rg.angularDrag = 2;
            //}
            if (axleInfo.steering && axleInfo.drift)
            {
                axleInfo.leftWheel.steerAngle *= -2;
                axleInfo.rightWheel.steerAngle *= -2;
            }
            //前進or後退
            if (axleInfo.motor)
            {
                //ガソリンを使用して加速(0になると加速不可)

                if (Input.GetKey(KeyCode.Space))
                {
                    if ((NowEngine >= 1 || infinitygasoline) && falltimer == 0)
                    {
                        cap = 9999;
                        rg.AddForce(transform.forward * 2000 * boostspeed);
                        if (!infinitygasoline)
                        {
                            NowEngine -= 1;
                        }

                    }

                    else { cap = 60; }
                }
                else
                {

                    cap = 60;
                }
                


                if (rg.velocity.magnitude >= cap)
                {
                    axleInfo.leftWheel.motorTorque = 0;
                    axleInfo.rightWheel.motorTorque = 0;
                }
                else
                {
                    motor1 = motor;
                    if (motor1 > 0)
                    {
                        if (rg.velocity.magnitude <= 40 && falltimer == 0)
                        {
                            rg.AddForce(transform.forward * (10000 - rg.velocity.magnitude * 250));

                        }

                        axleInfo.leftWheel.motorTorque = motor1;
                        axleInfo.rightWheel.motorTorque = motor1;
                    }
                    else if (motor1 < 0)
                    {
                        axleInfo.leftWheel.motorTorque = motor1;
                        axleInfo.rightWheel.motorTorque = motor1;
                    }
                    else
                    {
                        axleInfo.leftWheel.motorTorque = 0;
                        axleInfo.rightWheel.motorTorque = 0;
                    }
                }
                if (rg.velocity.magnitude > 40 && falltimer == 0)
                {
                    cartrail.SetActive(true);
                }
                else cartrail.SetActive(false);
            }
            if (Input.GetKey(KeyCode.P))
            {
                SceneManager.LoadScene("Title");
            }
            //ブレーキ
            if (Input.GetKey(KeyCode.C))
            {
                axleInfo.leftWheel.brakeTorque = breaking;
                axleInfo.rightWheel.brakeTorque = breaking;
            }

            //弱ブレーキ(カーブ強化)
            //else if (Input.GetKey(KeyCode.C))
            //{
            //    axleInfo.leftWheel.brakeTorque = maxMotorTorque / 5;
            //    axleInfo.rightWheel.brakeTorque = maxMotorTorque / 5;
            //}
            else

            {
                axleInfo.leftWheel.brakeTorque = 0;
                axleInfo.rightWheel.brakeTorque = 0;

            }
            //接地判定
            Ray ray = new Ray(transform.position, -transform.up);
            Ray ray2 = new Ray(transform.position + transform.up, transform.up);
            bool isgroun = Physics.Raycast(ray, out RaycastHit hit,2.0f);
            bool iscrash = Physics.Raycast(ray2, out RaycastHit hit2,2.0f);
            Debug.DrawRay(ray.origin, ray.direction, Color.red, 1);

            if (isgroun == false || hit.collider.gameObject.tag == "Obstacle")
            {

                if (once1 == false)
                {
                    fallposition = transform.position;
                    fallangle = rg.velocity.normalized;
                    fallangle.y = 0;
                    once1 = true;
                }

                falltimer += Time.deltaTime;
                if (falltimer >= falllimit)
                {
                    fall = true;

                }

            }
            else if (isgroun == true) {
                falltimer = 0;
                fall = false;
                once1 = false;
                finalfalltimer = 0;
                finalfall = false;
                finalfalltimer2 += Time.deltaTime;
                falltime = 0;
            }
            //詰み防止用
            if (isgroun == true && Input.GetKeyDown(KeyCode.Escape))
            {
                falltimer = 0;
                transform.position = fallposition;
                transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
                rg.velocity = Vector3.zero;
                falltime = 0;
            }
            //クラッシュ時即時復帰
            //if (iscrash && !(hit2.collider.gameObject.tag == "Obstacle"))
            //{
            //    falltimer = 0;
            //    transform.position = fallposition;
            //    transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
            //    rg.velocity = Vector3.zero;
            //    falltime = 0;
            //}
            if (fall == true) 
            {
                falltimer = 0;
                if (falltime > 3)
                {
                    transform.position = fallposition - fallangle * 20;
                    transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
                    rg.velocity = Vector3.zero;
                    falltime = 0;
                }
                
                finalfalltimer += Time.deltaTime;
                falltime += Time.deltaTime; 
            }
            if (finalfalltimer > falllimit)
            {
                finalfall = true;
            }
            if (finalfalltimer2 > falllimit)
            {
                finalfallposition = transform.position ;
                finalfalltimer2 = 0f;
            }
            if (finalfall == true)
            {
                finalfalltimer = 0;
                transform.position = finalfallposition;
                transform.rotation = finalfallangle;
            }
            //地面の属性による影響
            if (isgroun == true)
            {
                GameObject ground = hit.collider.gameObject;
                if (ground.tag == "Dirt" && !(ground.tag == "Cold") && !(ground.tag == "Bad"))
                {
                    rg.drag = 0.6f;
                    t_AllWheel wh = wheels.GetComponent<t_AllWheel>();
                    for (int i = 0; i < wh.Wheels.Length; i++)
                    {
                        wh.Wheels[i].Frif.Stiffness = 0.5f;
                        wh.Wheels[i].Sidf.Stiffness = 0.5f;

                    }
                }
                else if (ground.tag == "Cold")
                {
                    rg.drag = 0.1f;
                    t_AllWheel wh = wheels.GetComponent<t_AllWheel>();
                    for (int i = 0; i < wh.Wheels.Length; i++)
                    {
                        wh.Wheels[i].Frif.Stiffness = 1f;
                        wh.Wheels[i].Sidf.Stiffness = 1f;

                    }
                    rg.angularDrag = 1f;
                }
                else if (ground.tag == "Bad")
                {
                    t_AllWheel wh = wheels.GetComponent<t_AllWheel>();
                    for (int i = 0; i < wh.Wheels.Length; i++)
                    {
                        wh.Wheels[i].Frif.Stiffness = 1f;
                        wh.Wheels[i].Sidf.Stiffness = 1f;

                    }
                    rg.drag = 0.4f;
                }
                else
                {
                    t_AllWheel wh = wheels.GetComponent<t_AllWheel>();
                    for (int i = 0; i < wh.Wheels.Length; i++)
                    {
                        wh.Wheels[i].Frif.Stiffness = 1f;
                        wh.Wheels[i].Sidf.Stiffness = 1f;

                    }
                    rg.drag = 0.1f;
                    if (!(Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.V)))
                    {
                        rg.angularDrag = 3f;
                    }
                    
                }
            }
        }
    }
    
}
[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; //駆動輪か?
    public bool steering; //ハンドル操作をしたときに角度が変わるか？
    public bool drift = false;
}

