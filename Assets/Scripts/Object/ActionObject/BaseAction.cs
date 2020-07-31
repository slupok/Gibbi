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


public abstract class BaseAction : MonoBehaviour
{

    public Sprite Sprite;
    protected bool _isActive = false;
    
    public abstract void StartAction(HudView view);

    public abstract void StopAction(HudView view);
    
}
