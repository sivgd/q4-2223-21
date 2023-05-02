using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterParty : MonoBehaviour
{
    [SerializeField] List<Character> characters;

    public List<Character> Characters
    {
        get
        {
            return characters;
        }
    }

    private void Start()
    {
        foreach (var character in Characters)
        {
            character.Init();
        }
    }

    public Character GetHealthyCharacter()
    {
        return characters.Where(x => x.HP > 0).FirstOrDefault();
    }
}
