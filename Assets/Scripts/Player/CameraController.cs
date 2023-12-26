using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float zoomSpeed = 5f;
    [SerializeField] float minZoom = 2f;
    [SerializeField] float maxZoom = 10f;

    void Start()
    {
        // Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        Camera.main.fieldOfView -= scrollInput * zoomSpeed;
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, minZoom, maxZoom);
    }

    void LateUpdate()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            transform.position = new Vector3(target.position.x - 0f, transform.position.y, target.position.z - 8f);
            transform.LookAt(target);
        }
        else
        {
            ControlCameraWithMouse();
            ControlCameraWithKeyboard();
        }
    }

    void ControlCameraWithMouse(){
        if (Input.mousePosition.x > Screen.width - 10)
        {
            transform.position += new Vector3(1, 0, 0) * Time.deltaTime * 10;
        }
        if (Input.mousePosition.x < 10)
        {
            transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * 10;
        }
        if (Input.mousePosition.y > Screen.height - 10)
        {
            transform.position += new Vector3(0, 0, 1) * Time.deltaTime * 10;
        }
        if (Input.mousePosition.y < 10)
        {
            transform.position += new Vector3(0, 0, -1) * Time.deltaTime * 10;
        }
    }

    void ControlCameraWithKeyboard(){
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0, 0, 1) * Time.deltaTime * 10;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0, 0, -1) * Time.deltaTime * 10;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * 10;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0) * Time.deltaTime * 10;
        }
    }
}
