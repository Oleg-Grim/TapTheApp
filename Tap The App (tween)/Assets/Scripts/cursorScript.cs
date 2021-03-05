using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cursorScript : MonoBehaviour
{

    private bool isThrown = false;
    private bool canControll = true;
    [SerializeField]
    private Rigidbody2D cursorBody;
    [SerializeField]
    private BoxCollider2D cursorCollider;
    [SerializeField] private SpriteRenderer myRend;

    [SerializeField]
    private Vector2 throwForce;
    private void Update()
    {
        myRend.sprite = SpritesAndMats.instance.availableSprites[ControllerScript.selectedCursorSprite];

        if (ControllerScript.instance.UI_Controller.activeUI.name == "(2)EndGame_Holder")
        {
            isThrown = true;
            return;
        }

        if(canControll)
        {
            if(Input.GetMouseButtonDown(0))
            {

                if (isThrown)
                    return;

                cursorBody.AddForce(throwForce, ForceMode2D.Impulse);
                cursorBody.gravityScale = 1;
                isThrown = true;
            }
        }
        ReleaseForce();
    }

    public void ReleaseForce()
    {
        if (transform.parent == null && isThrown)
        {
            cursorBody.bodyType = RigidbodyType2D.Dynamic;
            cursorBody.constraints = RigidbodyConstraints2D.None;
        }
    }

    public ParticleSystem splat;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        splat.Play();
        Vibr();
        if (!isThrown)
            return;

        if (collision.collider.tag == "Target")
        {
            ControllerScript.instance.PlaySound();
            ControllerScript.score++;
            ControllerScript.instance.UpdateScore();
            cursorBody.velocity = new Vector2(0, 0);
            cursorBody.bodyType = RigidbodyType2D.Kinematic;
            transform.SetParent(collision.collider.transform);
            ControllerScript.instance.InstantiateCursor();
            isThrown = true;
        }
        else if (collision.collider.tag == "Cursor")
        {
            ControllerScript.instance.PlaySound();
            canControll = false;
            isThrown = false;
            cursorBody.velocity = new Vector2(cursorBody.velocity.x, -2);
            ControllerScript.instance.RestartGame(true);
            ControllerScript.instance.UpdateScore();
        }
    }

    public void Vibr()
    {
        Handheld.Vibrate();
    }
}
