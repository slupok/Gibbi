using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Chesse : MonoBehaviour
{
    [HideInInspector]public StartGunAction OldGun;
    public float Speed;
    private Rigidbody _rb;
    private Vector3 _vel;
    
    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = transform.TransformDirection(Vector3.forward * Speed);
    }

    public void SetOldGun(StartGunAction gun)
    {
        OldGun = gun;
    }

    public void DestroyChesse()
    {
        OldGun.ActiveChesse(true);
        Destroy(gameObject);
    }
    
}
