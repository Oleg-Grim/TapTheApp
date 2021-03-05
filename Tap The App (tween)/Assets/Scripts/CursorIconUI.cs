using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorIconUI : MonoBehaviour
{
    [SerializeField] private Transform[] icons;
    [SerializeField] private Color usedColor;

    private void Awake()
    {
        icons = new Transform[transform.childCount];

        for (int i = 0; i < icons.Length; i++)
        {
            icons[i] = transform.GetChild(i);
        }
    }

    public void SetHitCounter(int count)
    {
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < count; i++)
        {
            icons[i].gameObject.SetActive(true);
        }

    }

    public void UpdateHitCounter(int count)
    {
        icons[count].GetComponent<Image>().color = usedColor;
    }
}
