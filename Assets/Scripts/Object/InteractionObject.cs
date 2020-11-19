using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WithCamera
{
    public bool UsingCamera;
    public Transform CameraPosition;
}
public class InteractionObject : BaseInteractionObject
{
    //[Tooltip("Какие элементы управления игрока будут доступны при взаимодействии с текущим объектом")]
    public AllowPlayerControl AllowsPlayerControl;
    public WithCamera CameraPosition;
    public override void StartInteraction(HudView hud)
    {
        if (CameraPosition.UsingCamera == false)
        {
            if(!CameraPosition.CameraPosition)
                return;
            OrbitCamera camera = Camera.main.GetComponent<OrbitCamera>();
            camera.SetCamera(CameraPosition.CameraPosition.position, this.transform);
        }
        //hud.ActiveOpen(InteractionView);
    }
    public override void StopInteraction()
    {
        //InteractionView.Close();
    }
    public override void UpdateInput()
    {
        
    }
    
    
}
