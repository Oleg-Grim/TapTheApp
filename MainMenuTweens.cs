using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuTweens : MonoBehaviour
{
    public GameObject playButton;
    public GameObject shopButton;

    public GameObject tap;
    public Vector2 tapPos;
    public GameObject the;
    public Vector2 thePos;
    public GameObject app;
    public Vector2 appPos;
    public GameObject hiscoreText;

    private void Awake()
    {
        StartCoroutine("TweenIn");
    }

    private IEnumerator TweenIn()
    {
        yield return new WaitForSecondsRealtime(0.1f);

        LeanTween.moveLocal(app, appPos, 0.2f);
        yield return new WaitForSecondsRealtime(0.2f);
        LeanTween.moveLocal(tap, tapPos, 0.2f);
        yield return new WaitForSecondsRealtime(0.3f);
        LeanTween.moveLocal(the, thePos, 0.3f);
        yield return new WaitForSecondsRealtime(0.3f);
        LeanTween.moveLocalX(playButton, 0f, 0.3f);
        yield return new WaitForSecondsRealtime(0.1f);
        LeanTween.moveLocalX(shopButton, 0, 0.3f);
        LeanTween.scaleX(hiscoreText, 1, 0.3f);

        StopCoroutine("TweenIn");
    }

    public void PressStart()
    {
        StartCoroutine("TweenOut");
    }

    private IEnumerator TweenOut()
    {
        LeanTween.moveLocalX(app, 3000f, 0.25f);
        LeanTween.moveLocalY(tap, 2000, 0.25f);
        LeanTween.moveLocalX(the, -3000, 0.25f);
        LeanTween.moveLocalX(playButton, -3000f, 0.25f);
        LeanTween.moveLocalX(shopButton, 3000, 0.25f);
        LeanTween.scaleX(hiscoreText, 0, 0.25f);
        yield return new WaitForSecondsRealtime(0.25f);
        ControllerScript.instance.ReloadTheGame();
        StopCoroutine("TweenOut");
    }

}
