using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burrow : MonoBehaviour
{
    public Game Game;
    private void OnTriggerEnter(Collider other)
    {
        if (Game==null)
        {
           return; 
        }
        var chesse = other.GetComponent<Chesse>();
        if (chesse != null)
        {
            Game.EndGame();
            Destroy(other.gameObject);
        }
    }
}
