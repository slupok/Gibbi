using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
    public Player Player;
    
    //public OrbitCamera OrbitCamera;
    public HudView HudView;
    
    private InteractionObject _baseInteractionObject;//объект управления

    private bool _allowPlayerControll = true;
    
    private void Start()
    {
        //SetObject(null);
        SetPlayer();
    }

    public void SetPlayer()
    {

        Player.StartInteraction(HudView);
    }
    
    //если параметр obj имеет null, то это значит что необходимо передать управление персонажем
    public void SetObject(InteractionObject obj)
    {
        Player.SetPlayerControl(obj.AllowsPlayerControl);
        
        if (_baseInteractionObject != null)
        {
            //если существовал предыдущий объект управления
            _baseInteractionObject.StopInteraction();
            //_baseInteractionObject.SetInteractionObjectEvent -= SetObject; 
        }

        /*if (obj == null)
        {
            SetPlayer();
            return;
        }*/
        _baseInteractionObject = obj;
        //_baseInteractionObject.SetInteractionObjectEvent += SetObject;
        _baseInteractionObject.StartInteraction(HudView);
    }

    private void ResetObject()
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        //управление текущим объектом
        if(_allowPlayerControll)
            Player.UpdateInput();
        
        if (_baseInteractionObject == null)
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    InteractionObject obj = hit.collider.GetComponent<InteractionObject>();
                    if (obj)
                    {
                        SetObject(obj); //устанавливаем новый объект взаимодествия
                    }
                }
            }
        }
        else
        {
            //управление текущим объектом
            _baseInteractionObject.UpdateInput();
        }
    }
}
