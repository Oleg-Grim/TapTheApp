using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpritesAndMats : MonoBehaviour
{
    public Sprite[] levelSprites;
    public Sprite[] levelParts;
    public Sprite[] bossSprites;
    public Sprite[] bossParts;

    public static SpritesAndMats instance;

    [SerializeField] private GameObject prefabCursor;
    [SerializeField] private GameObject display;

    public Sprite[] availableSprites;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        prefabCursor.GetComponent<SpriteRenderer>().sprite = availableSprites[ControllerScript.selectedCursorSprite];
        display.GetComponent<Image>().sprite = availableSprites[ControllerScript.selectedCursorSprite];
    }
}
