using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishingZone : MonoBehaviour
{
    public PlayerInventory.RodType rodType;
    public List<FishItem> commonFishLoot;
    public List<FishItem> rareFishLoot;
    public List<FishItem> legendaryFishLoot;

    public Texture2D cursorRod;
    public Texture2D cursorCantFish;

    public PlayerInventory playerInventory;
    public GameObject uiFishCaught;
    public GameObject uiFishingGame;
    public PlayerInventory.FishStack currentCaughtFish;
    public string hookedFish;
    public int hookedFishTier = 0; //0 = Common, 1 = Rare, 2 = Legendary
    public float increaseSpeed = 0.5f;
    public float decreaseSpeed = 0.1f;
    public float tensionIncrease = 0.6f;
    public float tensionDecrease = 0.75f;

    public GameObject uiWarning;

    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorRod, Vector2.zero, CursorMode.Auto);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void Update()
    {
        GameObject progressBar = uiFishingGame.transform.GetChild(0).gameObject;
        GameObject tensionBar = uiFishingGame.transform.GetChild(3).gameObject;
        Slider tension = tensionBar.GetComponent<Slider>();
        Slider value = progressBar.GetComponent<Slider>();
        if (uiFishingGame.activeSelf == true)
        {
            if (Input.GetKey(KeyCode.E))
            {
                value.value += increaseSpeed * Time.deltaTime;
                tension.value += tensionIncrease * Time.deltaTime;
            }
            else
            {
                value.value -= decreaseSpeed * Time.deltaTime;
                tension.value -= tensionDecrease * Time.deltaTime;
            }
            if (value.value == 0f || tension.value == 1f)
            {
                uiFishingGame.SetActive(false);
            }
            else if(value.value == 1f)
            {
                uiFishingGame.SetActive(false);
                catchFish(currentCaughtFish);
            }
        }
    }
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
            if(uiWarning != null)
            {
                TextMeshProUGUI text = uiWarning.GetComponent<TextMeshProUGUI>();
                text.text = "You need " + (rodType == PlayerInventory.RodType.OCEAN ? "an " : "a ") + rodType + " Rod";
                text.color = new Color(255F, 0F, 0F, 255F);
            }
            StartCoroutine(warningTask());
            return;
        }
        if (canFishHere())
        {
            FishItem item = null;
            float range = Random.Range(0f, 100f);

            if (range <= 60.0F) //Common Loot
            {
                var fishIndex = Random.Range(0, commonFishLoot.Count); //Get Random item
                item = commonFishLoot[fishIndex];
                hookedFishTier = 0;
            }
            else if (range > 60.0F && range <= 90.0F) //Rare Loot
            {
                var fishIndex = Random.Range(0, rareFishLoot.Count); //Get Random item
                item = rareFishLoot[fishIndex];
                hookedFishTier = 1;
            }
            else if (range > 90.0F) //Legendary Loot
            {
                var fishIndex = Random.Range(0, legendaryFishLoot.Count); //Get Random item
                item = legendaryFishLoot[fishIndex];
                hookedFishTier = 2;
            }

            PlayerInventory.FishStack stack = new PlayerInventory.FishStack(item, 1);
            fishingMinigame(stack);
        }
    }

    public IEnumerator warningTask()
    {
        uiWarning.SetActive(true);
        yield return new WaitForSeconds(3.0F);
        uiWarning.SetActive(false);
    }

    public void fishingMinigame(PlayerInventory.FishStack stack)
    {
        Debug.Log("Got here");
        uiFishingGame.SetActive(true);
        GameObject progressBar = uiFishingGame.transform.GetChild(0).gameObject;
        GameObject tensionBar = uiFishingGame.transform.GetChild(3).gameObject;
        Slider value = progressBar.GetComponent<Slider>();
        Slider tension = tensionBar.GetComponent<Slider>();
        value.value = 0.25f;
        tension.value = 0f;
        currentCaughtFish = stack;
        if(hookedFishTier == 0)
        {
            increaseSpeed = 0.4f;
            decreaseSpeed = 0.1f;
            tensionIncrease = 0.6f;
            tensionDecrease = 0.5f;
        }
        else if(hookedFishTier == 1)
        {
            increaseSpeed = 0.3f;
            decreaseSpeed = 0.2f;
            tensionIncrease = 0.6f;
            tensionDecrease = 0.5f;
        }
        else if(hookedFishTier == 2)
        {
            increaseSpeed = 0.2f;
            decreaseSpeed = 0.15f;
            tensionIncrease = 0.8f;
            tensionDecrease = 0.7f;
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
