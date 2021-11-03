using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDictionary : MonoBehaviour
{
    public List<FishItem> fish;

    public FishItem getItemByID(string id)
    {
        foreach(FishItem item in fish){
            if(item.id == id)
            {
                return item;
            }
        }
        return null;
    }
}
