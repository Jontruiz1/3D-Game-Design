using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Opening : MonoBehaviour
{
    public AudioSource crash;
    void Update() {
        if (crash.isPlaying != true) {
            SceneManager.LoadScene("Forest");
        }
    }
}