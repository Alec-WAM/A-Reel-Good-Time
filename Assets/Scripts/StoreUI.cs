using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreUI : MonoBehaviour
{
    public int storeTab = 0;
    private PlayerInventory playerInventory;

    public Button sellButton;
    public Button rodButton;
    public Button lureButton;
    public Button upgradeButton;

    public GameObject listContent;
    public GameObject itemPrefab;
    
    public GameObject sellItemTab;
    public int sellSlot = 0;

    public float lakeRodPrice = 50;
    public float riverRodPrice = 100;
    public float oceanRodPrice = 250;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerInventory = player.GetComponent<PlayerInventory>();
        switchTab(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (storeTab == 0)
        {
            //Sell Tab
            GameObject sellTab = listContent.transform.GetChild(0) !=null ? listContent.transform.GetChild(0).gameObject : null;
            if(sellTab != null)
            {
                GameObject fishSlots = sellTab.transform.GetChild(0).gameObject;
                GameObject selectionImage = fishSlots.transform.GetChild(1).gameObject;
                selectionImage.transform.localPosition = new Vector3(-160 + (80 * sellSlot), 0, 0);
            }
        }
    }

    public void reloadTab()
    {
        switchTab(this.storeTab);
    }

    public void clickSell()
    {
        switchTab(0);
    }

    public void clickRod()
    {
        switchTab(1);
    }

    public void clickLure()
    {
        switchTab(2);
    }

    public void clickUpgrade()
    {
        switchTab(3);
    }

    public void switchTab(int tab)
    {
        this.storeTab = tab;
        if(tab == 0)
        {
            sellButton.enabled = false;
            rodButton.enabled = true;
            lureButton.enabled = true;
            upgradeButton.enabled = true;

            foreach (Transform child in listContent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            GameObject sellTab = Instantiate(sellItemTab);
            sellTab.transform.SetParent(listContent.transform, false);
            GameObject fishSlots = sellTab.transform.GetChild(0).gameObject;
            GameObject fishSlotList = fishSlots.transform.GetChild(0).gameObject;

            GameObject selectionImage = fishSlots.transform.GetChild(1).gameObject;
            GameObject selectionDecrButton = fishSlots.transform.GetChild(2).gameObject;
            GameObject selectionIncrButton = fishSlots.transform.GetChild(3).gameObject;

            for (int i = 0; i < 5; i++)
            {
                GameObject slot = fishSlotList.transform.GetChild(i).gameObject;
                GameObject child = slot.transform.GetChild(0).gameObject;
                PlayerInventory.FishStack stack = playerInventory.getFishItem(i);
                Image img = slot.GetComponent<Image>();
                if (stack == null || !stack.isValid())
                {
                    //Set icon to empty, hide slot, and clear text
                    img.sprite = null;
                    slot.SetActive(false);
                    if (child != null)
                    {
                        TextMeshProUGUI text = child.GetComponent<TextMeshProUGUI>();
                        text.text = "";
                    }
                }
                else
                {
                    //Set icon to item's icon and update text count
                    img.sprite = stack.item.sprite;
                    slot.SetActive(true);
                    if (child != null)
                    {
                        TextMeshProUGUI text = child.GetComponent<TextMeshProUGUI>();
                        text.text = "" + stack.count;
                    }
                }
            }

            selectionImage.transform.localPosition = new Vector3(-160 + (80 * sellSlot), 0, 0);

            if (selectionDecrButton != null)
            {
                Button button = selectionDecrButton.GetComponent<Button>();
                button.onClick.AddListener(delegate { decreaseSellSlot(); });
            }
            if (selectionIncrButton != null)
            {
                Button button = selectionIncrButton.GetComponent<Button>();
                button.onClick.AddListener(delegate { increaseSellSlot(); });
            }
        }
        if (tab == 1)
        {
            sellButton.enabled = true;
            rodButton.enabled = false;
            lureButton.enabled = true;
            upgradeButton.enabled = true;

            foreach (Transform child in listContent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            Debug.Log(playerInventory.currentRod);

            int itemY = 0;
            if (playerInventory.currentRod == PlayerInventory.RodType.POND)
            {
                GameObject lakeRod = Instantiate(itemPrefab);
                lakeRod.transform.SetParent(listContent.transform, false);
                GameObject lakeRodButton = lakeRod.transform.GetChild(0).gameObject;
                GameObject lakeRodPriceText = lakeRod.transform.GetChild(1).gameObject;
                GameObject lakeRodName = lakeRod.transform.GetChild(2).gameObject;
                bool canAffordLake = playerInventory.money >= lakeRodPrice;
                if(lakeRodButton != null)
                {
                    Button button = lakeRodButton.GetComponent<Button>();
                    button.enabled = canAffordLake;
                    button.onClick.AddListener(delegate { buyRod(PlayerInventory.RodType.LAKE, lakeRodPrice); });
                }
                if (lakeRodPriceText != null)
                {
                    TextMeshProUGUI text = lakeRodPriceText.GetComponent<TextMeshProUGUI>();
                    text.text = "$" + lakeRodPrice;
                }
                if (lakeRodName != null)
                {
                    TextMeshProUGUI text = lakeRodName.GetComponent<TextMeshProUGUI>();
                    text.text = "Lake Rod";
                }
                itemY += 100;
            }

            if (playerInventory.currentRod != PlayerInventory.RodType.RIVER && playerInventory.currentRod != PlayerInventory.RodType.OCEAN)
            {
                GameObject riverRod = Instantiate(itemPrefab, new Vector3(0, -itemY, 0), Quaternion.identity);
                riverRod.transform.SetParent(listContent.transform, false);
                GameObject riverRodButton = riverRod.transform.GetChild(0).gameObject;
                GameObject riverRodPriceText = riverRod.transform.GetChild(1).gameObject;
                GameObject riverRodName = riverRod.transform.GetChild(2).gameObject;
                bool canAffordRiver = playerInventory.money >= riverRodPrice && playerInventory.currentRod == PlayerInventory.RodType.LAKE;
                if (riverRodButton != null)
                {
                    Button button = riverRodButton.GetComponent<Button>();
                    button.enabled = canAffordRiver;
                    button.onClick.AddListener(delegate { buyRod(PlayerInventory.RodType.RIVER, riverRodPrice); });
                }
                if (riverRodPriceText != null)
                {
                    TextMeshProUGUI text = riverRodPriceText.GetComponent<TextMeshProUGUI>();
                    text.text = "$" + riverRodPrice;
                }
                if (riverRodName != null)
                {
                    TextMeshProUGUI text = riverRodName.GetComponent<TextMeshProUGUI>();
                    text.text = "River Rod";
                }
                itemY += 100;
            }

            if (playerInventory.currentRod != PlayerInventory.RodType.OCEAN)
            {
                GameObject oceanRod = Instantiate(itemPrefab, new Vector3(0, -itemY, 0), Quaternion.identity);
                oceanRod.transform.SetParent(listContent.transform, false);
                GameObject oceanRodButton = oceanRod.transform.GetChild(0).gameObject;
                GameObject oceanRodPriceText = oceanRod.transform.GetChild(1).gameObject;
                GameObject oceanRodName = oceanRod.transform.GetChild(2).gameObject;
                bool canAffordOcean = playerInventory.money >= oceanRodPrice && playerInventory.currentRod == PlayerInventory.RodType.RIVER;
                if (oceanRodButton != null)
                {
                    Button button = oceanRodButton.GetComponent<Button>();
                    button.enabled = canAffordOcean;
                    button.onClick.AddListener(delegate { buyRod(PlayerInventory.RodType.OCEAN, oceanRodPrice); });
                }
                if (oceanRodPriceText != null)
                {
                    TextMeshProUGUI text = oceanRodPriceText.GetComponent<TextMeshProUGUI>();
                    text.text = "$" + oceanRodPrice;
                }
                if (oceanRodName != null)
                {
                    TextMeshProUGUI text = oceanRodName.GetComponent<TextMeshProUGUI>();
                    text.text = "Ocean Rod";
                }
            }
        }
        if (tab == 2)
        {
            sellButton.enabled = true;
            rodButton.enabled = true;
            lureButton.enabled = false;
            upgradeButton.enabled = true;

            foreach (Transform child in listContent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        if (tab == 3)
        {
            sellButton.enabled = true;
            rodButton.enabled = true;
            lureButton.enabled = true;
            upgradeButton.enabled = false;

            foreach (Transform child in listContent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    public void increaseSellSlot()
    {
        Debug.Log("Increase SellSlot");
        if (sellSlot < 4)
        {
            sellSlot++;
        }
    }

    public void decreaseSellSlot()
    {
        Debug.Log("Decrease SellSlot");
        if (sellSlot > 0)
        {
            sellSlot--;
        }
    }

    public void closeStoreUI()
    {
        gameObject.SetActive(false);
    }

    public void buyRod(PlayerInventory.RodType rod, float money)
    {
        if(playerInventory.money >= money)
        {
            playerInventory.currentRod = rod;
            playerInventory.money -= money;
            playerInventory.writeToSave();
            reloadTab();
        }
    }
}
