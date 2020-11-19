using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))] 
public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform _targetCamera; 
    public Animator Animator;
    
    [Header("Свойства игрока")]
    public float RotSpeed = 15.0f;
    public float MoveSpeed = 6.0f;
    
    //для прыжков
    public float JumpSpeed = 15.0f;
    public float Gravity = 9.8f;

    private CharacterController _characterController;
    private Vector3 _moveDirection = Vector3.zero;
    private bool _runFlag;
    
    private bool _isPressedRMB = false;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }
    public void UpdateMovement() 
    {
        if (Input.GetMouseButton(1))
        {
            
            Quaternion direction = Quaternion.Euler(0, _targetCamera.eulerAngles.y, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation,direction, RotSpeed * Time.deltaTime);
            _isPressedRMB = true;
        }
        else
        {
            _isPressedRMB = false;
        }
        
        if (_characterController.isGrounded)
        {

            _moveDirection = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
            if (_moveDirection.magnitude != 0)
            {
                Animator.SetBool("IsRun",true);
                _runFlag = true;
                
                _moveDirection.Normalize();
                _moveDirection *= MoveSpeed;

                if (_isPressedRMB)
                {
                    //движение прямо относительно камеры
                    Quaternion tmp = _targetCamera.rotation;
                    _targetCamera.eulerAngles = new Vector3(0, _targetCamera.eulerAngles.y, 0);
                    _moveDirection = _targetCamera.TransformDirection(_moveDirection);
                    _targetCamera.rotation = tmp;
                }
                else
                {
                    //движение прямо относительно игрока
                    _moveDirection = transform.TransformDirection(_moveDirection);
                }
            }
            else
            {
                if (_runFlag)
                {
                    Animator.SetBool("IsRun",false);
                    _runFlag = false;
                }
            }
            if (Input.GetButton("Jump"))
            {
                _moveDirection.y = JumpSpeed;
            }

        }
        _moveDirection.y -= Gravity * Time.deltaTime;
        _characterController.Move(_moveDirection * Time.deltaTime);

    }
    
}
