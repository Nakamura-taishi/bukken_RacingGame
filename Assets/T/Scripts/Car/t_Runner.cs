using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// �Ԃ̎d�l
/// ��{����: W or��/S or�� �O�i/���,A or��/D or�� �^�C������/�E�ɋȂ���,C �u���[�L
/// Space �K�\�������g�p���ĉ���(�����ꍇ�͖���),Escape �Ԃ����̏�ŕ��A������(�l�ݖh�~�p�A����������)
/// V�h���t�g
/// �����^�C�}�[�ɂ��Ă�falltimer���������Ă��鎞��
/// finalfalltimer�ɂ��Ă͋l�ݖh�~�ׂ̈Ɏ����������̂ł�(falltimer�ŕ��A�o���Ȃ������Ƃ��Ɏ��s)�B
/// ��{���ׂ�falllimit���Ǘ����Ă���̂�falllimit��ύX���Ă��������B
/// �Ԃɉ���������ׂɒʏ��wheelcollider�̑���̑��ɎԎ��g�ɂ��͂������Ă܂��B
/// �h���t�g���̂݃X�e�A�����O�ƃ��[�^�[�̖�������ւɈڍs���Ă܂��B
/// angularDrag(�Ԃ̋Ȃ���ɂ���)�ɂ��Ă̓h���t�g���܂��͕X�̎��P�C�ʏ�R�Œ������Ă܂��B(�ς��Ă�����)
/// Drag�͂��������̐i�݂₷����\���Ă��܂��B
/// ���̑��ɂ��Ă͂ق��̃R�����g�A�E�g�����Ă��������B
/// ��{�I�ɑ��삵�Ă��ĕs�����Ȃ��悤�ɍ��̂��ڕW�ł��B
/// �ł���Ή��o����肽���ƍl���Ă��܂��B
/// </summary>

public class t_Runner : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public GameObject wheels;
    public GameObject cartrail;
    //�ő�̑��x(��{)
    public float maxMotorTorque;
    //�^�C�����Ȃ����p�x
    public float SteeringAngle;
    //�����{��
    public float boostspeed;
    //�u���[�L�̔n��
    public float breaking;
    //���̒l(�Q�Ɨp)
    public float steering1 = 0;
    public float motor1 = 0;
    //�����O�̑��x
    public Vector3 prospeed;
    //�����̐����p(�K�\����)
    public float MaxEngine;
    public float NowEngine;
    //��������
    public float friction;
    //������������
    public float falllimit;
    //�����^�C�}�[
    public float falltimer;
    public float falltime;
    public float finalfalltimer;
    public float finalfalltimer2;
    //��������
    public bool fall;
    public bool finalfall;
    //�����ʒu
    public Vector3 fallposition;
    public Vector3 fallangle;
    public Vector3 finalfallposition;
    public Quaternion finalfallangle;
    //��񐧌�
    bool once1 = false;
    //���x���
    public int cap = 60;
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
        Rigidbody rg = this.GetComponent<Rigidbody>();
        float steering = SteeringAngle * Input.GetAxis("Horizontal");
        //�p�x����
        if (Input.GetAxis("Horizontal") > 0)
        {
            steering1 = steering;


        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            steering1 = steering;


        }
        else
        {
            steering1 = 0;
        }
    }
    public void FixedUpdate()
    {
        Rigidbody rg = this.GetComponent<Rigidbody>();
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        //���ߖh�~
        if (NowEngine > MaxEngine)
        {
            NowEngine = MaxEngine;
        }
        //�h���t�g���s
        if ((Input.GetKey(KeyCode.V) || Input.GetAxis("ZL") != 0) && falltimer == 0 && !(steering1 == 0))
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
                    axleinfo.leftWheel.steerAngle = 0;
                    axleinfo.rightWheel.steerAngle = 0;
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
                if (axleinfo.drift == true && axleinfo.motor)
                {
                    axleinfo.leftWheel.steerAngle = 0;
                    axleinfo.rightWheel.steerAngle = 0;
                    axleinfo.leftWheel.motorTorque = 0;
                    axleinfo.rightWheel.motorTorque = 0;
                    axleinfo.steering = true;
                    axleinfo.motor = false;
                    axleinfo.drift = false;
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
            //�p�x�ݒ�
            if (axleInfo.steering)
            {
                if (motor > 0 || motor < 0 && falltimer == 0)
                {
                    rg.AddForce(transform.right * 100 * steering1);
                    if (!(steering1 == 0) && rg.velocity.magnitude <= 60)
                    {
                        rg.AddForce(transform.forward * -1 * (15000 - rg.velocity.magnitude * 250));
                    }
                }
                axleInfo.leftWheel.steerAngle = steering1;
                axleInfo.rightWheel.steerAngle = steering1;
            }
            ////��u���[�L
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
                if (motor > 0 || motor < 0 && falltimer == 0)
                {
                    rg.AddForce(transform.right * 200 * steering1);
                }
                axleInfo.leftWheel.steerAngle *= -2;
                axleInfo.rightWheel.steerAngle *= -2;
            }
            //�O�ior���
            if (axleInfo.motor)
            {
                //�K�\�������g�p���ĉ���(0�ɂȂ�Ɖ����s��)

                if (Input.GetKey(KeyCode.Space) || Input.GetAxis("ZR") != 0)
                {
                    if ((NowEngine >= 1) && falltimer == 0 && !(motor1 == 0))
                    {
                        cap = 9999;
                        rg.AddForce(transform.forward * 2000 * boostspeed);
                            NowEngine -= 1;

                    }

                    else { cap = 90; }
                }
                else
                {

                    cap = 90;
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
                        if (rg.velocity.magnitude <= 60 && falltimer == 0)
                        {
                            rg.AddForce(transform.forward * (15000 - rg.velocity.magnitude * 250));

                        }

                        axleInfo.leftWheel.motorTorque = motor1;
                        axleInfo.rightWheel.motorTorque = motor1;
                    }
                    else if (motor1 < 0)
                    {
                        axleInfo.leftWheel.motorTorque = motor1 * 2;
                        axleInfo.rightWheel.motorTorque = motor1 * 2;
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
            //�u���[�L
            if (Input.GetKey(KeyCode.C))
            {
                axleInfo.leftWheel.brakeTorque = breaking;
                axleInfo.rightWheel.brakeTorque = breaking;
            }

            //��u���[�L(�J�[�u����)
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
            //�ڒn����
            Ray ray = new Ray(transform.position, new Vector3(0, -1, 0));
            Ray ray2 = new Ray(transform.position + transform.up, transform.up);
            bool isgroun = Physics.Raycast(ray, out RaycastHit hit, 2.0f);
            bool iscrash = Physics.Raycast(ray2, out RaycastHit hit2, 2.0f);
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
            else if (isgroun == true)
            {
                falltimer = 0;
                fall = false;
                once1 = false;
                finalfalltimer = 0;
                finalfall = false;
                finalfalltimer2 += Time.deltaTime;
                falltime = 0;
            }
            if (isgroun == true && hit.collider.gameObject.tag == "Fall")
            {
                fall = true;
                falltime = 7;
            }
            //�l�ݖh�~�p
            //if (isgroun == true && Input.GetKeyDown(KeyCode.Escape))
            //{
            //    falltimer = 0;
            //    transform.position = fallposition;
            //    transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
            //    rg.velocity = Vector3.zero;
            //    falltime = 0;
            //}
            //�N���b�V�����������A
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
                    transform.position = new Vector3(fallposition.x - fallangle.x * 20, 10, fallposition.z - fallangle.z * 20);
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
                finalfallposition = transform.position;
                finalfalltimer2 = 0f;
            }
            if (finalfall == true)
            {
                finalfalltimer = 0;
                transform.position = new Vector3(finalfallposition.x - finalfallangle.x * 20, 10, finalfallposition.z - finalfallangle.z * 20);
                transform.rotation = finalfallangle;
            }
            //�n�ʂ̑����ɂ��e��
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
    public bool motor; //�쓮�ւ�?
    public bool steering; //�n���h������������Ƃ��Ɋp�x���ς�邩�H
    public bool drift = false;
}

