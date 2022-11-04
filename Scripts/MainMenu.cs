using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject selectcharacter;
    public GameObject Mainmenu;


    public void OnSelectCharacter()
    {
        selectcharacter.SetActive(true);
        Mainmenu.SetActive(false);

    }

    public void OnPlay()
    {
        SceneManager.LoadScene("ZombieApocalypse");

    }

    public void OnQutigame()
    {
        Debug.Log("Quiting game....");
        Application.Quit();
    }
}
