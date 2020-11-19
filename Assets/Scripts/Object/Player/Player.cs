using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : BaseInteractionObject
{
    private AllowPlayerControl _allowsPlayerControl = new AllowPlayerControl(true);
    public Transform CameraPosition;
    private RelativeMovement _movement;
    public override void StartInteraction(HudView hud)
    {
        if(!CameraPosition)
            return;
        OrbitCamera camera = Camera.main.GetComponent<OrbitCamera>();
        camera.SetCamera(CameraPosition.position, this.transform);
        //view.ActiveOpen(InteractionView);
    }
    public override void StopInteraction()
    {
        //InteractionView.Close();
    }

    public override void UpdateInput()
    {
        if(_allowsPlayerControl._moving)
            _movement.UpdateMovement();
        
    }

    public void SetPlayerControl(AllowPlayerControl control)
    {
        _allowsPlayerControl = (AllowPlayerControl) control.Clone();
    }
    private void Start()
    {
        _movement = GetComponent<RelativeMovement>();
    }
    
}
