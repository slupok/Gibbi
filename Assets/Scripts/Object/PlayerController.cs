using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    public NavMeshAgent Agent;
    //[HideInInspector]public Vector3 Target;
    public GameObject FlagPrefab;
    public Animator Animator;
    public HudView HudView;
    private BaseAction _actionTarget;
    //находимся ли мы на цели
    //или еще движемся к ней
    private bool _isSucceed = true;

    public void OnEnable()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    private void SetTarget(Vector3 target)
    {
        if (!Agent.SetDestination(target))
        {
            Unsucceed();
        }
        if (_isSucceed)
        { //если мы стояли на таргете
            _isSucceed = false;
            Animator.SetBool("IsRun",true);
        }
        if(FlagPrefab == null)
           return;
        var flag = Instantiate(FlagPrefab, target, Quaternion.identity);
        Destroy(flag,0.5f);
    }
    //достижение цели
    private void Succeed()
    {
        if (_isSucceed)
            return;//мы  и так на цели
        _isSucceed = true;
        Animator.SetBool("IsRun",false);
        _actionTarget?.StartAction(HudView);
    }

    //не удается достичь цели
    private void Unsucceed()
    {
        
    }
    
    
    public void Update()
    {
        if (HudView == null)
        {
            return;
        }
        if (Agent.remainingDistance <= Agent.stoppingDistance)
        {
            Succeed();
        }
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) 
            {
                var action = hit.collider.GetComponent<BaseAction>();
                if (_actionTarget != null)
                {
                    _actionTarget.StopAction(HudView);
                }
                /*if (action != null)
                    _actionTarget = action;*/
                _actionTarget = action != null ? action : null;
                SetTarget(hit.point);
                
            }
        }
        
    }
}
