using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class t_RULE3 : MonoBehaviour
{
    
    // Start is called before the first frame update
    public Sprite[] ruleimages;
    public Image ruleimagescript;
    public TextMeshProUGUI now;
    public TextMeshProUGUI maximum;
    public int nowimage;
    public Button[] buttons;
    void Start()
    {
        if (nowimage == ruleimages.Length - 1)
        {
            buttons[1].interactable = false;
        }
        if (0 == nowimage)
        {
            buttons[0].interactable = false;
        }
        ruleimagescript.sprite = ruleimages[nowimage];
        maximum.text = ruleimages.Length.ToString();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            buttons[0].onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            buttons[1].onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            buttons[2].onClick.Invoke();
        }
    }
    public void Onclickback()
    {
        SceneManager.LoadScene("Title");
    }
    public void OnClickdown()
    {
        nowimage += 1;
        now.text = (nowimage + 1).ToString();
        ruleimagescript.sprite = ruleimages[nowimage];
        if (nowimage == ruleimages.Length - 1)
        {
            buttons[1].interactable = false;
        }
        if (buttons[0].interactable == false)
        {
            buttons[0].interactable = true;
        }
    }
    public void OnClickup()
    {
        nowimage -= 1;
        now.text = (nowimage+1).ToString();
        ruleimagescript.sprite = ruleimages[nowimage];
        if (0 == nowimage)
        {
            buttons[0].interactable = false;
        }
        if (buttons[1].interactable == false)
        {
            buttons[1].interactable = true;
        }
    }
}
