using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
    public Player Player;
    
    public OrbitCamera OrbitCamera;
    public HudView HudView;
    
    private InteractionObject _interactionObject;//объект управления

    private void Start()
    {
        SetObject(null);
        //SetPlayer();
    }

    public void SetPlayer()
    {
        _interactionObject = Player;
        _interactionObject.SetInteractionObjectEvent += SetObject;
        _interactionObject.StartInteraction(HudView,OrbitCamera);
    }
    
    //если параметр obj имеет null, то это значит что необходимо передать управление персонажем
    public void SetObject(InteractionObject obj)
    {
        if (_interactionObject)
        {
            //если существовал предыдущий объект управления
            _interactionObject.StopInteraction(HudView);
            _interactionObject.SetInteractionObjectEvent -= SetObject; 
        }

        if (!obj)
        {
            SetPlayer();
            return;
        }
        _interactionObject = obj;
        _interactionObject.SetInteractionObjectEvent += SetObject;
        _interactionObject.StartInteraction(HudView,OrbitCamera);
    }
    // Update is called once per frame
    void Update()
    {
        //управление текущим объектом
        _interactionObject.UpdateInput();
    }
}
