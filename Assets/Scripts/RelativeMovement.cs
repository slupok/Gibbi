using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))] 
public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform _targetCamera; 
    public Animator Animator;
    public float RotSpeed = 15.0f;
    public float moveSpeed = 6.0f;
    private CharacterController _characterController;
    private bool _runFlag;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }
    void Update() {
        Vector3 movement = Vector3.zero;
        float horInput = Input.GetAxis("Horizontal") ;
        float vertInput = Input.GetAxis("Vertical");
        if (horInput != 0 || vertInput != 0) 
        {
            Animator.SetBool("IsRun",true);
            _runFlag = true;
            
            movement.x = horInput * moveSpeed;
            movement.z = vertInput * moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed);
            Quaternion tmp = _targetCamera.rotation; 
            _targetCamera.eulerAngles = new Vector3(0, _targetCamera.eulerAngles.y, 0);
            movement = _targetCamera.TransformDirection(movement); 
            _targetCamera.rotation = tmp;
            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation,direction, RotSpeed * Time.deltaTime);
        }
        else
        {
            if (_runFlag)
            {
                Animator.SetBool("IsRun",false);
                _runFlag = false;
            }
        }
        movement *= Time.deltaTime; 
        _characterController.Move(movement);
    }
}
