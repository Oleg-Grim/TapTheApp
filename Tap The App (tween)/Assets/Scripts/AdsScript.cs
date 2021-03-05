using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsScript : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem PS;
    [SerializeField]
    private SpriteRenderer adBody;
    private bool collected = false;


    private void Update()
    {
        if (transform.parent != null)
            return;

        collected = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collected)
            return;
        collected = true;
        ControllerScript.currency++;
        adBody.enabled = false;
        PS.Play();
        ControllerScript.instance.UpdateScore();
        StartCoroutine("Death");
    } 

    private IEnumerator Death()
    {
        yield return new WaitForSecondsRealtime(2);
        Destroy(PS.gameObject);
        Destroy(gameObject);
    }
}
