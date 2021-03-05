using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public ShopItemSO referenceSO;
    public GameObject[] mySprites;

    private void Awake()
    {
        mySprites[0].GetComponent<Image>().sprite = referenceSO.sprite;
        
        UpdateShopUI();
    }

    public void UpdateShopUI()
    {
        for (int i = 0; i < mySprites.Length; i++)
        {
            mySprites[i].gameObject.SetActive(false);
        }

        if (!referenceSO.isObtained)
            mySprites[1].gameObject.SetActive(true);
        else
            mySprites[0].gameObject.SetActive(true);
    }

    public void SetSelectedShopItem()
    {
        ControllerScript.selectedSHopItem = gameObject;
        ControllerScript.instance.ShopButtonCheck();
        if (!referenceSO.isObtained)
            return;

        ControllerScript.selectedCursorSprite = referenceSO.index;
        Sprite selectedSprite = mySprites[0].GetComponent<Image>().sprite;
        ControllerScript.instance.UI_Controller.UpdateCursorIcon(selectedSprite);

    }
}
