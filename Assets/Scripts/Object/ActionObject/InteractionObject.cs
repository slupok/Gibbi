using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGun
{
    void Shot();//выстрел
    void PathLine();//траектория полета
}

public interface IRotate
{
    void RotateLeft();
    void RotateRight();
}

public abstract class InteractionObject : MonoBehaviour
{
    public Action<InteractionObject> SetInteractionObjectEvent;
    
    public Transform CameraPosition;
    
    public Sprite Sprite;

    public abstract void StartInteraction(HudView view, OrbitCamera camera);

    public abstract void UpdateInput();
    public abstract void StopInteraction(HudView view);
    void OnDrawGizmos() { 
        //Gizmos.DrawIcon(CameraPosition.position, "CameraGizmo.png", true); 
    }
}
