using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showMenu()
    {
        gameObject.SetActive(true);
    }

    public void hideMenu()
    {
        gameObject.SetActive(false);
    }

    public void resumeGame()
    {
        PlayerController.isGamePaused = false;
        hideMenu();
    }

    public void controlsMenu()
    {

    }

    public void quitGame()
    {
        SceneManager.LoadScene("TitleScreen", LoadSceneMode.Single);
        PlayerController.isGamePaused = false;
    }
}
