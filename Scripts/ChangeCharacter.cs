using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeCharacter : MonoBehaviour
{
    public GameObject selectcharacter;
    public GameObject mainmenu;

    public void OnBack()
    {
        selectcharacter.SetActive(false);
        mainmenu.SetActive(true);

        //SceneManager.LoadScene("MainMenu");
    }


    public void Character1()
    {
        SceneManager.LoadScene("ZombieApocalypse");
    }

    public void Character2()
    {
        SceneManager.LoadScene("ZombieApocalypse1");
    }

    public void Character3()
    {
        SceneManager.LoadScene("ZombieApocalypse2");
    }

}
