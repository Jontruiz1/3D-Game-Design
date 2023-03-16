using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    private TMP_Text message;
    public GameObject button;
    public AudioSource splatter;

    public void Awake() {
        message = GetComponent<TMP_Text>();
        button.SetActive(false);
    }

    void Update() {
        if (splatter.isPlaying != true) {
            StartCoroutine(ShowMessage());
            button.SetActive(true);
        }

        // Debug.Log(Time.timeSinceLevelLoad);
    }

    public IEnumerator ShowMessage() {
        message.color = new Color(message.color.r, message.color.g, message.color.b, 1);
        message.SetText("You Died");
        yield return null;

    }
}
