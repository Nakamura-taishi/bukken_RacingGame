using System.Threading.Tasks;
using UnityEngine;

public class t_Countdown : MonoBehaviour
{
    public GameObject Panel;
    public GameObject Text3;
    public GameObject Text2;
    public GameObject Text1;
    public GameObject TextStart;
    public AudioClip startaudio;
    public GameObject BGM;
    public m_CheckPointManager CheckPointManager;
    static public bool playing = false;
    async void Start()
    {
        CheckPointManager.lastLap = t_LapSetting.laps;
        Time.timeScale = 0f;
        Panel.SetActive(false);
        Text3.SetActive(false);
        Text2.SetActive(false);
        Text1.SetActive(false);
        TextStart.SetActive(false);
        // 1ïbñàÇ…äeä÷êîÇé¿çs
        await text3();
        await text2();
        await text1();
        await starttext();
        await finishtext();
    }
    async Task text3()
    {
        await Task.Delay(1000);
        Text3.SetActive(true);
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(startaudio);
    }
    async Task text2()
    {
        await Task.Delay(1000);
        Text3.SetActive(false);
        Text2.SetActive(true);
    }
    async Task text1()
    {
        await Task.Delay(1000);
        Text2.SetActive(false);
        Text1.SetActive(true);
    }
    async Task starttext()
    {
        await Task.Delay(1000);
        Text1.SetActive(false);
        TextStart.SetActive(true);
        Time.timeScale = 1f;
        playing = true;
        BGM.SetActive(true);
    }
    async Task finishtext()
    {
        await Task.Delay(1000);
        TextStart.SetActive(false);
    }
}