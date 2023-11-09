using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public Image image;
    public Image image1;

    Action updater;

    float intime1 = 0f;
    float intime2 = 0f;
    float targettime1 = 0f;
    float targettime2 = 0f;

    private void Start()
    {
        GameManager.Inst.Player.SkillUsed1 += coolActivate1;
        GameManager.Inst.Player.SkillUsed2 += coolActivate2;
        updater = () => { };
        GameManager.Inst.GameStart += () =>
        {
            updater -= cooltimeEffect1;
            updater -= cooltimeEffect2;
            intime1 = 0f;
            intime2 = 0f;
            image.fillAmount = 1;
            image1.fillAmount = 1;

            this.gameObject.SetActive(true);
            if (GameManager.Inst.sprites[0] != null)
            {
                image.sprite = GameManager.Inst.sprites[0];
                image.color = Color.white;
            }
            else
            {
                image.sprite = null;
                image.color = Color.clear;
            }
            if (GameManager.Inst.sprites[1] != null)
            {
                image1.sprite = GameManager.Inst.sprites[1];
                image1.color = Color.white;
            }
            else
            {
                image1.sprite = null;
                image1.color = Color.clear;
            }
        };
        GameManager.Inst.GameOver += (bool trye) => { this.gameObject.SetActive(false); };
        this.gameObject.SetActive(false);
    }
    private void Update()
    {
        updater();
    }

    void cooltimeEffect1()
    {
        intime1 += Time.deltaTime;
        image.fillAmount = intime1 / targettime2;
        if (intime1 > targettime1)
        {
            updater -= cooltimeEffect1;
        }
    }
    void cooltimeEffect2()
    {
        intime2 += Time.deltaTime;
        image1.fillAmount = intime2 / targettime2;
        if (intime2 > targettime2)
        {
            updater -= cooltimeEffect2;
        }
    }
    void coolActivate1(float time)
    {
        intime1 = 0f;
        targettime1 = time;
        updater += cooltimeEffect1;
    }
    void coolActivate2(float time)
    {
        intime2 = 0f;
        targettime2 = time;
        updater += cooltimeEffect2;
    }
}
