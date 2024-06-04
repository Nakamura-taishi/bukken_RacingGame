using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class t_SpeedScript : MonoBehaviour
{
    float float_speed;
    public float speed;
    [SerializeField] TextMeshProUGUI speedText;
    void Update()
    {
        Rigidbody steer5 = GameObject.Find("t_playerCAR").GetComponent<Rigidbody>();
        float_speed = steer5.velocity.magnitude;
        speed = (float_speed / 2) * (float_speed / 5);
        speedText.text = Math.Round(speed).ToString();
    }
}
