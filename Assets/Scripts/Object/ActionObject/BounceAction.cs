using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceAction : BaseAction,IRotate
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
    public override void StartAction(HudView view)
    {
        _isActive = true;
        view?.ActiveOpen(Sprite);
    }

    public override void StopAction(HudView view)
    {
        _isActive = false;
        view?.ActiveClose();
    }
    private void Update()
    {
        if(!_isActive)
            return;
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
