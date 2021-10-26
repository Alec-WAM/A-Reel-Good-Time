using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [System.Serializable]
    public class FishStack
    {
        public FishItem item;
        public int count;

        public FishStack(FishItem item, int count)
        {
            this.item = item;
            this.count = count;
        }

        //Is item real and has a valid count?
        public bool isValid()
        {
            return item != null && count > 0;
        }
    }
    
    public FishStack[] fishSlots;
    public float money;

    void Update()
    {
        //Debugging Sell function to allow pressing I + SlotNum to sell that item
        if (Input.GetKey(KeyCode.I))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                sellFishItem(0, 1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                sellFishItem(1, 1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                sellFishItem(2, 1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                sellFishItem(3, 1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                sellFishItem(4, 1);
            }
        }
    }

    /*
     * returns null if slot is invalid otherwise return stack in slot
     */
    public FishStack getFishItem(int slot)
    {
        if (slot < 0 || slot > fishSlots.Length) return null;
        return fishSlots[slot];
    }

    /*
     * Add stack to inventory
     * returns true if item fit and false if inventory could not fit the item
     */
    public bool addFishItem(FishStack stack)
    {
        int emptyIndex = -1; //Index of first empty slot
        for(int i = 0; i < fishSlots.Length; i++)
        {
            FishStack otherStack = fishSlots[i];
            if(otherStack !=null && otherStack.isValid())
            {
                if (otherStack.item.id == stack.item.id) //Check if the same item
                {
                    otherStack.count += stack.count; //Increase already exisiting slots size
                    return true;
                }
            } else
            {
                if (emptyIndex == -1)
                {
                    //If we have not already found an empty slot mark this one
                    emptyIndex = i;
                }
            }
        }
        //Stack does not exist so add it to the inventory
        if(emptyIndex != -1)
        {
            //We have room
            fishSlots[emptyIndex] = stack;
            return true;
        }
        //Inventory is full
        return false;
    }

    public void sellFishItem(int slot, int amt)
    {
        FishStack stack = getFishItem(slot);
        if(stack !=null && stack.isValid())
        {
            int realAmount = Mathf.Min(amt, stack.count);
            float soldMoney = realAmount * stack.item.value;
            money += soldMoney;
            stack.count -= realAmount;
            if(stack.count <= 0)
            {
                fishSlots[slot] = null;
            }
        }
    }

}
