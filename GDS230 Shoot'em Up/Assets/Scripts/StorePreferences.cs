using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorePreferences : MonoBehaviour
{
    //Integers to keep track of what item is sold later.
    int isGreenSold;
    int isRedSold;
    int isYellowSold;

    //A reference to our currency.
    int ZELAmount;

    //Text for our currency.
    public Text ZELAmountText;

    //A reference to our item prices being sold.
    public Text[] itemPrices;
    public Text greenSkinPrice;
    public Text redSkinPrice;
    public Text yellowSkinPrice;


    // A reference to our buying buttons.
    public Button[] buttons;
    public Button greenButton;
    public Button redButton;
    public Button yellowButton;

    void Start()
    {
        ZELAmount = PlayerPrefs.GetInt("ZELAmount");
    }

    void Update()
    {
        ZELAmountText.text = "ZEL: " + ZELAmount.ToString();

        isGreenSold = PlayerPrefs.GetInt("IsGreenSold");
        isRedSold = PlayerPrefs.GetInt("IsRedSold");
        isYellowSold = PlayerPrefs.GetInt("IsYellowSold");

        GreenSoldCheck();
        RedSoldCheck();
        YellowSoldCheck();
    }

    //Called when the green button is pressed - added to the OnClick event located in the button itself.
    public void BuyGreenSkin()
    {
        SkinPurchase();
        PlayerPrefs.SetInt("IsGreenSold", 1);
        itemPrices[0].text = "Sold!";
        buttons[0].gameObject.SetActive(false);
    }

    public void BuyRedSkin()
    {
        SkinPurchase();
        PlayerPrefs.SetInt("IsRedSold", 1);
        itemPrices[1].text = "Sold!";
        buttons[1].gameObject.SetActive(false);
    }

    public void BuyYellowSkin()
    {
        SkinPurchase();
        PlayerPrefs.SetInt("IsYellowSold", 1);
        itemPrices[2].text = "Sold!";
        buttons[2].gameObject.SetActive(false);
    }

    //This just saves the player prefs for the currency.
    public void ExitShop()
    {
        PlayerPrefs.SetInt("ZELAmount", ZELAmount);
    }

    public void ResetPlayerPrefs()
    {
        ZELAmount = 0;
        greenButton.gameObject.SetActive(true);
        redButton.gameObject.SetActive(true);
        yellowButton.gameObject.SetActive(true);
        greenSkinPrice.text = "ZEL: 500";
        redSkinPrice.text = "ZEL: 500";
        yellowSkinPrice.text = "ZEL: 500";
        PlayerPrefs.DeleteAll();
    }

    void SkinPurchase()
    {
        ZELAmount -= 500;
    }

    void GreenSoldCheck()
    {
        if (ZELAmount >= 500 && isGreenSold == 0)
        {
            greenButton.gameObject.SetActive(true);
        }
        else
        {
            greenButton.gameObject.SetActive(false);
        }
    }

    void RedSoldCheck()
    {
        if (ZELAmount >= 500 && isRedSold == 0)
        {
            redButton.gameObject.SetActive(true);
        }
        else
        {
            redButton.gameObject.SetActive(false);
        }
    }

    void YellowSoldCheck()
    {
        if (ZELAmount >= 500 && isYellowSold == 0)
        {
            yellowButton.gameObject.SetActive(true);
        }
        else
        {
            yellowButton.gameObject.SetActive(false);
        }
    }
}
