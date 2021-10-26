using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHotbar : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public GameObject[] slots;
    public GameObject moneyText;

    void Start()
    {
        foreach(GameObject obj in slots)
        {
            Image img = obj.GetComponent<Image>();
            if(img != null)
            {
                //Set icon to empty and hide slot
                img.sprite = null;
                obj.SetActive(false);
            }
            GameObject child = obj.transform.GetChild(0).gameObject;
            if(child != null)
            {
                //Clear Text
                TextMeshProUGUI text = child.GetComponent<TextMeshProUGUI>();
                text.text = "";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //TODO Change this to update only when changed
        for(int i = 0; i < 5; i++)
        {
            GameObject slot = slots[i];
            GameObject child = slot.transform.GetChild(0).gameObject;
            PlayerInventory.FishStack stack = playerInventory.getFishItem(i);
            Image img = slot.GetComponent<Image>();
            if(stack == null || !stack.isValid())
            {
                //Set icon to empty, hide slot, and clear text
                img.sprite = null;
                slot.SetActive(false);
                if (child != null)
                {
                    TextMeshProUGUI text = child.GetComponent<TextMeshProUGUI>();
                    text.text = "";
                }
            } else
            {
                //Set icon to item's icon and update text count
                img.sprite = stack.item.sprite;
                slot.SetActive(true);
                if (child != null)
                {
                    TextMeshProUGUI text = child.GetComponent<TextMeshProUGUI>();
                    text.text = ""+stack.count;
                }
            }
        }

        //Update money total
        if(moneyText != null)
        {
            TextMeshProUGUI text = moneyText.GetComponent<TextMeshProUGUI>();
            if(text != null)
            {
                float moneyRounded = (float)Math.Round((double)playerInventory.money, 2);
                text.text = "$" + playerInventory.money.ToString("F2");
            }
        }
    }
}
