using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NameUtilities
{
    private static List<string> names = new List<string>()
    {
        "Link",
        "AmLeng",
        "Phil",
        "Belling",
        "Victor",
        "Messi",
        "PatMan",
        "Wolf",
        "James",
        "Heiu",
        "Hi Eu",
        "Hi Ep",
        "Perez",
        "AlexanderKu",
        "Rogers",
        "Law",
        "Nami",
        "Ana",
        "ISu",
        "KenPachi",
        "Shadow",
        "Beily",
        "Ederson",
        "Deny",
        "Thor",
        "Helling",
        "Marie",
        "Walker",
    };

    public static List<string> GetNames(int amount)
    {
        var list = names.OrderBy(d => System.Guid.NewGuid());
        return list.Take(amount).ToList();
    }

    public static string GetRandomName()
    {
        return names[Random.Range(0, names.Count)];
    }
}
