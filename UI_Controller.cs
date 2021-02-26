using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Controller : MonoBehaviour
{
    public Text levelScoreText;
    public Text currencyText;

    public GameObject buyButton;

    private GameObject[] UI_Elements;
    public GameObject activeUI;

    public TextMeshProUGUI recordScore, recordLevel;
    public int toPass;

    private void Awake()
    {
        UI_Elements = new GameObject[transform.childCount];
        for (int i = 0; i < UI_Elements.Length; i++)
        {
            UI_Elements[i] = transform.GetChild(i).gameObject;
        }

        activeUI = UI_Elements[1];
        
        BossCheck(ControllerScript.bossCounter);

        activeUI.SetActive(true);
    }

    public void SwapMenus(int index)
    {
        activeUI.SetActive(false);
        activeUI = UI_Elements[index];
        if (activeUI.name == "(0)Main_Menu_Holder")
        {
            ControllerScript.instance.theTarget.SetActive(false);
        }

        activeUI.SetActive(true);
    }

    public Text endgameScoreText;
    public Text hiStageText;

    public void EndScore(int score, int stage)
    {
        endgameScoreText.text = score.ToString();

        hiStageText.text = stage.ToString();
    }

    public void RecordScores(int score, int stage)
    {
        recordLevel.text = "STAGE: " + stage.ToString();
        recordScore.text = "SCORE: " + score.ToString();
    }

    public Text buyButtonText;

    public void TweenButton(bool show, int cost)
    {

        buyButtonText.text = "BUY FOR " + cost.ToString();

        if (show)
            LeanTween.scale(buyButton, Vector3.one, 0.1f);
        else
            LeanTween.scale(buyButton, Vector3.zero, 0.1f);
    }

    public void ShakeButton()
    {
        LeanTween.moveLocalX(buyButton, buyButton.transform.position.x, 0.15f).setEaseShake();
    }

    public Image cursorIcon;

    public void UpdateCursorIcon(Sprite sprite)
    {
        cursorIcon.sprite = sprite;
    }

    public GameObject[] bossSprites;

    public void BossCheck(int count)
    {
        for (int i = 0; i < count; i++)
        {
            bossSprites[i].GetComponent<ShopItem>().referenceSO.isObtained = true;
        }
    }
}


    
