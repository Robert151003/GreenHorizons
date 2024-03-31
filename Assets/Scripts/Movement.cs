using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private float targetFieldOfView = 50;
    [SerializeField] private float fieldOfViewMax = 50;
    [SerializeField] private float fieldOfViewMin = 10;

    public float moveSpeed = 25f;
    public float rotateSpeed = 25f;
    public bool useEdgeScrolling;

    public float minX = -50f; // Minimum x position
    public float maxX = 50f;  // Maximum x position
    public float minZ = -50f; // Minimum z position
    public float maxZ = 50f;  // Maximum z position

    void Start()
    {
        
    }

    
    void Update()
    {
        handleCameraMovement();
        if(useEdgeScrolling) handleCameraMovementEdgeScrolling();
        handleCameraRotation();
        handleCameraZoom();

    }

    private void handleCameraRotation()
    {
        float rotateDir = 0f;
        if (Input.GetKey(KeyCode.Q)) rotateDir = +1f;
        if (Input.GetKey(KeyCode.E)) rotateDir = -1f;

        transform.eulerAngles += new Vector3(0, rotateDir * rotateSpeed * Time.deltaTime, 0);
    }
    private void handleCameraMovement()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W)) inputDir.z = +1f;
        if (Input.GetKey(KeyCode.S)) inputDir.z = -1f;
        if (Input.GetKey(KeyCode.A)) inputDir.x = -1f;
        if (Input.GetKey(KeyCode.D)) inputDir.x = +1f;
        
        Vector3 MoveDir = transform.forward * inputDir.z + transform.right * inputDir.x;
        transform.position += MoveDir * moveSpeed * Time.deltaTime;
        Vector3 newPosition = transform.position + MoveDir * moveSpeed * Time.deltaTime;

        // Clamp the x and z positions
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

        transform.position = newPosition;
    }
    private void handleCameraMovementEdgeScrolling()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);
        int edgeScrollSize = 20;

        if (Input.mousePosition.x < edgeScrollSize) inputDir.x = -1f;
        if (Input.mousePosition.y < edgeScrollSize) inputDir.z = -1f;
        if (Input.mousePosition.x > Screen.width - edgeScrollSize) inputDir.x = +1f;
        if (Input.mousePosition.y > Screen.height - edgeScrollSize) inputDir.z = +1f;

        Vector3 MoveDir = transform.forward * inputDir.z + transform.right * inputDir.x;
        transform.position += MoveDir * moveSpeed * Time.deltaTime;
        Vector3 newPosition = transform.position + MoveDir * moveSpeed * Time.deltaTime;

        // Clamp the x and z positions
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

        transform.position = newPosition;
    }
    public void handleCameraZoom()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            targetFieldOfView -= 5;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            targetFieldOfView += 5;
        }
        
        if (cinemachineVirtualCamera != null)
        {
            targetFieldOfView = Mathf.Clamp(targetFieldOfView, fieldOfViewMin, fieldOfViewMax);

            float zoomSpeed = 10f;
            if (cinemachineVirtualCamera != null)
            {
                cinemachineVirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(cinemachineVirtualCamera.m_Lens.FieldOfView, targetFieldOfView, Time.deltaTime * zoomSpeed);
            }
        }
    }
    
}
