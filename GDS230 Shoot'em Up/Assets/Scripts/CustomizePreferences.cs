using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CustomizePreferences : MonoBehaviour
{
    public Dropdown charactersDropDown;

    public Dropdown skinsDropDown;

    public Dropdown weaponsDropDown;

    const string charName = "charactersoption";
    const string skinName = "skinsoption";
    const string weaponName = "weaponsoption";


    void Awake()
    {
        charactersDropDown.onValueChanged.AddListener(new UnityAction<int>(indexer =>
        {
            PlayerPrefs.SetInt(charName, charactersDropDown.value);
            PlayerPrefs.Save();
        }));

        skinsDropDown.onValueChanged.AddListener(new UnityAction<int>(indexer =>
        {
            PlayerPrefs.SetInt(skinName, skinsDropDown.value);
            PlayerPrefs.Save();
        }));

        weaponsDropDown.onValueChanged.AddListener(new UnityAction<int>(indexer =>
        {
            PlayerPrefs.SetInt(weaponName, weaponsDropDown.value);
            PlayerPrefs.Save();
        }));
    }

    void Start()
    {
        charactersDropDown.value = PlayerPrefs.GetInt(charName, 0);
        skinsDropDown.value = PlayerPrefs.GetInt(skinName, 0);
        weaponsDropDown.value = PlayerPrefs.GetInt(weaponName, 0);
    }

    public void SetCharacter()
    {
        PlayerPrefs.SetInt(charName, charactersDropDown.value);
    }

    public void SetSkin()
    {
        PlayerPrefs.SetInt(skinName, skinsDropDown.value);
    }

    public void SetWeapon()
    {
        PlayerPrefs.SetInt(weaponName, weaponsDropDown.value);
    }

}
