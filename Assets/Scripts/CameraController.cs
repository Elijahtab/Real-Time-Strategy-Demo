using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public LayerMask layerMask;

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
    private float scrollSpeed = 50f;
    private float shiftScrollSpeedMultiplier = 2f;



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
    
    //Camera movement using WASD, LeftShift
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
        float scroll = -Input.GetAxis("Mouse ScrollWheel");
        scroll = scroll * scrollSpeed;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            scroll *= shiftScrollSpeedMultiplier;
        }
        if (scroll != 0)
        {
            // Get the current mouse position
            Vector3 mousePosition = Input.mousePosition;

            // Cast a ray from the camera through the mouse position to a plane at the camera's height
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                newPosition = transform.position + (hit.point - transform.position) * -scroll;
            }
            newPosition.y = Mathf.Clamp(newPosition.y, minHeight, maxHeight);


        }
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);

    }
    /**
    void MouseZoom()
    {
        float scroll = -Input.GetAxis("Mouse ScrollWheel");
        scroll = scroll * scrollSpeed;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            scroll *= shiftScrollSpeedMultiplier;
        }
        if (scroll != 0)
        {
            // Get the current mouse position
            Vector3 mousePosition = Input.mousePosition;

            // Cast a ray from the camera through the mouse position to a plane at the camera's height
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                Vector3 newZoom = newPosition + (hit.point - transform.position) * -scroll;
                transform.position = Vector3.Lerp(transform.position, newZoom, Time.deltaTime * movementTime);
            }

        }
        
    }*/
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
