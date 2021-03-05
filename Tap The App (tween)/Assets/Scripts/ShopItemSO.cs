using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ShopItem")]
public class ShopItemSO : ScriptableObject
{
    public int index;
    public int price;
    public bool isBoss;
    public bool isObtained;
    public Sprite sprite;
}
