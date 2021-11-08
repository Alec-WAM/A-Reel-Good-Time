using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreUI : MonoBehaviour
{
    public int storeTab = 0;

    public Button sellButton;
    public Button rodButton;
    public Button lureButton;
    public Button upgradeButton;

    public GameObject listContent;
    public GameObject itemPrefab;

    public float lakeRodPrice = 50;
    public float riverRodPrice = 100;
    public float oceanRodPrice = 250;

    // Start is called before the first frame update
    void Start()
    {
        switchTab(0);
    }

    // Update is called once per frame
    void Update()
    {
        
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

            GameObject lakeRod = Instantiate(itemPrefab);
            lakeRod.transform.SetParent(listContent.transform, false);
            GameObject lakeRodButton = lakeRod.transform.GetChild(0).gameObject;
            GameObject lakeRodPriceText = lakeRod.transform.GetChild(1).gameObject;
            GameObject lakeRodName = lakeRod.transform.GetChild(2).gameObject;
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

            GameObject riverRod = Instantiate(itemPrefab, new Vector3(0, -100, 0), Quaternion.identity);
            riverRod.transform.SetParent(listContent.transform, false);
            GameObject riverRodButton = riverRod.transform.GetChild(0).gameObject;
            GameObject riverRodPriceText = riverRod.transform.GetChild(1).gameObject;
            GameObject riverRodName = riverRod.transform.GetChild(2).gameObject;
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

            GameObject oceanRod = Instantiate(itemPrefab, new Vector3(0, -200, 0), Quaternion.identity);
            oceanRod.transform.SetParent(listContent.transform, false);
            GameObject oceanRodButton = oceanRod.transform.GetChild(0).gameObject;
            GameObject oceanRodPriceText = oceanRod.transform.GetChild(1).gameObject;
            GameObject oceanRodName = oceanRod.transform.GetChild(2).gameObject;
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
}
