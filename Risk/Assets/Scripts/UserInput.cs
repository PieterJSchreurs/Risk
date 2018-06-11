using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS;

public class UserInput : MonoBehaviour
{ 
    public static Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
            MoveCamera();
            RotateCamera();
            MouseActivity();
    }

    private void MouseActivity()
    {
        if (Input.GetMouseButtonDown(0)) LeftMouseClick();
        else if (Input.GetMouseButtonDown(1)) RightMouseClick();
    }

    private void LeftMouseClick()
    {
        GameObject hitObject = FindHitObject();
        if(hitObject.GetComponent<ISelectAble>() != null && hitObject.tag == "Country")
        {
            //Country country = hitObject.GetComponent<Country>();
            //country.Select();
            //Player player = PlayerTurnHandler.GetCurrentPlayer();
            //country.SetPlayerOwner(player);
        }
    }

    private GameObject FindHitObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) return hit.collider.gameObject;
        return null;
    }

    private void RightMouseClick()
    {
        //if (player.hud.MouseInBounds() && !Input.GetKey(KeyCode.LeftAlt) && player.SelectedObject)
        //{
        //    player.SelectedObject.SetSelection(false);
        //    player.SelectedObject = null;
        //}
    }

    private void RotateCamera()
    {
        Vector3 origin = mainCamera.transform.eulerAngles;
        Vector3 destination = origin;

        //detect rotation amount if ALT is being held and the Right mouse button is down
        if ((Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Q)) && Input.GetMouseButton(1))
        {
            destination.x -= Input.GetAxis("Mouse Y") * ResourceManager.RotateAmount;
            destination.y += Input.GetAxis("Mouse X") * ResourceManager.RotateAmount;
        }

        //if a change in position is detected perform the necessary update
        if (destination != origin)
        {
            mainCamera.transform.eulerAngles = Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.RotateSpeed);
        }
    }

    private void MoveCamera()
    {
        //Mouse input & screen widths/heights
        float xpos = Input.mousePosition.x;
        float ypos = Input.mousePosition.y;
        float screenX = Screen.width;
        float screenY = Screen.height;

        ScreenScroller(xpos, ypos, screenX, screenY);
       
    }

    private void ScreenScroller(float xpos, float ypos, float screenX, float screenY)
    {
        //if (xpos < 0 || xpos > screenX || ypos < 0 || ypos > screenY)
        //    return;

        Vector3 movement = new Vector3(0, 0, 0);

        if (xpos >= screenX * 0.95 || Input.GetKey(KeyCode.D))
        {
            movement.x += ResourceManager.ScrollSpeed;
        }
        if (xpos <= screenX * 0.05 || Input.GetKey(KeyCode.A))
        {
            movement.x -= ResourceManager.ScrollSpeed;
        }

        if (ypos >= screenY * 0.95 || Input.GetKey(KeyCode.W))
        {
            movement.y += ResourceManager.ScrollSpeed;
        }
        if (ypos <= screenY * 0.05 || Input.GetKey(KeyCode.S))
        {
            movement.y -= ResourceManager.ScrollSpeed;
        }

        movement = mainCamera.transform.TransformDirection(movement);
        movement.y = 0;

        //away from ground movement
        movement.y -= ResourceManager.ScrollSpeed * Input.GetAxis("Mouse ScrollWheel");

        //calculate desired camera position based on received input
        Vector3 origin = mainCamera.transform.position;
        Vector3 destination = origin;
        destination.x += movement.x;
        destination.y += movement.y;
        destination.z += movement.z;

        //limit away from ground movement to be between a minimum and maximum distance
        if (destination.y > ResourceManager.MaxCameraHeight)
        {
            destination.y = ResourceManager.MaxCameraHeight;
        }
        else if (destination.y < ResourceManager.MinCameraHeight)
        {
            destination.y = ResourceManager.MinCameraHeight;
        }

        //if a change in position is detected perform the necessary update
        if (destination != origin)
        {
            mainCamera.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.ScrollSpeed);
        }
    }
}
