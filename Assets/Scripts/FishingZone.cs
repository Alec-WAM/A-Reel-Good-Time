using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingZone : MonoBehaviour
{
    public List<FishItem> fishLoot;

    public PlayerInventory playerInventory;

    void onStart()
    {
        //GameObject player = GameObject.FindWithTag("Player");
        //playerInventory = player.GetComponent<PlayerInventory>();
    }

    public void onClick()
    {
        giveRandomLoot();
    }

    private void giveRandomLoot()
    {
        if (fishLoot != null)
        {
            var fishIndex = Random.Range(0, fishLoot.Count);
            FishItem item = fishLoot[fishIndex];
            PlayerInventory.FishStack stack = new PlayerInventory.FishStack(item, 1);
            if (playerInventory.addFishItem(stack))
            {
                Debug.Log(item.name);
            }
            else
            {
                Debug.Log("Inv Full");
            }
        }
    }
}
