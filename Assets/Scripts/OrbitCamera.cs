using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] private Transform _player; 
    private Transform _target; 
    public float RotSpeed = 1.5f;
    private float _rotX;
    private float _rotY;
    
    public float MinimumY = -40F;
    public float MaximumY = 30F;
    
    private Vector3 _offset;
    void Start() {
        if (_player == null)
        {
            _target.position = new Vector3(0,0,0);
            return;
        }
        SetTarget(_player);
        _rotY = transform.eulerAngles.y;
        _rotX = transform.eulerAngles.x;
    
        _offset = _target.position - transform.position; 
    }
    void LateUpdate() 
    {
        _rotY += Input.GetAxis("Mouse Y") * RotSpeed * 3;
        _rotX += Input.GetAxis("Mouse X") * RotSpeed * 3;
        
        _rotY = ClampAngle(_rotY, MinimumY, MaximumY);
        
        Quaternion rotation = Quaternion.Euler(-_rotY, _rotX, 0);
        transform.position = _target.position - (rotation * _offset);
        transform.LookAt(_target); 
    }

    public void SetTarget(Transform target)
    {
        _target = target;
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
