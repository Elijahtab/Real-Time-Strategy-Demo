using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Movement speed and duration of movement drift.
    public float movementSpeed;
    public float movementTime;

    //Rotation variables
    public float rotateSpeed;
    private Vector2 p1;
    private Vector2 p2;

    //Max and Min height for Camera
    [SerializeField]
    private float maxHeight;
    [SerializeField]
    private float minHeight;
    
    //Camera tilt limits
    [SerializeField]
    private float minRotationHeight = 60;

    //Fast and normal movement speeds.
    [SerializeField]
    private float fastMovement;
    [SerializeField]
    private float normalMovement;

    //Scroll speed, shiftScrollMultiplier, and position variable for newPosition.y
    [SerializeField]
    private float scrollSpeed = 50.0f;
    public float shiftScrollSpeedMultiplier = 5.0f;
    private float newPositionY;



    //Vector3 Position for camera
    public Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement();
        MouseRotation();
    }
    
    //Camera movement using WASD, LeftShift, and ScrollWheel
    void CameraMovement()
    {

        //Checks if left shift key is pressed.
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = fastMovement;
        }
        else
        {
            movementSpeed = normalMovement;
        }

        //WASD movement updating newPosition based on movementSpeed.
        if (Input.GetKey(KeyCode.W))
        {
            newPosition += (transform.forward * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            newPosition += (transform.forward * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            newPosition += (transform.right * movementSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            newPosition += (transform.right * -movementSpeed);
        }

        //Gets input from scrollWheel
        float scroll = -Input.GetAxis("Mouse ScrollWheel");
        //Increases downward scrolling
        if (scroll < 0){
            scroll = scroll * 2;
        }
        float effectiveScrollSpeed = scrollSpeed;
        
        //Lets you speed up scroll with shift keys.
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
            effectiveScrollSpeed *= shiftScrollSpeedMultiplier;
        }

        //Limits the camera height between min and max height. If statement is used to keep the speed high until 
        //we get close to the min or max.
        if(transform.position.y < minHeight + 5 || transform.position.y > maxHeight - 5)
        {
            newPositionY = Mathf.Clamp(transform.position.y + scroll *effectiveScrollSpeed, minHeight, maxHeight);
        }
        else {
            newPositionY = transform.position.y + scroll *effectiveScrollSpeed;
        }

        //Sets camera's y axis to the scroll position.
        newPosition.y = newPositionY;
        //Updates the position of the camera using Vector3.Lerp for smooth movement.
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
    }
    void MouseRotation()
    {
        if (Input.GetMouseButtonDown(2))
        {
            p1 = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            p2 = Input.mousePosition;
            float horizontalRotation = (p2.x - p1.x) * rotateSpeed * Time.deltaTime;
            float verticalRotation = (p2.y - p1.y) * rotateSpeed * Time.deltaTime;

            // Get the current rotation of the child object
            Quaternion currentChildRotation = transform.GetChild(0).transform.rotation;

            // Calculate the new X rotation
            float newChildXRotation = currentChildRotation.eulerAngles.x - verticalRotation;

            // Limit the X rotation to be between 10 and 60 degrees
            newChildXRotation = Mathf.Clamp(newChildXRotation, 10, minRotationHeight);

            // Set the new rotation of the child object
            transform.GetChild(0).transform.rotation = Quaternion.Euler(newChildXRotation, currentChildRotation.eulerAngles.y, currentChildRotation.eulerAngles.z);

            // Apply the Y rotation to the main camera
            transform.rotation *= Quaternion.Euler(new Vector3(0, horizontalRotation, 0));

            p1 = p2;
        }
    }
}
