using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    public Transform location;
    public Transform orientation;
    public Transform flashPos;
    public float offsetX = 0;
    public float offsetY = 2;
    public float offetZ = 0;
    public float sensitivityX; //x-axis
    public float sensitivityY; //y-axis

    float xRotation;
    float yRotation;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            transform.position = (location.position + new Vector3(offsetX, offsetY, offetZ));   //location of camera relative to player
            flashPos.position = (location.position + new Vector3(offsetX, offsetY, offetZ));    //location of spotlight
                                                                                                //relative to 
            float mouseX = Input.GetAxisRaw("Mouse X") * sensitivityX;          //input
            float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivityY;          //input

            yRotation += mouseX; //rotation
            xRotation -= mouseY; //rotation
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);                      //limited up down view rotation

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);     // rotation for camera
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);           // rotation for player
            flashPos.rotation = Quaternion.Euler(xRotation, yRotation, 0);      // rotation for flashlight
        }
       
    }

}
