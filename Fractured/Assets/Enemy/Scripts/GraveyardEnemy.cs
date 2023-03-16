using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class GraveyardEnemy : MonoBehaviour {
    private float step = 8f;
    private GameObject player;

    public void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update() {
        transform.LookAt(player.transform.position);
        if (player.GetComponent<PlayerGraveyard>().moveSpeed == 25) {
            step = 9.50f;
        }
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step * Time.deltaTime);
    }
}