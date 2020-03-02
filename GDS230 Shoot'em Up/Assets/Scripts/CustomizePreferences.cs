using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CustomizePreferences : MonoBehaviour
{
    //A reference to our 3 dropdowns for customization.
    public Dropdown charactersDropDown;
    public Dropdown skinsDropDown;
    public Dropdown weaponsDropDown;

    //A reference to our 3 images that appear above the dropdowns.
    public Image skinPicture;
    public Image weaponPicture;
    public Image charPicture;

    //A reference to our strings that our player prefs use.
    const string charName = "charactersoption";
    const string skinName = "skinsoption";
    const string weaponName = "weaponsoption";

    //These integers handle how our Pictures for the CHARACTERS, SKINS & WEAPONS change.
    public int i;
    public int e;
    public int o;


    // Handles what happens to our customization prefs during a Unity event & saves them.
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
        //Sets our pref values to default values initially.
        charactersDropDown.value = PlayerPrefs.GetInt(charName, 0);
        skinsDropDown.value = PlayerPrefs.GetInt(skinName, 0);
        weaponsDropDown.value = PlayerPrefs.GetInt(weaponName, 0);

        //sets our integer (i, e, o) values to be the same as its corresponding Dropdown value.
        i = skinsDropDown.value;
        e = charactersDropDown.value;
        o = weaponsDropDown.value;
    }

    void Update()
    {
        i = skinsDropDown.value;
        e = charactersDropDown.value;
        o = weaponsDropDown.value;
        CharacterState();
        PictureState();
        WeaponState();
    }


    //Sets our character prefs in game.
    public void SetCharacter()
    {
        PlayerPrefs.SetInt(charName, charactersDropDown.value);
    }

    //Sets our skin prefs in game.
    public void SetSkin()
    {
        PlayerPrefs.SetInt(skinName, skinsDropDown.value);
    }

    //Sets our skin prefs in game.
    public void SetWeapon()
    {
        PlayerPrefs.SetInt(weaponName, weaponsDropDown.value);
    }

    //Handles what color our Skin Picture changes to depending on i.
    void PictureState()
    {
        switch (i)
        {
            case 4:
                skinPicture.color = Color.black;
                break;
            case 3:
                skinPicture.color = Color.yellow;
                break;
            case 2:
                skinPicture.color = Color.green;
                break;
            case 1:
                skinPicture.color = Color.blue;
                break;
            case 0:
                skinPicture.color = Color.red;
                break;
            default:
                Debug.Log("REEEEE");
                break;
        }
    }

    //Handles what color our Character Picture changes to depending on e.
    void CharacterState()
    {
        switch (e)
        {
            case 4:
                charPicture.color = Color.black;
                break;
            case 3:
                charPicture.color = Color.yellow;
                break;
            case 2:
                charPicture.color = Color.green;
                break;
            case 1:
                charPicture.color = Color.blue;
                break;
            case 0:
                charPicture.color = Color.red;
                break;
            default:
                Debug.Log("REEEEE");
                break;
        }
    }

    void WeaponState()
    {
        switch (o)
        {
            case 4:
                weaponPicture.color = Color.black;
                break;
            case 3:
                weaponPicture.color = Color.yellow;
                break;
            case 2:
                weaponPicture.color = Color.green;
                break;
            case 1:
                weaponPicture.color = Color.blue;
                break;
            case 0:
                weaponPicture.color = Color.red;
                break;
            default:
                Debug.Log("REEEEE");
                break;
        }
    }
}
