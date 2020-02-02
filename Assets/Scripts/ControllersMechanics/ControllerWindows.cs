using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerWindows : MonoBehaviour
{
    public GameObject WindowCharacter;
    public GameObject WindowToBeContinued;
    public GameObject buttonGoAhead, buttonContinue;
    public AudioSource firstCharacter;
    public AudioSource mainAudio;
    private int _numberCharacter = 0;  
    
    [SerializeField] private Sprite[] characters;
    [SerializeField] private Sprite[] endCharacters;
    [SerializeField] private Sprite[] endSecrets;
    [SerializeField] private Sprite[] secrets;
    public void BeginCharacter()
    {      
        
        buttonGoAhead.SetActive(true);
        buttonContinue.SetActive(false);
        WindowCharacter.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = characters[_numberCharacter + 1];
        WindowCharacter.transform.GetChild(1).GetComponent<Image>().sprite = secrets[_numberCharacter + 1 ];
        WindowCharacter.SetActive(true);
    }
    public void ItemReady(int numberCharacter)
    {
        _numberCharacter = numberCharacter;
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

    public void ToBeContinued()
    {       
        WindowCharacter.SetActive(false);
        WindowToBeContinued.SetActive(true);
    }


}
