using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovingState
{
    Walk,
    Run,
    SitDown
}
[RequireComponent(typeof(CharacterController))]
public class Move : MonoBehaviour
{
    [HideInInspector]public MovingState State;
    
    public float WalkSpeed = 3F;
    public float FastSpeed = 4f;
    //!!необходимо ожидать пока завершиться анимация ускорения/замедления
    //для того, чтобы перейти к другой анимации
    public float TransitionSpeedTime = 0.3f;
    
    public float Gravity = 20.0F;
    public float _currentSpeed;
    private Vector3 _moveDirection = Vector3.zero;
    private CharacterController _controller;
    void Start()
    {
        Walk();
        _controller = GetComponent<CharacterController>();
    }
    void Update() {
        if (_controller.isGrounded) {
            _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _moveDirection = transform.TransformDirection(_moveDirection);
            _moveDirection *= _currentSpeed;
        }
        _moveDirection.y -= Gravity * Time.deltaTime;
        _controller.Move(_moveDirection * Time.deltaTime);
    }

    public void Run()
    {
        if( State == MovingState.Walk || State == MovingState.SitDown)
            Acceleration();
        else
            _currentSpeed = FastSpeed;
        State = MovingState.Run;
    }

    public void Walk()
    {
        if(State == MovingState.Run)
            Deceleration();
        else
            _currentSpeed = WalkSpeed;
        State = MovingState.Walk;
    }
    
    private void Acceleration()
    {
        StartCoroutine(AccelerationCoroutine());
    }
    private void Deceleration()
    {
        StartCoroutine(DecelerationCoroutine());
    }
    IEnumerator AccelerationCoroutine()
    {
        int n = 10;
        float deltaSpeed = (FastSpeed - WalkSpeed)/n;
        for (int i = 0; i < n; i++)
        {
            /*if (State != MovingState.Run)
            {
                yield break;
            }*/
            yield return new WaitForSeconds(TransitionSpeedTime/10);
            _currentSpeed += deltaSpeed;
        }
        //на всякий случай делаем Run
        _currentSpeed = FastSpeed;
    }
    IEnumerator DecelerationCoroutine()
    {
        int n = 10;
        float deltaSpeed = (FastSpeed - WalkSpeed)/n;
        for (int i = 0; i < n; i++)
        {
            /*if (State != MovingState.Walk)
            {
                yield break;
            }*/
            yield return new WaitForSeconds(TransitionSpeedTime/10);
            _currentSpeed -= deltaSpeed;
        }
        //на всякий случай делаем Walk
        _currentSpeed = WalkSpeed;
    }
}
