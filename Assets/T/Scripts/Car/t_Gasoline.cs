using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t_Gasoline : MonoBehaviour
{
    /// <summary>
    /// �K�\�����⋋�̃I�u�W�F�N�g�p�̃X�N���v�g�ł��B
    /// �A�^�b�`�����I�u�W�F�N�g��gusres�ɃA�^�b�`����Ƃ����ɋ@�\���܂��B
    /// �Ԃ����̃I�u�W�F�N�g�ɐG�ꂽ�ꍇ�Ԃ̃K�\������S�񕜂��ď������܂��B
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
