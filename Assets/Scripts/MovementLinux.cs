using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLinux : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public float sensitivity;
    public Transform cameraTransform;
    private float prevYaw = 0;
    void Update()
    {
        float rotateHorizontal = Input.GetAxis("Mouse X");
        cameraTransform.Rotate(transform.up * rotateHorizontal * sensitivity);

    }
    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetCursorLock(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetCursorLock(false);
        }
    }
    private void SetCursorLock(bool lockCursor)
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
