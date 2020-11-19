using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceAction : InteractionObject,IRotate
{
    [SerializeField]private float _rotateStep = 1;
    public void RotateLeft()
    {
        transform.Rotate(0,-_rotateStep,0);
    }

    public void RotateRight()
    {
        transform.Rotate(0,_rotateStep,0);
    }
    public override void StartInteraction(HudView hud)
    {
        if(!CameraPosition.CameraPosition)
            return;
        OrbitCamera camera = Camera.main.GetComponent<OrbitCamera>();
        camera.SetCamera(CameraPosition.CameraPosition.position, this.transform);
        //hud.ActiveOpen(InteractionView);
    }

    public override void StopInteraction()
    {
        //InteractionView.Close();
    }
    public override void UpdateInput()
    {
        if (Input.GetKey(KeyCode.E))
        {
            RotateRight();
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            RotateLeft();
        }
    }
}
