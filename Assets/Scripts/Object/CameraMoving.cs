using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CameraMoving : MonoBehaviour
{
    [SerializeField]private float speed = 15;
    private Vector3 _vel;
    private Rigidbody _rb;
    
    //для управления камерой мышкой
    //на будующее
    //private float SizeX;
   //private float SizeY;
    
    private void OnEnable()
    {
        //SizeX = Screen.width;
        //SizeY = Screen.height;
        _rb = GetComponent<Rigidbody>();
        _vel = Vector3.zero;
    }

    void Update()
    {
        _vel = transform.TransformVector(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        _rb.velocity = _vel.normalized*speed;
    }
}
