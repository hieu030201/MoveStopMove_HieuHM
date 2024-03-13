using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Utilities
{
    public static List<string> listNameCharacter = new List<string>
        {
           "Sofia",
           "Delight",
           "Havert",
           "Monosuke",
           "Debele",
           "NoNem",
           "NiNo",
           "am Tilo",
           "am Phil",
           "Philip",
           "Bean",
           "Buft",
           "Burn",
           "Kat",
           "Kty",
           "Whillyan"
        };
    public static string SetNameRandom()
    {
        return listNameCharacter[Random.Range(0, listNameCharacter.Count)];
    }
    public static bool Chance(int rand, int max = 100)
    {
        return Random.Range(0, max) < rand;
    }

}