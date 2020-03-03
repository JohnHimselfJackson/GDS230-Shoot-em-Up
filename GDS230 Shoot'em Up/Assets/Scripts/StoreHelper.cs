using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreHelper : MonoBehaviour
{
    public static int ZELAmount;
    public Text ZELText;
    int isGreenSold;
    public GameObject green;

    void Start()
    {
        ZELAmount = PlayerPrefs.GetInt("ZELAmount");
        isGreenSold = PlayerPrefs.GetInt("IsGreenSold");

        if (isGreenSold == 1/* Will Need another check to make sure that green is selected in the customize settings yaaaay */)
        {
            green.SetActive(true);
        }
        else
        {
            green.SetActive(false);
        }
    }

    void Update()
    {
        ZELText.text = "ZEL: " + ZELAmount.ToString();
    }

    public void SaveMoney()
    {
        PlayerPrefs.SetInt("ZELAmount", ZELAmount);
    }
}
