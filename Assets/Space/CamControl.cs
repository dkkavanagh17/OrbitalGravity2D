using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public static CamControl instance;
    public Camera cameraChild;
    public float zoomIncrease;
    public float minZoom;
    public float maxZoom;
    public float zoom;


    public float movementSpeed;
    public float movementTime;
    public float normalSpeed;
    public float fastSpeed;
    public Vector3 newPosition;

    public Vector3 minPositionValues;
    public Vector3 maxPositionValues;

    public float rotationAmount;
    public float rotationTime;
    public Quaternion newRotation;

    public Vector3 dragStartPosition;
    public Vector3 dragCurrentPosition;
    public Vector3 rotateStartPosition;
    public Vector3 rotateCurrentPosition;

    public Transform followTransform;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        newPosition = transform.position;
        newRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(followTransform != null)
        {
            transform.position = followTransform.position;
        }
        else
        {
            HandleMouseMovement();
        }

        HandleMouseRotation();
        
    }

    void HandleMouseMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }

        }

        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);

                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }

        }

        
        newPosition.x = Mathf.Clamp(newPosition.x, minPositionValues.x, maxPositionValues.x);
        newPosition.z = Mathf.Clamp(newPosition.z, minPositionValues.z, maxPositionValues.z);

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        

    }

    void HandleMouseRotation()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            cameraChild.orthographicSize -= Input.mouseScrollDelta.y * zoomIncrease;
        }

        if (Input.GetMouseButtonDown(2))
        {
            rotateStartPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            rotateCurrentPosition = Input.mousePosition;

            Vector3 difference = rotateStartPosition - rotateCurrentPosition;

            rotateStartPosition = rotateCurrentPosition;

            newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationTime);
            
        }
        cameraChild.orthographicSize = Mathf.Clamp(cameraChild.orthographicSize, minZoom, maxZoom);
    }

    
}