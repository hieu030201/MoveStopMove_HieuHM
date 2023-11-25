using Scriptable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HatType
{
    None = 0,
    Arrow = 1,
    Cap = 2,
    Crownboy = 3,
    Crown = 4,
    Ear = 5,
    Headphone = 6,
    Horn = 7,
    PoliceCap = 8,
    StrawHat = 9,
}

[CreateAssetMenu(fileName = "HatData", menuName = "ScriptableObjects/HatData")]
public class HatData : ScriptableObject
{
    [SerializeField] GameObject[] hatData;
    public GameObject GetHat(HatType hatDataType)
    {
        return hatData[(int)hatDataType];
    }
}
