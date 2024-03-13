using Scriptable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptable
{
    public enum ColorType 
    {
        Normal = 0,
        BodyColor1 = 1,
        BodyColor2 = 2,
        BodyColor3 = 3,
        BodyColor4 = 4,
        BodyColor5 = 5,
        BodyColor6 = 6,
        BodyColor7 = 7,
        BodyColor8 = 8,
        BodyColor9 = 9,
        BodyColor10 = 10,
    }
}

[CreateAssetMenu(fileName = "ColorData", menuName = "ScriptableObjects/Color")]
public class ColorSO : ScriptableObject
{
    [SerializeField] Material[] materials;

    public Material GetColor(ColorType colorType)
    {
        return materials[(int)colorType];
    }
}
