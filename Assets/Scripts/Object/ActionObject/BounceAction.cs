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
