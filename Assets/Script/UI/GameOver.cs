using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI wintext;
    public TextMeshProUGUI Timetext;
    public TextMeshProUGUI FriendMassage;

    public Image skill1;
    public Image skill2;


    public float timeresult;
    public int mit;
    public int sec;
    public int mil;
    private void Start()
    {
        GameManager.Inst.GameOver += reasoultCheck;
        this.gameObject.SetActive(false);
    }
    void reasoultCheck(bool WinOrLose)
    {
        this.gameObject.SetActive(true);
        wintext.text = (WinOrLose ? "승리" : "패배") + " 했습니다.";
        getTimeResolt();
        if (GameManager.Inst.sprites[0] != null)
        {
            skill1.sprite = GameManager.Inst.sprites[0];
            skill1.color = Color.white;
        }
        else
        {
            skill1.sprite = null;
            skill1.color = Color.clear;
        }
        if (GameManager.Inst.sprites[1] != null)
        {
            skill2.sprite = GameManager.Inst.sprites[1];
            skill2.color = Color.white;
        }
        else
        {
            skill2.sprite = null;
            skill2.color = Color.clear;
        }
        if (WinOrLose)
        {
            if (GameManager.Inst.Dificalty == dificalty.easy)
            {
                FriendMassage.text = "친구의 메시지 : 너무 쉬웠나?";
            }
            else if (GameManager.Inst.Dificalty == dificalty.middle)
            {
                FriendMassage.text = "친구의 메시지 : 오.. 낫베드";
            }
            else
            {
                FriendMassage.text = "친구의 메시지 : 어캐했누 ㅋㅋㅋㅋㅋ";
            }
        }
        else
        {
            if (GameManager.Inst.Dificalty == dificalty.easy)
            {
                FriendMassage.text = "친구의 메시지 : 풉킥풉킥";
            }
            else if (GameManager.Inst.Dificalty == dificalty.middle)
            {
                FriendMassage.text = "친구의 메시지 : 흠... 뭐.. 그럴수 있지..\n 일단 넌 쉬움으로 다시 해라.";
            }
            else
            {
                FriendMassage.text = "친구의 메시지 : 내가 못깬다 했잖어~";
            }
        }
    }
    void getTimeResolt()
    {
        timeresult = GameManager.Inst.timeCount;
        mit = (int)timeresult / 60;
        sec = (int)timeresult % 60;
        mil = (int)(timeresult * 1000) % 100;
        Timetext.text = $"플레이 타임 : {mit: 00} : {sec: 00} : {mil: 00}";
    }
    public void ResetButton()
    {
        GameManager.Inst.ResetTheGame?.Invoke();
        this.gameObject.SetActive(false);
    }
}
