using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCam : MonoBehaviour
{
    private void Start() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
