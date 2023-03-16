using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimations : MonoBehaviour
{
    public PlayerControls player;
    [SerializeField] private Animator door = null;

    public void isOpen() {
        Debug.Log("Is it working?");
        door.Play("DoorOpen", 0, 0f);
    }

    public void isOpenCD() {
        door.Play("CabinDoorAnimation", 0, 0.0f);
        Debug.Log("Is the cabin one working?");
    }
}
