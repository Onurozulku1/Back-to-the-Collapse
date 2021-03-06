using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Item item;

    private void Awake()
    {
        if (item != null)
        {
            GetComponentInChildren<Image>().sprite = item.Icon;
        }
    }
}
