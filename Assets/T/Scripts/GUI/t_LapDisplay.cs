using TMPro;
using UnityEngine;

public class t_LapDisplay : MonoBehaviour
{
    public TextMeshProUGUI laptext;//���b�v����\������e�L�X�g�{�b�N�X
    public t_LapCounter lap_counter;//�O���̃��b�v���̕ϐ�

    int now_lapnum;//���̃��b�v��.
    int goal_lapnum;//�ڕW���b�v��
    string display_text;

    void Update()
    {
        now_lapnum = lap_counter.lap;//���̃��b�v������
        goal_lapnum = 3;//�ڕW���b�v������(3�͉�)
        display_text = now_lapnum.ToString() + "/" + goal_lapnum.ToString();
        laptext.text = display_text;
    }
}
