using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathTrigger : MonoBehaviour {
    // debugging purposees
    private bool scriptEnable = true;
    private void Awake()
    {
        PlayerPrefs.SetInt("lastLevel", SceneManager.GetActiveScene().buildIndex);
    }


    void OnTriggerEnter(Collider other) {
        if (other.transform.tag.CompareTo("DeathTrigger") == 0 && scriptEnable) {
            Debug.Log("help");
            PlayerPrefs.SetInt("lastLevel", SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene("Death");
        }
    }
}