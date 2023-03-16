using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerControls : MonoBehaviour
{
    public DoorAnimations gateAnime;
    public DoorAnimations doorAnime;
    public GameManager gameManager;
    // player stat variables
    public float moveSpeed, maxForce;
    private int counter = 0;

    public Rigidbody rb;
    public Light flashlight;
    private Vector2 move;
    public PickUpNotif pickUpDisp, objectivesDisp, sideObjectivesDisp;
    public Camera pov;

    private PlayerInputActions playerInputActions;
    public PlayerInput playerInput;

    private float grabDistance = 4f, changeTime = 0;
    private bool flashEquipped = false, fenceEquipped = false, isOn = false, paused = false, cabinEquipped = true, carKeyEquipped = false;
    public AudioSource flashClick, pickupSound, footSteps, gate, door;
    private bool isCooldown;
    private bool isSprinting;
    private float sprintTimer;
    private float sprintTime = 5.0f;
    private float speed;
    InputAction.CallbackContext sprintState;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();                         // enabling the input actions

        playerInputActions.Player.PlayerSprint.performed += Sprint; // looks for shift key and if pressed/canceled calls Sprint() function
        playerInputActions.Player.PlayerSprint.canceled += Sprint;

        playerInputActions.Player.PlayerInteract.performed += Interact;

        playerInputActions.Player.PlayerFlash.performed += flashToggle;

        playerInputActions.Player.PlayerPause.performed += pause;
        
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; //stops capsule from falling
        objectivesDisp.objectiveUpdate("Pick up the flashlight");
    }

    private void Update()
    {
        if (!paused)
        {
            /*
            if (Input.GetKey(KeyCode.LeftShift) && !isCooldown) { 
                isSprinting = true;
                sprintTimer += Time.deltaTime;
                Debug.Log(moveSpeed);
            }
            else {
                isSprinting = false;
                if (isCooldown && !Input.GetKey(KeyCode.LeftShift)) {
                    sprintTimer -= Time.deltaTime;
                }
                Debug.Log(moveSpeed);
            }
            sprintTimer = Mathf.Clamp(sprintTimer, 0f, sprintTime);

            if (isSprinting) moveSpeed = 21f;
            else moveSpeed = 16f;

            if (sprintTimer >= sprintTime) isCooldown = true;
            if (sprintTimer <= 0.5f) isCooldown = false;
*/
            //Debug.Log(sprintTime);
            MyInput();
            cabinKey();
        }
    }

    private void FixedUpdate()
    {
        if (!paused) MovePlayer();
    }

    private void MyInput()
    {
        // reads in a 2d vector for the movement values from the input system
        move = playerInputActions.Player.PlayerMovement.ReadValue<Vector2>();
        // if we're not reading movement input, we're not moving, no footstep noises
        footSteps.enabled = !move.Equals(Vector2.zero) ? true : false;
        
        if (sprintTime >= 5f) isCooldown = false;
        if (sprintTime <= 0.5f) isCooldown = true;

        sprintTime = (isSprinting ? sprintTime : sprintTime + Time.deltaTime);
        moveSpeed = (isSprinting && !isCooldown ? 21 : 16);

        if (isSprinting && !isCooldown) sprintTime -= Time.deltaTime;
        sprintTime = Mathf.Clamp(sprintTime, 0, 5f);

        Debug.Log(moveSpeed);
        Debug.Log(sprintTime);
    }

    private void MovePlayer()
    {
        // find target velocity
        Vector3 velocity = rb.velocity;
        Vector3 targetVelocity = new Vector3(move.x, 0, move.y);

        //Debug.Log("Current speed:");
        //Debug.Log(moveSpeed);
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

    private void Interact(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        
        if (context.performed && Physics.Raycast(pov.transform.position, pov.transform.forward, out hit, grabDistance) && !paused)
        {
            // get tag for hit collider
            string gTag = hit.collider.gameObject.tag.ToString();
            switch (gTag) {
                case "Flashmodel":
                    Destroy(hit.collider.gameObject);
                    pickupSound.Play();
                    flashEquipped = true;
                    pickUpDisp.pickUpDisplay("Flashlight");
                    objectivesDisp.objectiveUpdate("Investigate the cabin");
                    break;
                case "Fence Key":
                    Destroy(hit.collider.gameObject);
                    pickupSound.Play();
                    fenceEquipped = true;
                    pickUpDisp.pickUpDisplay("Mysterious key");
                    objectivesDisp.objectiveUpdate("Investigate the fence");
                    break;
                case "GateDoor":
                    if (fenceEquipped) {
                        gate.Play();
                        gateAnime.isOpen();
                        //Destroy(hit.collider.gameObject);
                        objectivesDisp.objectiveUpdate("Find the cabin key");
                    }
                    else {
                        objectivesDisp.objectiveUpdate("The gate is locked");
                    }
                    break;
                case "Door":
                    if (cabinEquipped) {
                        door.Play();
                        doorAnime.isOpenCD();
                        //Destroy(hit.collider.gameObject);
                        objectivesDisp.objectiveUpdate("Grab car key");
                    }
                    else {
                        objectivesDisp.objectiveUpdate("The door is locked");
                    }
                    break;
                case "Keys":
                    gameManager.pickUp(hit.collider.gameObject);
                    objectivesDisp.objectiveUpdate("Not the right key...");
                    counter++;
                    break;
                case "Cabin Key":
                    Destroy(hit.collider.gameObject);
                    gameManager.startHunt();
                    cabinEquipped = true;
                    objectivesDisp.objectiveUpdate("Run.");                  
                    sideObjectivesDisp.sideObjectiveUpdate("Get to the cabin");
                    break;
                case "CarKey":
                    Destroy(hit.collider.gameObject);
                    carKeyEquipped = true;
                    objectivesDisp.objectiveUpdate("Go through the back door");
                    break;
                case "TunnelDoor":
                    if (!carKeyEquipped) objectivesDisp.objectiveUpdate("I need the car keys first");
                    else
                    {
                        Debug.Log("tunnel door");
                        hit.collider.gameObject.GetComponent<AudioSource>().Play();
                        Destroy(hit.collider.gameObject);
                    }
                    break;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        string gTag = other.gameObject.tag;
        string gName = other.gameObject.name;

        switch (gTag) {
            // flashlight flicker time
            case "Disturbance":
                changeTime = Random.Range(0, 100);
                flashlight.intensity = (changeTime > 95 ? 0 : 2);
                isOn = (changeTime > 95 ? false : true);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        string gTag = other.gameObject.tag;
        switch (gTag)
        {
            case "DeathTrigger":
                SceneManager.LoadScene("Death");
                break;
            case "GraveyardTrigger":
                SceneManager.LoadScene("Grave");
                break;
            case "Area":
                Debug.Log("change area");
                gameManager.enemy.changeArea(other);
                break;
        }
    }

    private void Sprint(InputAction.CallbackContext context) {
        isSprinting = (context.performed ? true : false);
    }


private void flashToggle(InputAction.CallbackContext context)
    {
        if (context.performed && !isOn && flashEquipped && !paused)
        {
            isOn = true;
            flashlight.intensity = 2;
            flashClick.Play();
        }
        else if(context.performed && flashEquipped && isOn && !paused)
        {
            isOn = false;
            flashlight.intensity = 0;
            flashClick.Play();
        }
    }

    private void pause(InputAction.CallbackContext context)
    {
        // just handle it in the game manager
        paused = gameManager.pauseGame();
    }

    public void cabinKey() {
        if(counter >= 2) {
            gameManager.spawnCabinKey();
            counter = -5;
        }
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