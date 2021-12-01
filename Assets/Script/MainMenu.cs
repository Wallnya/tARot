using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private int nbPlayers = 4;

    public void PlayGame()
    {
        Debug.Log("nb joueurs :"+nbPlayers);
        SceneManager.LoadScene("ImageTrackingExtendedMultiple");
    }

    public void NbPlayers4()
    {

        Debug.Log("4 joueurs");
        nbPlayers = 4;
    }

    public void NbPlayers3()
    {
        Debug.Log("3 joueurs");
        nbPlayers = 3;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
