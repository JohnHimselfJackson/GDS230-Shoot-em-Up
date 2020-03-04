using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreHelper : MonoBehaviour
{
    //Our currency and currency Text.
    public static int ZELAmount;
    public static int i;
    public static int e;
    public static int o;

    public Text ZELText;

    //A reference to our skin integers that are sold or unsold.
    int isGreenSold;
    int isRedSold;
    int isYellowSold;

    //A reference to our skins.
    public GameObject green;
    public GameObject red;
    public GameObject yellow;

    void Start()
    {
        //Gets our player prefs for our currency and our skins.
        ZELAmount = PlayerPrefs.GetInt("ZELAmount");
        i = PlayerPrefs.GetInt("skinsoption");
        Debug.Log("I is equal to " + i);
        isGreenSold = PlayerPrefs.GetInt("IsGreenSold");
        isRedSold = PlayerPrefs.GetInt("IsRedSold");
        isYellowSold = PlayerPrefs.GetInt("IsYellowSold");

        //Checks to see if the skins are sold then activates or deactivates the gameobject associated with it.
        if (isGreenSold == 1 && i == 2)
        {
            green.SetActive(true);
        }
        else
        {
            green.SetActive(false);
        }

        if (isRedSold == 1 && i == 0)
        {
            red.SetActive(true);
        }
        else
        {
            red.SetActive(false);
        }

        if (isYellowSold == 1 && i == 3)
        {
            yellow.SetActive(true);
        }
        else
        {
            yellow.SetActive(false);
        }


    }

    void Update()
    {
        //Sets our currency to a text.
        ZELText.text = "ZEL: " + ZELAmount.ToString();
    }

    public void SaveMoney()
    {
        //Saves our currency in prefs.
        PlayerPrefs.SetInt("ZELAmount", ZELAmount);
    }
}
