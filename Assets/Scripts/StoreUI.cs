using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    public int storeTab = 0;

    public Button sellButton;
    public Button rodButton;
    public Button lureButton;
    public Button upgradeButton;

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
        }
        if (tab == 1)
        {
            sellButton.enabled = true;
            rodButton.enabled = false;
            lureButton.enabled = true;
            upgradeButton.enabled = true;
        }
        if (tab == 2)
        {
            sellButton.enabled = true;
            rodButton.enabled = true;
            lureButton.enabled = false;
            upgradeButton.enabled = true;
        }
        if (tab == 3)
        {
            sellButton.enabled = true;
            rodButton.enabled = true;
            lureButton.enabled = true;
            upgradeButton.enabled = false;
        }
    }
}
