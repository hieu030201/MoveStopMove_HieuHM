using Scriptable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptable
{
    public enum ColorType
    {
        Normal = 0,
        Angle = 1,
        Deadpool = 2,
        Deavil = 3,
        Thor = 4,
    }
}

[CreateAssetMenu(fileName = "ColorData", menuName = "ScriptableObjects/ColorData", order = 1)]
public class ColorData : ScriptableObject
{
    //theo tha material theo dung thu tu ColorType
    [SerializeField] Material[] materials;

    //lay material theo mau tuong ung
    public Material GetMat(ColorType colorType)
    {
        return materials[(int)colorType];
    }
}
