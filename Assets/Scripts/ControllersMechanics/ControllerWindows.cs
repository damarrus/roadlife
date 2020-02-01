using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerWindows : MonoBehaviour
{
    public GameObject WindowCharacter;
    public GameObject buttonGoAhead, buttonContinue;
    
    [SerializeField] private Sprite[] characters;
    [SerializeField] private Sprite[] endCharacters;
    [SerializeField] private Sprite[] endSecrets;
    [SerializeField] private Sprite[] secrets;
    public void BeginCharacter(int numberCharacter)
    {
        buttonGoAhead.SetActive(true);
        buttonContinue.SetActive(false);
        WindowCharacter.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = characters[numberCharacter];
        WindowCharacter.transform.GetChild(1).GetComponent<Image>().sprite = secrets[numberCharacter];
        WindowCharacter.SetActive(true);
    }
    public void ItemReady(int numberCharacter)
    {
        buttonGoAhead.SetActive(false);
        buttonContinue.SetActive(true);
        WindowCharacter.transform.GetChild(0).GetComponent<Image>().sprite = endCharacters[numberCharacter];
        WindowCharacter.transform.GetChild(1).GetComponent<Image>().sprite = endSecrets[numberCharacter];
        WindowCharacter.SetActive(true);
    }
   
    public void BeginRepairItem()
    {
        WindowCharacter.SetActive(false);
    }
}
