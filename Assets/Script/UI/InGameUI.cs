using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public Image image;
    public Image image1;


    private void OnEnable()
    {
        if (GameManager.Inst.sprites[0] != null)
        {
            image.sprite = GameManager.Inst.sprites[0];
        }
        else if (GameManager.Inst.sprites[1] != null)
        {
            image1.sprite = GameManager.Inst.sprites[1];
        }
    }
    private void Start()
    {
        GameManager.Inst.GameStart += () => { this.gameObject.SetActive(true); };
        GameManager.Inst.GameOver += (bool trye) => { this.gameObject.SetActive(false); };
        this.gameObject.SetActive(false);
    }
}
