using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTweens : MonoBehaviour
{
    public UI_Controller UIController;
    public GameObject holder;
    private Vector2 popVector = new Vector2(0, 0);

    private void Awake()
    {

    }

    private void OnEnable()
    {
        StartCoroutine("TweenIn");
    }

    private IEnumerator TweenIn()
    {
        LeanTween.moveLocalX(holder, 0f, 0.1f);
        yield return new WaitForSecondsRealtime(1.5f);
        StopCoroutine("TweenIn");
    }

    public void HomeButton()
    {
        StartCoroutine("TweenOut");
    }

    private IEnumerator TweenOut()
    {
        LeanTween.moveLocalX(holder, 1500, 0.1f);
        yield return new WaitForSecondsRealtime(0.1f);
        UIController.SwapMenus(0);
        StopCoroutine("TweenOut");
    }
}
