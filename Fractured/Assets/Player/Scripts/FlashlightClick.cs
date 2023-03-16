using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class FlashlightClick : MonoBehaviour {
    public AudioSource flashClick;

    void Update() {
        if (Input.GetKeyDown("f")) {
            flashClick.enabled = true;
        }
    }
}
