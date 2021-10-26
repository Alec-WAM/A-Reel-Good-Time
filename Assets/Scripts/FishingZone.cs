using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingZone : MonoBehaviour
{
    public List<FishItem> fishLoot;

    public PlayerInventory playerInventory;

    public void onClick()
    {
        //TODO Add fishing minigame here
        giveRandomLoot();
    }

    //Pick random fish in fish loot list and give it to the player
    private void giveRandomLoot()
    {
        if (fishLoot != null)
        {
            var fishIndex = Random.Range(0, fishLoot.Count); //Get Random item
            FishItem item = fishLoot[fishIndex];

            PlayerInventory.FishStack stack = new PlayerInventory.FishStack(item, 1);
            if (playerInventory.addFishItem(stack))
            {
                //Item Successfully added
                Debug.Log(item.name);
            }
            else
            {
                Debug.Log("Inv Full");
            }
        }
    }
}
