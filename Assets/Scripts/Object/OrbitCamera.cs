using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//это надо вообще? 
//пока оставлю, вдруг нужно будет)
//
//может понадобиться, если при взаимодействии с некоторыми объектами,
//необходимо определенные действия камерой
public enum StateCamera
{
    none,
    move,
    zoom,
    all
    
}
public class OrbitCamera : MonoBehaviour
{
    [SerializeField] private Player _player;
    private Transform _target;
    private Vector3 StartPosition;
    
    private bool _isPressedRMB = false;
    
    public float RotSpeed = 1.5f;
    private float _rotX;
    private float _rotY;
    
    public float MinimumY = -40F;
    public float MaximumY = 30F;
    
    private Vector3 _offset;
    void LateUpdate() 
    {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
        {
            _isPressedRMB = true;
            Cursor.visible = false;
        }
        if (Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(0))
        {
            _isPressedRMB = false;
            Cursor.visible = true;

        }
        if (_isPressedRMB)
        {
            _rotY += Input.GetAxis("Mouse Y") * RotSpeed * 3;
            _rotX += Input.GetAxis("Mouse X") * RotSpeed * 3;
        }
        _rotY = ClampAngle(_rotY, MinimumY, MaximumY);
        
        Quaternion rotation = Quaternion.Euler(-_rotY, _rotX, 0);
            
        transform.position = _target.position - (rotation * _offset);
        transform.LookAt(_target); 
        transform.Rotate(-7,0,0);//поворот чуть вверх, чтобы персонаж был снизу
        
    }

    public void SetCamera(Vector3 CameraPosition,Transform target)
    {
        transform.position = CameraPosition;
        transform.LookAt(target);
        
        _target = target;
        _offset = _target.position - transform.position; 
        _rotY = transform.eulerAngles.y;
        _rotX = transform.eulerAngles.x;
    }
    public static float ClampAngle (float angle, float min, float max)
    {
        angle = angle % 360;
        if ((angle >= -360F) && (angle <= 360F)) {
            if (angle < -360F) {
                angle += 360F;
            }
            if (angle > 360F) {
                angle -= 360F;
            }			
        }
        return Mathf.Clamp (angle, min, max);
    }
}
