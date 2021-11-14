using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        SceneManager.LoadScene("Market", LoadSceneMode.Single);
        //Reset Player Inventory on Each Play
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
