using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{ 
    public List<Transform> instructionsList = new List<Transform>();
    public List<Transform> optionsList = new List<Transform>();
    private void Start() {
        Time.timeScale = 1;
    }

    public void startGame()
    {
        StartCoroutine(start(2.0f));
        
    }

    public void instructions()
    {
        hider(instructionsList);
        PlayerPrefs.SetString("menuChoice", "instructions");
    }

    public void options()
    {
        hider(optionsList);
        PlayerPrefs.SetString("menuChoice", "options");
    }

    public void back()
    {
        string screen = PlayerPrefs.GetString("menuChoice");

        switch (screen)
        {
            case "options":
                hider(optionsList);
                break;
            case "instructions":
                hider(instructionsList);
                break;

        }
        PlayerPrefs.SetString("menuChoice", "main");

    }
    
    private void hider(List<Transform> UI)
    {
        foreach (Transform go in UI)
        {
            go.gameObject.SetActive(!go.gameObject.activeSelf);
        }
    }


    private IEnumerator start(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Start");
    }

    public void restartLevel() {
        StartCoroutine(reload(1.5f));
        
    }

    IEnumerator reload(float time) {
        yield return new WaitForSeconds(time);
        Debug.Log("loading");
        SceneManager.LoadScene(PlayerPrefs.GetInt("lastLevel"));

    }
    public void doExitGame() {
        Debug.Log("You quit the game!");
        Application.Quit();
    }
}
