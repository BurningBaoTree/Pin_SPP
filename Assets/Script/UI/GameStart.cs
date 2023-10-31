using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    public TMP_Dropdown drop;
    public Button nextPage;
    Animator animator;

    public Image skillS1;
    public Image skillS2;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        skillS1.sprite = null;
        skillS1.color = Color.clear;
        skillS2.sprite = null;
        skillS2.color = Color.clear;
    }
    private void Start()
    {
        GameManager.Inst.StageSet += StartGame;
        GameManager.Inst.ResetTheGame += () => { this.gameObject.SetActive(true); };
        nextPage.onClick.AddListener(NextPageGO);
        animator.speed = 1;
    }
    /// <summary>
    /// 시작할때 입력할 변수들
    /// </summary>
    void StartGame()
    {
        GameManager.Inst.Dificalty = (dificalty)drop.value;
    }
    void NextPageGO()
    {
        animator.SetTrigger("NextPage");
        GameManager.Inst.StageSet?.Invoke();
    }

    /// <summary>
    /// 버튼에 연동됨(시작버튼)
    /// </summary>
    public void StartButton()
    {
        GameManager.Inst.GameStart?.Invoke();
        this.gameObject.SetActive(false);
    }
    public void SkillSellect(Sprite sprite)
    {
        if (skillS1.sprite == null)
        {
            skillS1.sprite = sprite;
            GameManager.Inst.sprites[0] = sprite;
            skillS1.color = Color.white;
        }
        else if (skillS2.sprite == null)
        {
            skillS2.sprite = sprite;
            GameManager.Inst.sprites[1] = sprite;
            skillS2.color = Color.white;
        }
    }
    public void SkillUpLoad(SkillBase skill)
    {
        if (GameManager.Inst.Skill[0] == null)
        {
            GameManager.Inst.Skill[0] = skill;
        }
        else if (GameManager.Inst.Skill[1] == null)
        {
            GameManager.Inst.Skill[1] = skill;
        }
    }
}
