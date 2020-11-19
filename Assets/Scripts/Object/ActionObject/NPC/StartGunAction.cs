using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGunAction : InteractionObject,IGun,IRotate
{
    public GameObject Chesse;
    public Transform ShotPos;
    [SerializeField]private float _rotateStep = 1;
    private Animator _animator;
    private bool _activeChesse = true;

    public LaserManager LaserManager;


    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
    }

    //private bool _isActive = false;
    public void PathLine()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        LaserManager?.RemoveOldLine();
        LaserManager?.CalcLine(ShotPos.position,forward);
        //Debug.DrawRay(ShotPos.position, forward, Color.yellow);
        
    }
    public void Shot()
    {
        if (Chesse == null || ShotPos == null || !_activeChesse)
            return;
        _animator?.SetTrigger("start");
        ActiveChesse(false);
        StartCoroutine(AwaitAnimation());
        //Chesse = null;
    }
    IEnumerator AwaitAnimation()
    {
        yield return new WaitForSeconds(0.6f);
        var chesse = Instantiate(Chesse, ShotPos.position, ShotPos.rotation).GetComponent<Chesse>();
        chesse.SetOldGun(this);
    }
    public void RotateLeft()
    {
        transform.Rotate(0,-_rotateStep,0);
    }

    public void RotateRight()
    {
        transform.Rotate(0,_rotateStep,0);
    }


    public override void StartInteraction(HudView hud)
    {
        base.StartInteraction(hud);
    }

    public override void StopInteraction()
    {
        base.StopInteraction();
    }

    public void ActiveChesse( bool flag)
    {
        _activeChesse = flag;
    }

    private void Update()
    {
        PathLine();
    }
    public override void UpdateInput()
    {
        if (Chesse == null)
        {
            return;
        }
        //PathLine();

        if (Input.GetKey(KeyCode.E))
        {
            RotateRight();
        }
        
        else if (Input.GetKey(KeyCode.Q))
        {
            RotateLeft();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shot();
        }
    }

}
