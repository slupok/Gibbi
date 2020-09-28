using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : InteractionObject
{
    public RelativeMovement Movement;
    public override void StartInteraction(HudView view, OrbitCamera camera)
    {
        if(!CameraPosition)
            return;
        camera.SetCamera(CameraPosition.position, this.transform);
        view.ActiveOpen(Sprite);
    }
    public override void StopInteraction(HudView view)
    {
        view.ActiveClose();
    }

    public override void UpdateInput()
    {
        //if (Input.GetMouseButton(1))
        {
            Movement.UpdateMovement();
        }

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) 
            {
                var obj = hit.collider.GetComponent<InteractionObject>();
                if (obj)
                {
                    SetInteractionObjectEvent?.Invoke(obj);//устанавливаем новый объект взаимодествия
                }
            }
        }
    }
    private void Start()
    {
        Movement = GetComponent<RelativeMovement>();
    }
    
}
