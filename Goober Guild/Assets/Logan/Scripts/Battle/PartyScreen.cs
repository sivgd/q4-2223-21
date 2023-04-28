using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyScreen : MonoBehaviour
{
    [SerializeField] Text messageText;

    PartyMemberUI[] memberSlots;

    public void Init()
    {
        memberSlots = GetComponentsInChildren<PartyMemberUI>();
    }

    public void SetPartyData(List<Character> characters)
    {
        for (int i = 0; i < memberSlots.Length; i++)
        {
            if (i < characters.Count)
                memberSlots[i].SetData(characters[i]);
            else
                memberSlots[i].gameObject.SetActive(false);
        }

        messageText.text = "Select a character";
    }
}
