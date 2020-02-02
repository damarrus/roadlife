using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject button;
    public void BeginGame()
    {
        button.GetComponent<AudioSource>().Play();
        InvokeRepeating("LoadScene", 1, 0);
    }
    private void LoadScene()
    {
        Application.LoadLevel("Bureau");
    }
}
