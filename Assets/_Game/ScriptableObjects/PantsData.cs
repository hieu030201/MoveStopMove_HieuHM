using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PantsType
{
    HidePant = 0,
    Pant_1 = 1,
    Pant_2 = 2,
    Pant_3 = 3,
    Pant_4 = 4,
    Pant_5 = 5,
    Pant_6 = 6,
    Pant_7 = 7,
    Pant_8 = 8,
    Pant_9 = 9,
}
[CreateAssetMenu(fileName = "PantsData", menuName = "ScriptableObjects/PantsData", order = 1)]
public class PantsData : ScriptableObject
{
    [SerializeField] Material[] pants;

    public Material GetPants(PantsType pantsType)
    {
        return pants[(int)pantsType];
    }
}
