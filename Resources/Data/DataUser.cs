using Scriptable;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


[CreateAssetMenu(fileName ="DataUser", menuName ="ScriptableObjects/DataUser")]
public class DataUser : ScriptableObject
{
    private static DataUser ins;
    public static DataUser Ins
    {
        get
        {
            if (ins == null)
            {
                DataUser[] datas = Resources.LoadAll<DataUser>("");

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

    public int level = 0;
    public int coin = 0;

    public const string Key_Level = "Level";
    public const string Key_Coin = "Coin";

    public const string Key_Player_Weapon = "PlayerWeapon";
    public const string Key_Player_Hat = "PlayerHat";
    public const string Key_Player_Pant = "PlayerPant";
    public const string Key_Player_Color = "PlayerColor";

    public const string Keys_Weapon_Data = "WeaponDatas";
    public const string Keys_Hat_Data = "HatDatas";
    public const string Keys_Pant_Data = "PantDatas";
    public const string Keys_Accessory_Data = "AccessoryDatas";
    public const string Keys_Skin_Data = "SkinDatas";

    public HatType playerHat;
    public PantType playerPant;
    public ColorType playerColor;
    //Example
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

        playerHat = GetEnumData(Key_Player_Hat, HatType.none);
    }

    public void OnResetData()
    {
        PlayerPrefs.DeleteAll();
        OnInitData();
    }
}
#if UNITY_EDITOR

//[CustomEditor(typeof(DataUser))]
//public class UserDataEditor : Editor
//{
//    DataUser userData;

//    private void OnEnable()
//    {
//        userData = (DataUser)target;
//    }

//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();

//        if (GUILayout.Button("Load Data"))
//        {
//            userData.OnInitData();
//            EditorUtility.SetDirty(userData);
//        }

//    }
//}

#endif