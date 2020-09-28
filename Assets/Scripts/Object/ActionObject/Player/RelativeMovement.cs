using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))] 
public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform _targetCamera; 
    public Animator Animator;
    
    public float RotSpeed = 15.0f;
    public float MoveSpeed = 6.0f;
    
    //для прыжков
    public float JumpSpeed = 15.0f;
    public float Gravity = 9.8f;
    public float TimeJumpDelay = 0.3f;
    private bool _jumpReady = true;
    
    private CharacterController _characterController;
    private Vector3 _moveDirection = Vector3.zero;
    private bool _runFlag;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }
    public void UpdateMovement() {
        if (_characterController.isGrounded)
        {

            _moveDirection = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
            if (_moveDirection.magnitude != 0)
            {
                Animator.SetBool("IsRun",true);
                _runFlag = true;
                _moveDirection.Normalize();
                _moveDirection *= MoveSpeed;
            
                Quaternion tmp = _targetCamera.rotation; 
                _targetCamera.eulerAngles = new Vector3(0, _targetCamera.eulerAngles.y, 0);
                _moveDirection = _targetCamera.TransformDirection(_moveDirection); 
                _targetCamera.rotation = tmp;
                Quaternion direction = Quaternion.LookRotation(_moveDirection);
            
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
            //добавить возможность совершения прыжка, не ранее чем через N секунд, после последнего
            if (Input.GetButton("Jump") && _jumpReady)
            {
                _moveDirection.y = JumpSpeed;
                StartCoroutine(JumpDelayCoroutine());
            }

        }
        _moveDirection.y -= Gravity * Time.deltaTime;
        _characterController.Move(_moveDirection * Time.deltaTime);
        
        
        /*
        Vector3 movement = Vector3.zero;
        float horInput = Input.GetAxis("Horizontal") ;
        float vertInput = Input.GetAxis("Vertical");
        if (horInput != 0 || vertInput != 0) 
        {
            Animator.SetBool("IsRun",true);
            _runFlag = true;
            
            movement.x = horInput * MoveSpeed;
            movement.z = vertInput * MoveSpeed;
            movement = Vector3.ClampMagnitude(movement, MoveSpeed);
            
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

        if (_characterController.isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                _vertSpeed = JumpSpeed;
            }
            else
            {
                _vertSpeed = MinFall;
            }
        }
        else
        {
            _vertSpeed += Gravity * 5 * Time.deltaTime;
            if (_vertSpeed < MaxVelocity)
            {
                _vertSpeed = MaxVelocity;
            }
        }

        movement.y = _vertSpeed;
        movement *= Time.deltaTime; 
        _characterController.Move(movement);*/
    }

    IEnumerator JumpDelayCoroutine()
    {
        _jumpReady = false;
        yield return new WaitForSeconds(TimeJumpDelay);
        _jumpReady = true;
    }
}
