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

        public bool isValid()
        {
            return item != null && count > 0;
        }
    }
    
    public FishStack[] fishSlots;

    public FishStack getFishItem(int slot)
    {
        if (slot < 0 || slot > fishSlots.Length) return null;
        return fishSlots[slot];
    }

    public bool addFishItem(FishStack stack)
    {
        int emptyIndex = -1;
        for(int i = 0; i < fishSlots.Length; i++)
        {
            FishStack otherStack = fishSlots[i];
            if(otherStack !=null && otherStack.isValid())
            {
                if (otherStack.item.id == stack.item.id)
                {
                    otherStack.count += stack.count;
                    return true;
                }
            } else
            {
                if(emptyIndex == -1)emptyIndex = i;
            }
        }
        if(emptyIndex != -1)
        {
            fishSlots[emptyIndex] = stack;
            return true;
        }
        return false;
    }

}
