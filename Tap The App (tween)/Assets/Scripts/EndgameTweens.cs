using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndgameTweens : MonoBehaviour
{
    public GameObject holder;
    public GameObject popup;
    public GameObject restartButton;
    public GameObject shopButton;
    public GameObject homeButton;
    private Vector2 popVector = new Vector2 (1, 1);

    private void Awake()
    {
    }

    private void OnEnable()
    {
        StartCoroutine("TweenIn");
    }

    private IEnumerator TweenIn()
    {
        LeanTween.moveLocalY(holder, 0f, 0.1f);
        yield return new WaitForSecondsRealtime(0.2f);
        LeanTween.moveLocalX(homeButton, -570f, 0.2f);
        yield return new WaitForSecondsRealtime(0.15f);
        LeanTween.scale(popup, popVector, 0.1f);

        LeanTween.moveLocalX(restartButton, 0, 0.4f);
        LeanTween.moveLocalX(shopButton, 0, 0.4f);
        yield return new WaitForSecondsRealtime(1);
        LeanTween.moveLocalY(restartButton, -1000f, 0.2f);
        StopCoroutine("TweenIn");
    }

    public void RestartButton()
    {
        StartCoroutine("TweenOut");
    }

    private IEnumerator TweenOut()
    {
        LeanTween.scale(holder, Vector2.zero, 0.15f);
        yield return new WaitForSecondsRealtime(0.15f);
        ControllerScript.instance.ReloadTheGame();
        StopCoroutine("TweenOut");
    }
}
