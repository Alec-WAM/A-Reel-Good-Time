using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerInventory : MonoBehaviour
{
    private static string PLAYER_FISH_KEY = "PLAYER_FISH";
    private static string PLAYER_MONEY_KEY = "PLAYER_MONEY";
    private static string PLAYER_ROD_KEY = "PLAYER_ROD";
    private static string PLAYER_LURE_KEY = "PLAYER_LURE";
    private static string PLAYER_LURES_BOUGHT_KEY = "PLAYER_LURES_BOUGHT";

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

    [CustomEditor(typeof(PlayerInventory))]
    public class customButton : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            PlayerInventory myScript = (PlayerInventory)target;
            if (GUILayout.Button("Clear Inventory"))
            {
                myScript.clearInventory();
            }
            if (GUILayout.Button("Lake Rod"))
            {
                myScript.currentRod = RodType.LAKE;
                myScript.writeToSave();
            }
            if (GUILayout.Button("River Rod"))
            {
                myScript.currentRod = RodType.RIVER;
                myScript.writeToSave();
            }
            if (GUILayout.Button("Ocean Rod"))
            {
                myScript.currentRod = RodType.OCEAN;
                myScript.writeToSave();
            }
        }

    }

    public enum RodType
    {
        POND, LAKE, RIVER, OCEAN
    }

    public FishDictionary fishDictionary;

    public FishStack[] fishSlots;
    public float money;
    public RodType currentRod = RodType.POND;

    void Start()
    {
        loadFromSave();
    }

    void writeToSave()
    {
        string fishString = "";
        for(int i = 0; i < fishSlots.Length; i++)
        {
            FishStack stack = fishSlots[i];
            if (i != 0)
            {
                fishString += ",";
            }
            if (stack == null || !stack.isValid())
            {
                fishString += "empty";
            }
            else
            {
                fishString += (stack.item.id) + "x" + stack.count;
            }
        }
        PlayerPrefs.SetString(PLAYER_FISH_KEY, fishString);
        PlayerPrefs.SetFloat(PLAYER_MONEY_KEY, money);
        PlayerPrefs.SetString(PLAYER_ROD_KEY, currentRod.ToString());
        PlayerPrefs.Save();
    }

    void loadFromSave()
    {
        if (PlayerPrefs.HasKey(PLAYER_FISH_KEY))
        {
            string fishString = PlayerPrefs.GetString(PLAYER_FISH_KEY);
            string[] fishList = fishString.Split(',');
            fishSlots = new FishStack[fishSlots.Length];
            for (int i = 0; i < fishList.Length; i++)
            {
                string item = fishList[i];
                if (item == "empty")
                {
                    continue;
                }
                else
                {
                    string[] info = item.Split('x');
                    string id = info[0];
                    string count = info[1];
                    FishItem lookupItem = fishDictionary.getItemByID(id);
                    if (lookupItem != null)
                    {
                        fishSlots[i] = new FishStack(lookupItem, int.Parse(count));
                    }
                }
            }
        }
        if (PlayerPrefs.HasKey(PLAYER_MONEY_KEY))
        {
            money = PlayerPrefs.GetFloat(PLAYER_MONEY_KEY);
        }
        if (PlayerPrefs.HasKey(PLAYER_ROD_KEY))
        {
            string type = PlayerPrefs.GetString(PLAYER_ROD_KEY);
            if (type.ToUpper().Equals("LAKE"))
            {
                currentRod = RodType.LAKE;
            }
            if (type.ToUpper().Equals("RIVER"))
            {
                currentRod = RodType.RIVER;
            }
            if (type.ToUpper().Equals("OCEAN"))
            {
                currentRod = RodType.OCEAN;
            }
        }
    }

    void clearInventory()
    {
        fishSlots = new FishStack[fishSlots.Length];
        money = 0;
        writeToSave();
    }

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
                    writeToSave();
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
            writeToSave();
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
            writeToSave();
        }
    }

}
