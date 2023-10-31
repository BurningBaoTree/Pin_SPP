using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI wintext;
    public TextMeshProUGUI Timetext;

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
    }
    void getTimeResolt()
    {
        timeresult = GameManager.Inst.timeCount;
        mit = (int)timeresult / 60;
        sec = (int)timeresult % 60;
        mil = (int)(timeresult * 1000) % 100;
        Timetext.text = $"{mit: 00} : {sec: 00} : {mil: 00}";
    }
    public void ResetButton()
    {
        GameManager.Inst.ResetTheGame?.Invoke();
        this.gameObject.SetActive(false);
    }
}
