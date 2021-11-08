using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishingZone : MonoBehaviour
{
    public PlayerInventory.RodType rodType;
    public List<FishItem> fishLoot;

    public PlayerInventory playerInventory;
    public GameObject uiFishCaught;
    public PlayerInventory.FishStack currentCaughtFish;
    public string hookedFish;

    public void onClick()
    {
        //TODO Add fishing minigame here
        if (hookedFish == null || hookedFish == "")
        {
            giveRandomLoot();
        }
    }

    public bool canFishHere()
    {
        PlayerInventory.RodType playerRod = playerInventory.currentRod;
        if(rodType == PlayerInventory.RodType.POND)
        {
            return true;
        }
        if (rodType == PlayerInventory.RodType.LAKE)
        {
            return playerRod != PlayerInventory.RodType.POND;
        }
        if (rodType == PlayerInventory.RodType.RIVER)
        {
            return playerRod != PlayerInventory.RodType.POND && playerRod != PlayerInventory.RodType.LAKE;
        }
        if (rodType == PlayerInventory.RodType.OCEAN)
        {
            return playerRod == PlayerInventory.RodType.OCEAN;
        }
        return false;
    }

    //Pick random fish in fish loot list and give it to the player
    private void giveRandomLoot()
    {
        if (!canFishHere())
        {
            Debug.Log("You can't fish here");
            return;
        }
        if (fishLoot != null && canFishHere())
        {
            var fishIndex = Random.Range(0, fishLoot.Count); //Get Random item
            FishItem item = fishLoot[fishIndex];

            PlayerInventory.FishStack stack = new PlayerInventory.FishStack(item, 1);
            catchFish(stack);
        }
    }

    public void catchFish(PlayerInventory.FishStack stack)
    {
        uiFishCaught.SetActive(true);
        currentCaughtFish = stack;
        GameObject imgObject = uiFishCaught.transform.GetChild(0).gameObject;
        GameObject textObject = uiFishCaught.transform.GetChild(1).gameObject;

        if(imgObject != null)
        {
            Image img = imgObject.GetComponent<Image>();
            img.sprite = stack.item.sprite;
        }
        if(textObject != null)
        {
            TextMeshProUGUI text = textObject.GetComponent<TextMeshProUGUI>();
            text.text = stack.item.name;
        }

        hookedFish = stack.item.id;
    }

    public void keepFish()
    {
        if (playerInventory.addFishItem(currentCaughtFish))
        {
            //Item Successfully added
            Debug.Log(currentCaughtFish.item.name);
        }
        else
        {
            Debug.Log("Inv Full");
        }
        hookedFish = null;
        uiFishCaught.SetActive(false);
    }

    public void releaseFish()
    {
        hookedFish = null;
        uiFishCaught.SetActive(false);
    }
}
