using Scriptable;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "UserData", menuName = "ScriptableObjects/UserData", order = 1)]
public class UserData : ScriptableObject
{
    private static UserData ins;
    public static UserData Ins
    {
        get
        {
            if (ins == null)
            {
                UserData[] datas = Resources.LoadAll<UserData>("");

                if (datas.Length == 1)
                {
                    ins = datas[0];
                }
                else
                if (datas.Length == 0)
                {
                    Debug.LogError("Can find Scriptableobject UserData");
                }
                else
                {
                    Debug.LogError("have multiple Scriptableobject UserData");
                }
            }

            return ins;
        }
    }

    public const string Key_Level = "Level";
    public const string Key_Coin = "Coin";
    public const string Key_SoundIsOn = "SoundIsOn";
    public const string Key_Vibrate = "VibrateIsOn";
    public const string Key_RemoveAds = "RemoveAds";
    public const string Key_Tutorial = "Tutorial";

    public const string Key_Player_Weapon = "PlayerWeapon";
    public const string Key_Player_Hat = "PlayerHat";
    public const string Key_Player_Pant = "PlayerPant";
    public const string Key_Player_Accessory = "PlayerAccessory";
    public const string Key_Player_Color = "PlayerColor";

    public const string Keys_Weapon_Data = "WeaponDatas";
    public const string Keys_Hat_Data = "HatDatas";
    public const string Keys_Pant_Data = "PantDatas";
    public const string Keys_Accessory_Data = "AccessoryDatas";
    public const string Keys_Skin_Data = "SkinDatas";

    public int level = 0;
    public int coin = 0;

    public bool soundIsOn = true;
    public bool vibrate = true;
    public bool removeAds = false;
    public bool tutorialed = false;

    public WeaponType playerWeapon;
    public HatType playerHat;
    public PantsType playerPant;
    public ColorType playerColor;
    //Example
    // UserData.Ins.SetInt(UserData.Key_Level, ref UserData.Ins.level, 1);

    //data for list
    /// <summary>
    ///  0 = lock , 1 = unlock , 2 = selected
    /// </summary>
    /// <param name="key"></param>
    /// <param name="ID"></param>
    /// <param name="state"></param>
    public void SetDataState(string key, int ID, int state)
    {
        PlayerPrefs.SetInt(key + ID, state);
    }
    /// <summary>
    ///  0 = lock , 1 = unlock , 2 = selected
    /// </summary>
    /// <param name="key"></param>
    /// <param name="ID"></param>
    /// <param name="state"></param>
    public int GetDataState(string key, int ID, int state = 0)
    {
        return PlayerPrefs.GetInt(key + ID, state);
    }

    public void SetDataState(string key, int state)
    {
        PlayerPrefs.SetInt(key, state);
    }

    public int GetDataState(string key, int state = 0)
    {
        return PlayerPrefs.GetInt(key, state);
    }

    /// <summary>
    /// Key_Name
    /// if(bool) true == 1
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void SetIntData(string key, ref int variable, int value)
    {
        variable = value;
        PlayerPrefs.SetInt(key, value);
    }

    public void SetBoolData(string key, ref bool variable, bool value)
    {
        variable = value;
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    public void SetFloatData(string key, ref float variable, float value)
    {
        variable = value;
        PlayerPrefs.GetFloat(key, value);
    }

    public void SetStringData(string key, ref string variable, string value)
    {
        variable = value;
        PlayerPrefs.SetString(key, value);
    }

    public void SetEnumData<T>(string key, ref T variable, T value)
    {
        variable = value;
        PlayerPrefs.SetInt(key, Convert.ToInt32(value));
    }

    public void SetEnumData<T>(string key, T value)
    {
        PlayerPrefs.SetInt(key, Convert.ToInt32(value));
    }

    public T GetEnumData<T>(string key, T defaultValue) where T : Enum
    {
        return (T)Enum.ToObject(typeof(T), PlayerPrefs.GetInt(key, Convert.ToInt32(defaultValue)));
    }


#if UNITY_EDITOR
    [Space(10)]
    [Header("---- Editor ----")]
    public bool isTest;
#endif

    public void OnInitData()
    {

#if UNITY_EDITOR
        if (isTest) return;
#endif

        level = PlayerPrefs.GetInt(Key_Level, 0);
        coin = PlayerPrefs.GetInt(Key_Coin, 0);

        removeAds = PlayerPrefs.GetInt(Key_RemoveAds, 0) == 1;
        tutorialed = PlayerPrefs.GetInt(Key_Tutorial, 0) == 1;
        soundIsOn = PlayerPrefs.GetInt(Key_SoundIsOn, 0) == 1;
        vibrate = PlayerPrefs.GetInt(Key_Vibrate, 0) == 1;

        playerWeapon = GetEnumData(Key_Player_Weapon, WeaponType.Hammer_ID01);
        playerHat = GetEnumData(Key_Player_Hat, HatType.None);
        playerPant = GetEnumData(Key_Player_Pant, PantsType.HidePant);
        playerColor = GetEnumData(Key_Player_Color, ColorType.Normal);
    }

    public void OnResetData()
    {
        PlayerPrefs.DeleteAll();
        OnInitData();
    }
}


