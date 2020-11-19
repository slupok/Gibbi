using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//namespace InreractionObject

    public interface IGun
    {
        void Shot(); //выстрел
        void PathLine(); //траектория полета
    }

    public interface IRotate
    {
        void RotateLeft();
        void RotateRight();
    }

public interface ICloneable
{
    object Clone();
}
    [Serializable]
    public class AllowPlayerControl:ICloneable
    {
        public bool _moving;
        public bool _skills;
        public bool _cameraRotation;

        public AllowPlayerControl()
        {
            
        }
        public AllowPlayerControl(bool value)
        {
            _moving = value;
            _skills = value;
            _cameraRotation = value;
        }
        public object Clone()
        {
            AllowPlayerControl allowControl = new AllowPlayerControl();
            allowControl._moving = this._moving;
            allowControl._skills = this._skills;
            allowControl._cameraRotation = this._cameraRotation;
            return allowControl;
        }
    }
    public abstract class BaseInteractionObject : MonoBehaviour
    {

        //соответсвенный элемент UI для взаимодействия с объектом
        //public InteractionObjectView InteractionView;

        //сделать инциалихации камеры внутри метода через синглтон MainCamera
        public abstract void StartInteraction(HudView hud);
        //public abstract void StartInteraction(HudView hud, OrbitCamera camera);

        public abstract void UpdateInput();
        public abstract void StopInteraction();

        void OnDrawGizmos()
        {
            //Gizmos.DrawIcon(CameraPosition.position, "CameraGizmo.png", true); 
        }
    }

