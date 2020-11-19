using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Chesse chesse = other.gameObject.GetComponent<Chesse>();
        if (chesse != null)
        {
            chesse.DestroyChesse();
        }
    }
}
