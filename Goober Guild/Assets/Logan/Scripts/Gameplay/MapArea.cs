using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapArea : MonoBehaviour
{
    [SerializeField] List<Character> wildCharacters;

    public Character GetRandomWildCharacter()
    {
        var wildCharacter = wildCharacters[Random.Range(0, wildCharacters.Count)];
        wildCharacter.Init();
        return wildCharacter;
    }
}
