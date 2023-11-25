using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct PlayerPrefsInt
{
    string key;
    [SerializeField] int value;

    public int Value { get => value; set { this.value = value; PlayerPrefs.SetInt(key, value); } }

    public PlayerPrefsInt(string key, int defaultValue)
    {
        this.key = key;
        this.value = PlayerPrefs.GetInt(key, defaultValue);
    }
}

[System.Serializable]
public struct PlayerPrefsFloat
{
    string key;
    [SerializeField] float value;
    public float Value { get => value; set { this.value = value; PlayerPrefs.SetFloat(key, value); } }

    public PlayerPrefsFloat(string key, float defaultValue)
    {
        this.key = key;
        this.value = PlayerPrefs.GetFloat(key, defaultValue);
    }
}

[System.Serializable]
public struct PlayerPrefsBool
{
    string key;
    [SerializeField] bool value;
    public bool Value { get => value; set { this.value = value; PlayerPrefs.SetInt(key, value ? 1 : 0); } }

    public PlayerPrefsBool(string key, bool defaultValue)
    {
        this.key = key;
        this.value = PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
    }

}

[System.Serializable]
public struct PlayerPrefsString
{
    string key;
    [SerializeField] string value;
    public string Value { get => value; set { this.value = value; PlayerPrefs.SetString(key, value); } }

    public PlayerPrefsString(string key, string defaultValue)
    {
        this.key = key;
        this.value = PlayerPrefs.GetString(key, defaultValue);
    }

}


[System.Serializable]
public struct PlayerPrefsList
{
    string key;
    int defaultValue;
    [SerializeField] List<int> values;

    public PlayerPrefsList(string key, int defaultValue, List<int> values, int amount = 0)
    {
        this.key = key;
        this.defaultValue = defaultValue;
        this.values = values;

        for (int i = 0; i < amount; i++)
        {
            values.Add(GetValue(i));
        }
    }

    private int GetValue(int index)
    {
        return PlayerPrefs.GetInt(key + index, defaultValue);
    }

    public int this[int index]
    {
        get
        {
            while (index >= values.Count)
            {
                values.Add(GetValue(values.Count));
            }
            return values[index];
        }

        set
        {
            while (index >= values.Count)
            {
                values.Add(GetValue(values.Count));
            }

            values[index] = value;
            PlayerPrefs.SetInt(key + index, value);
        }
    }
}
