using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[AddComponentMenu("Camera-Control/Smooth Mouse Look")]
public class CameraController : MonoBehaviour
{
	public Transform HeadLook;//для вращения камерой с головой( при зажатой ПКМ)
	public Transform CameraLook;//для вращения камеры без головы( осмотр )
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes Axes = RotationAxes.MouseXAndY;
	public float SensitivityX = 15F;
	public float SensitivityY = 15F;
 
	public float MinimumX = -360F;
	public float MaximumX = 360F;
 
	public float MinimumY = -60F;
	public float MaximumY = 60F;
 
	private float RotationX = 0F;
	private float RotationY = 0F;
 
	private List<float> _rotArrayX = new List<float>();
	private float _rotAverageX = 0F;	
 
	private List<float> _rotArrayY = new List<float>();
	private float _rotAverageY = 0F;
 
	public float FrameCounter = 20;
 
	private Quaternion _originalRotation;
	
	void Update ()
	{
		HeadRotate();
		/*if (Input.GetMouseButton(1))
		{
			HeadRotate();
		}
		else
		{
			ViewRotate();
		}*/
		/*else if (Axes == RotationAxes.MouseX)
		{			
			_rotAverageX = 0f;
 
			RotationX += Input.GetAxis("Mouse X") * SensitivityX;
 
			_rotArrayX.Add(RotationX);
 
			if (_rotArrayX.Count >= FrameCounter) {
				_rotArrayX.RemoveAt(0);
			}
			for(int i = 0; i < _rotArrayX.Count; i++) {
				_rotAverageX += _rotArrayX[i];
			}
			_rotAverageX /= _rotArrayX.Count;
 
			_rotAverageX = ClampAngle (_rotAverageX, MinimumX, MaximumX);
 
			Quaternion xQuaternion = Quaternion.AngleAxis (_rotAverageX, Vector3.up);
			transform.localRotation = _originalRotation * xQuaternion;			
		}
		else
		{			
			_rotAverageY = 0f;
 
			RotationY += Input.GetAxis("Mouse Y") * SensitivityY;
 
			_rotArrayY.Add(RotationY);
 
			if (_rotArrayY.Count >= FrameCounter) {
				_rotArrayY.RemoveAt(0);
			}
			for(int j = 0; j < _rotArrayY.Count; j++) {
				_rotAverageY += _rotArrayY[j];
			}
			_rotAverageY /= _rotArrayY.Count;
 
			_rotAverageY = ClampAngle (_rotAverageY, MinimumY, MaximumY);
 
			Quaternion yQuaternion = Quaternion.AngleAxis (_rotAverageY, Vector3.left);
			HeadLook.localRotation = _originalRotation * yQuaternion;
		}*/
	}
 
	void Start ()
	{		
		Rigidbody rb = GetComponent<Rigidbody>();	
		if (rb)
			rb.freezeRotation = true;
		_originalRotation = transform.localRotation;
	}

	public void HeadRotate()
	{
		
		_rotAverageY = 0f;
		_rotAverageX = 0f;
 
		RotationY += Input.GetAxis("Mouse Y") * SensitivityY;
		RotationX += Input.GetAxis("Mouse X") * SensitivityX;
 
		_rotArrayY.Add(RotationY);
		_rotArrayX.Add(RotationX);
 
		if (_rotArrayY.Count >= FrameCounter) {
			_rotArrayY.RemoveAt(0);
		}
		if (_rotArrayX.Count >= FrameCounter) {
			_rotArrayX.RemoveAt(0);
		}
 
		for(int j = 0; j < _rotArrayY.Count; j++) {
			_rotAverageY += _rotArrayY[j];
		}
		for(int i = 0; i < _rotArrayX.Count; i++) {
			_rotAverageX += _rotArrayX[i];
		}
 
		_rotAverageY /= _rotArrayY.Count;
		_rotAverageX /= _rotArrayX.Count;
 
		_rotAverageY = ClampAngle(_rotAverageY, MinimumY, MaximumY);
		_rotAverageX = ClampAngle(_rotAverageX, MinimumX, MaximumX);
 
		Quaternion yQuaternion = Quaternion.AngleAxis (_rotAverageY, Vector3.left);
		Quaternion xQuaternion = Quaternion.AngleAxis (_rotAverageX, Vector3.up);
		
		transform.localRotation = _originalRotation * xQuaternion;// * yQuaternion;
		HeadLook.localRotation = _originalRotation * yQuaternion;

	}
	public void ViewRotate()
	{
		
		_rotAverageY = 0f;
		_rotAverageX = 0f;
 
		RotationY += Input.GetAxis("Mouse Y") * SensitivityY;
		RotationX += Input.GetAxis("Mouse X") * SensitivityX;
 
		_rotArrayY.Add(RotationY);
		_rotArrayX.Add(RotationX);
 
		if (_rotArrayY.Count >= FrameCounter) {
			_rotArrayY.RemoveAt(0);
		}
		if (_rotArrayX.Count >= FrameCounter) {
			_rotArrayX.RemoveAt(0);
		}
 
		for(int j = 0; j < _rotArrayY.Count; j++) {
			_rotAverageY += _rotArrayY[j];
		}
		for(int i = 0; i < _rotArrayX.Count; i++) {
			_rotAverageX += _rotArrayX[i];
		}
 
		_rotAverageY /= _rotArrayY.Count;
		_rotAverageX /= _rotArrayX.Count;
 
		_rotAverageY = ClampAngle(_rotAverageY, MinimumY, MaximumY);
		_rotAverageX = ClampAngle(_rotAverageX, MinimumX, MaximumX);
 
		Quaternion yQuaternion = Quaternion.AngleAxis (_rotAverageY, Vector3.left);
		Quaternion xQuaternion = Quaternion.AngleAxis (_rotAverageX, Vector3.up);

		
		CameraLook.localRotation = _originalRotation * xQuaternion;
		
	}
	public static float ClampAngle (float angle, float min, float max)
	{
		angle = angle % 360;
		if ((angle >= -360F) && (angle <= 360F)) {
			if (angle < -360F) {
				angle += 360F;
			}
			if (angle > 360F) {
				angle -= 360F;
			}			
		}
		return Mathf.Clamp (angle, min, max);
	}
}
