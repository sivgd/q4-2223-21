using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterParty : MonoBehaviour
{
    [SerializeField] List<Character> characters;

    private void Start()
    {
        foreach (var character in characters)
        {
            character.Init();
        }
    }

    public Character GetHealthyCharacter()
    {
        return characters.Where(x => x.HP > 0).FirstOrDefault();
    }
}
