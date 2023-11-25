using System.Collections.Generic;
using UnityEngine;

public static class Cache
{
    private static Dictionary<Collider, Character> characters = new Dictionary<Collider, Character>();

    public static Character GetCollectCharacter(Collider collider)
    {
        if (!characters.ContainsKey(collider))
        {
            Character character = collider.GetComponent<Character>();
            characters.Add(collider, character);
        }
        return characters[collider];
    }

    private static Dictionary<SpriteRenderer, Character> charactersCicleChoice = new Dictionary<SpriteRenderer, Character>();
    public static Character GetCicleChoice(SpriteRenderer sp)
    {
        charactersCicleChoice.Add(sp, sp.GetComponent<Character>());

        return charactersCicleChoice[sp];
    }
}
