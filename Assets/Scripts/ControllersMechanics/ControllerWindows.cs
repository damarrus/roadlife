using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerWindows : MonoBehaviour
{
    public GameObject WindowCharacter;
    [SerializeField] private Sprite[] characters;
    [SerializeField] private Sprite[] endCharacters;
    [SerializeField] private Sprite[] endSecrets;
    [SerializeField] private Sprite[] secrets;
    public void ItemReady(int numberCharacter)
    {
        WindowCharacter.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = endCharacters[numberCharacter];
        WindowCharacter.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = endSecrets[numberCharacter];
        WindowCharacter.SetActive(true);
    }
    public void BeginCharacter(int numberCharacter)
    {
        WindowCharacter.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = secrets[numberCharacter];
        WindowCharacter.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = characters[numberCharacter];
        WindowCharacter.SetActive(true);
    }
    public void BeginRepairItem()
    {
        WindowCharacter.SetActive(false);
    }
}
