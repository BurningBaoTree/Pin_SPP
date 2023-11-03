using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public Image image;
    public Image image1;



    private void Start()
    {
        GameManager.Inst.GameStart += () =>
        {
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
}
