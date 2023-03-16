using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void restartLevel() {
        Debug.Log(PlayerPrefs.GetInt("restart"));
        Debug.Log(PlayerPrefs.GetInt("lastLevel"));
        SceneManager.LoadScene(PlayerPrefs.GetInt("lastLevel"));
    }
}