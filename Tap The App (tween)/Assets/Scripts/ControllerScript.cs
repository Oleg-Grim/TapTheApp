using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerScript : MonoBehaviour
{

    public static int levelIndex;
    public Sprite preset;
    public static int selectedCursorSprite;

    [SerializeField]
    private ParticleSystem deathParticle;
    public GameObject theTarget;

    public static GameObject selectedSHopItem;

    public UI_Controller UI_Controller;

    public static int score;
    public static int highscore;
    public static int currency;
    public static int stage;
    public static int highStage;

    private int cursorsCount;

    [SerializeField]
    private LevelsList level;
    private static bool gameplay;

    public Vector2 spawnPos;
    public GameObject toSpawn;

    public CursorIconUI cursorIcons;

    public static ControllerScript instance;

    private void Awake()
    {
        InitSprites();
        LoadGame();
        instance = this;
        UpdateScore();
        UI_Controller.RecordScores(highscore, highStage);
    }

    private void Start()
    {


        if (!gameplay)
        {
            UI_Controller.SwapMenus(0);
            theTarget.SetActive(false);
        }
        else
        {
            SetHitCounter();
            InstantiateCursor();
        }
    }

    public void InstantiateCursor()
    {
        if (cursorsCount > 0)
        {

            cursorsCount--;
            Instantiate(toSpawn, spawnPos, Quaternion.identity);
        }
        else
        {
            if (level.Levels[levelIndex].BossLevel)
            {
                BossDefeat();
                BossCounterSwitch();
            }
            else
            {
                CompletedLevel();
            }

            SafetySwitch();
            RestartGame(false);
        }
    }

    public void RestartGame(bool reset)
    {
        if (reset)
        {
            StartCoroutine("LosePattern");
        }
        else
        {
            cursorIcons.UpdateHitCounter(cursorsCount);
            
            StartCoroutine("WinPattern");
        }
    }

    private void SetHitCounter()
    {
        cursorsCount = level.Levels[levelIndex].hitCouter;
    }

    public void ResetHitCounter()
    {
        cursorsCount = 0;
    }

    private void SafetySwitch()
    {
        if (levelIndex + 1 < level.Levels.Count)
        {
            levelIndex++;
        }
        else
        {
            levelIndex = 0;
        }
    }

    public void UpdateScore()
    {
        UI_Controller.levelScoreText.text = score.ToString();
        UI_Controller.currencyText.text = currency.ToString();

        if (cursorsCount != 0)
        {
            cursorIcons.UpdateHitCounter(cursorsCount);
        }
        else
        {
            cursorIcons.SetHitCounter(level.Levels[levelIndex].hitCouter);
        }
    }

    private IEnumerator LosePattern()
    {
        highscore = highscore < score ? score : highscore;
        highStage = highStage < stage ? stage : highStage;

        UI_Controller.EndScore(score, stage);
        ResetHitCounter();

        yield return new WaitForSecondsRealtime(2);

        score = 0;
        stage = 0;
        levelIndex = 0;
        UI_Controller.SwapMenus(2);
    }

    private IEnumerator WinPattern()
    {
        stage++;
        FindObjectOfType<TargetRotation>().ReleaseCursors();
        deathParticle.Play();
        

        if (level.Levels[levelIndex].BossLevel)
        {
            BossWarning();
        }

        yield return new WaitForSecondsRealtime(2);
        ReloadTheGame();
    }

    [SerializeField] private GameObject bossWarningText;
    [SerializeField] private GameObject bossDefeatText;
    [SerializeField] private GameObject bossWarningPanel;
    [SerializeField] private GameObject completed;


    private void BossWarning()
    {
        StartCoroutine("BossWarningTween");
        StopCoroutine("Completed");
    }

    private void BossDefeat()
    {
        StartCoroutine("BossDefeatTween");
    }
    private void CompletedLevel()
    {
        StartCoroutine("Completed");
    }

    private IEnumerator BossWarningTween()
    {
        LeanTween.scaleY(bossWarningPanel, 1, 0.1f); // 0.1
        yield return new WaitForSecondsRealtime(0.2f); //0.3
        LeanTween.moveLocalX(bossWarningText, 0, 0.1f); //0.4
        yield return new WaitForSecondsRealtime(1.2f); // 1.6
        LeanTween.moveLocalX(bossWarningText, 4000, 0.1f); //1.7
        yield return new WaitForSecondsRealtime(0.2f);  //1.9
        LeanTween.scaleY(bossWarningPanel, 0f, 0.15f); // 2.15
        yield return new WaitForSecondsRealtime(0.2f);
        LeanTween.moveLocalX(bossWarningText, -4000, 0.12f);

        StopCoroutine("BossDefeatTween");
    }

    private IEnumerator BossDefeatTween()
    {
        LeanTween.scaleY(bossWarningPanel, 1, 0.1f);
        yield return new WaitForSecondsRealtime(0.2f);
        LeanTween.moveLocalX(bossDefeatText, 0, 0.1f);
        yield return new WaitForSecondsRealtime(1.2f);
        LeanTween.moveLocalX(bossDefeatText, -4000, 0.1f);
        yield return new WaitForSecondsRealtime(0.2f);
        LeanTween.scaleY(bossWarningPanel, 0f, 0.15f);
        yield return new WaitForSecondsRealtime(0.2f);
        LeanTween.moveLocalX(bossDefeatText, 4000, 0.12f);

        StopCoroutine("BossDefeatTween");
    }

    private string[] grats = new string[]{ "GREAT!", "GOOD JOB!", "AWESOME!", "YOU DID IT!", "NICELY DONE!", "YEAH!", "YAY!", "TAP! TAP! TAP!", };
    private IEnumerator Completed()
    {
        LeanTween.scaleY(bossWarningPanel, 1, 0.1f);
        yield return new WaitForSecondsRealtime(0.2f);
        float completedPos = completed.transform.position.x;
        completed.GetComponent<Text>().text = grats[UnityEngine.Random.Range(0, grats.Length)];
        LeanTween.moveLocalX(completed, 0, 0.1f);
        yield return new WaitForSecondsRealtime(1.2f);
        
        if(completedPos > 0)
        {
            LeanTween.moveLocalX(completed, -4000, 0.1f);
        }
        else
        {
            LeanTween.moveLocalX(completed, 4000, 0.1f);
        }
        
        yield return new WaitForSecondsRealtime(0.2f);
        LeanTween.scaleY(bossWarningPanel, 0f, 0.15f);

        StopCoroutine("Completed");
    }

    public void ReloadTheGame()
    {
        gameplay = true;
        SetHitCounter();
        InstantiateCursor();
        theTarget.SetActive(true);
        SaveGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public static int soundVolume;
    public static int vibrationsEnabled;
    private void SaveGame()
    {
        PlayerPrefs.SetInt("Currency", currency);
        PlayerPrefs.SetInt("Highscore", highscore);
        PlayerPrefs.SetInt("Highstage", highStage);
        PlayerPrefs.SetInt("BossCounter", bossCounter);
        PlayerPrefs.SetInt("CursorSprite", selectedCursorSprite);

        for (int i = 0; i < obtainedSprites.Length; i++)
        {
            PlayerPrefs.SetInt("sprite" + i.ToString(), obtainedSprites[i]);
        }
    }

    private void LoadGame()
    {
        currency = PlayerPrefs.GetInt("Currency");
        highscore = PlayerPrefs.GetInt("Highscore");
        highStage = PlayerPrefs.GetInt("Highstage");
        bossCounter = PlayerPrefs.GetInt("BossCounter");
        selectedCursorSprite = PlayerPrefs.GetInt("CursorSprite");

        LoadSprites();
    }


    private void OnApplicationPause()
    {
        if(UI_Controller.activeUI.name != "(1)Gameplay_Holder")
        {
            SaveGame();
        }
    }

    private void OnApplicationQuit()
    {
        if (UI_Controller.activeUI.name != "(1)Gameplay_Holder")
        {
            SaveGame();
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (UI_Controller.activeUI.name != "(1)Gameplay_Holder")
        {
            SaveGame();
        }
    }

    public void BuyItem()
    {
        if (currency < selectedSHopItem.GetComponent<ShopItem>().referenceSO.price)
        {
            UI_Controller.ShakeButton();
        }
        else
        {
            currency -= selectedSHopItem.GetComponent<ShopItem>().referenceSO.price;
            UpdateScore();
            selectedSHopItem.GetComponent<ShopItem>().referenceSO.isObtained = true;
            selectedSHopItem.GetComponent<ShopItem>().UpdateShopUI();
            UI_Controller.UpdateCursorIcon(selectedSHopItem.GetComponent<ShopItem>().mySprites[0].GetComponent<Image>().sprite);
            selectedCursorSprite = selectedSHopItem.GetComponent<ShopItem>().referenceSO.index;
            obtainedSprites[selectedSHopItem.GetComponent<ShopItem>().referenceSO.index] = 1;
            UI_Controller.TweenButton(false, 0);
        }
    }

    public void ShopButtonCheck()
    {

        int cost = selectedSHopItem.GetComponent<ShopItem>().referenceSO.price;

        if (selectedSHopItem.GetComponent<ShopItem>().referenceSO.isBoss || selectedSHopItem.GetComponent<ShopItem>().referenceSO.isObtained)
            UI_Controller.TweenButton(false, cost);
        else
            UI_Controller.TweenButton(true, cost);
    }

    public static int bossCounter = 0;

    public void BossCounterSwitch()
    {
        if (bossCounter < UI_Controller.bossSprites.Length)
        {
            bossCounter++;
        }
        else
        {
            bossCounter = UI_Controller.bossSprites.Length;
        }
    }

    public AudioSource clickSound;

    public void PlaySound()
    {
        clickSound.PlayOneShot(clickSound.clip);
    }

    public int[] obtainedSprites;
    private void InitSprites()
    {
        obtainedSprites = new int[shopItems.Length];

        for (int i = 0; i < obtainedSprites.Length; i++)
        {
            if (shopItems[i].GetComponent<ShopItem>().referenceSO.isObtained)
            {
                obtainedSprites[i] = 1;
            }
            else
            {
                obtainedSprites[i] = 0;
            }
        }
    }

    private void LoadSprites()
    {
        for (int i = 0; i < obtainedSprites.Length; i++)
        {
            if (obtainedSprites[i] == 1)
            {
                shopItems[i].GetComponent<ShopItem>().referenceSO.isObtained = true;
            }
            else
            {
                shopItems[i].GetComponent<ShopItem>().referenceSO.isObtained = false;
            }
        }
    }

    public GameObject[] shopItems;
}
