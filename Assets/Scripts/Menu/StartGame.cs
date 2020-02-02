using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject button;
    public void BeginGame()
    {
        button.GetComponent<AudioSource>().Play();
        Invoke("LoadScene", 0.5f);
    }
    private void LoadScene()
    {
        Application.LoadLevel("Bureau");
    }
    public void ExitGame()
    {
        button.GetComponent<AudioSource>().Play();
        Invoke("Exit", 1);
    }
    private void Exit()
    {
        Application.Quit();
    }
}
