using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorePreferences : MonoBehaviour
{
    int isGreenSold;
    int ZELAmount;

    public Text ZELAmountText;
    public Text greenSkinPrice;

    public Button greenButton;

    void Start()
    {
        ZELAmount = PlayerPrefs.GetInt("ZELAmount");
    }

    void Update()
    {
        ZELAmountText.text = "ZEL: " + ZELAmount.ToString();

        isGreenSold = PlayerPrefs.GetInt("IsGreenSold");

        if (ZELAmount >= 500 && isGreenSold == 0)
        {
            greenButton.gameObject.SetActive(true);
        }
        else
        {
            greenButton.gameObject.SetActive(false);
        }
    }

    public void BuyGreenSkin()
    {
        ZELAmount -= 500;
        PlayerPrefs.SetInt("IsGreenSold", 1);
        greenSkinPrice.text = "Sold!";
        greenButton.gameObject.SetActive(false);
    }

    public void ExitShop()
    {
        PlayerPrefs.SetInt("ZELAmount", ZELAmount);
    }

    public void ResetPlayerPrefs()
    {
        ZELAmount = 0;
        greenButton.gameObject.SetActive(true);
        greenSkinPrice.text = "ZEL: 500";
        PlayerPrefs.DeleteAll();
    }
}
