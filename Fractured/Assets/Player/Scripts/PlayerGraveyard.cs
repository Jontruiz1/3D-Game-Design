using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerGraveyard : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    public PlayerInput playerInput;
    public Camera pov;
    public float moveSpeed, maxForce;
    private bool isOn = false, paused = false;
    public Light flashlight;
    public AudioSource flashClick, footSteps;
    private float grabDistance = 4f;
    public Rigidbody rb;
    private Vector2 move;
    public GameObject pauseImage;

    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();                         // enabling the input actions

        playerInputActions.Player.PlayerSprint.performed += Sprint; // looks for shift key and if pressed/canceled calls Sprint() function
        playerInputActions.Player.PlayerSprint.canceled += Sprint;

        playerInputActions.Player.PlayerInteract.performed += Interact;

        playerInputActions.Player.PlayerFlash.performed += flashToggle;

        playerInputActions.Player.PlayerPause.performed += pause;

    }

    private void Interact(InputAction.CallbackContext context) {
        RaycastHit hit;
        if (context.performed && Physics.Raycast(pov.transform.position, pov.transform.forward, out hit, grabDistance)) {
            if (hit.collider.gameObject.CompareTag("Car")) {
                Debug.Log("Player opened the car");
                SceneManager.LoadScene("Ending");
            }
        }
    }

    private void Sprint(InputAction.CallbackContext context) {
        if (context.performed) {
            Debug.Log("Sprinting");
            moveSpeed = 25;
        }
        else {
            moveSpeed = 20;
        }
    }

    private void flashToggle(InputAction.CallbackContext context) {
        if (context.performed && !isOn) {
            isOn = true;
            flashlight.intensity = 1;
            Debug.Log("On");
            flashClick.Play();
        }
        else if (context.performed && isOn) {
            isOn = false;
            flashlight.intensity = 0;
            Debug.Log("Off");
            flashClick.Play();
        }
    }
    private void OnTriggerEnter(Collider other) {
        /*
        if (other.gameObject.tag == "GetIn") {
            objectivesDisp.objectiveUpdate("Enter the car!");
        }*/

        if(other.gameObject.tag == "DeathTrigger") {
            SceneManager.LoadScene("Death");
        }
    }

    private void pause(InputAction.CallbackContext context) {
        // if esc pressed and not paused pause else unpause
        // pov enabled disables/enables camera
        if (context.performed && !paused) {
            paused = true;
            Time.timeScale = 0f;
            pauseImage.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else {
            paused = false;
            Time.timeScale = 1f;
            pauseImage.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; //stops capsule from falling
        //objectivesDisp.objectiveUpdate("Run.");
    }

    private void Update() {
        MyInput();
    }

    private void FixedUpdate() {
        MovePlayer();
    }

    private void MyInput() {
        // reads in a 2d vector for the movement values from the input system
        move = playerInputActions.Player.PlayerMovement.ReadValue<Vector2>();
        // if we're not reading movement input, we're not moving, no footstep noises
        footSteps.enabled = !move.Equals(Vector2.zero) ? true : false;
    }

    private void MovePlayer() {
        // find target velocity
        Vector3 velocity = rb.velocity;
        Vector3 targetVelocity = new Vector3(move.x, 0, move.y);

        // adjust the speed
        targetVelocity *= (moveSpeed * 5f);

        // allign the direction
        targetVelocity = transform.TransformDirection(targetVelocity);

        // calculation for the force on player
        Vector3 velocityChange = targetVelocity - velocity;

        // limits force that can be put on the player to maxForce
        Vector3.ClampMagnitude(velocityChange, maxForce);

        // applies the force (the video I was watching used ForceMode.VelocityChange)
        // so if this becomes an issue later can always use that
        rb.AddForce(velocityChange, ForceMode.Force);
    }

    private void OnDestroy()
    {
        playerInputActions.Player.PlayerSprint.performed -= Sprint; // looks for shift key and if pressed/canceled calls Sprint() function
        playerInputActions.Player.PlayerSprint.canceled -= Sprint;

        playerInputActions.Player.PlayerInteract.performed -= Interact;

        playerInputActions.Player.PlayerFlash.performed -= flashToggle;

        playerInputActions.Player.PlayerPause.performed -= pause;
    }
}