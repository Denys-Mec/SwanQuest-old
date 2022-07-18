using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class CameraController1 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

	private enum Side
	{
	
		left,
		right,
		top,
		bottom
	}

    [SerializeField] private Side _side;
	[SerializeField] private float _speed;
	[SerializeField] private float _step;
	[SerializeField] private float _constrain_coordinate;
	[SerializeField] private Camera _camera;
 
	private bool MouseButtonHeldDown = false;
	public void OnPointerDown(PointerEventData eventData)
	{
 	   
 	      MouseButtonHeldDown  = true ;
 	}
 	public void OnPointerUp(PointerEventData eventData)
 	{
 	      MouseButtonHeldDown = false;
 	}
	public void Update() 
	{
		if(!MouseButtonHeldDown)
			return;
		var time = _speed * Time.deltaTime;
		var x_position = _camera.transform.position.x;
		var y_position = _camera.transform.position.y;
		var z_position = _camera.transform.position.z;
		var local_position = new Vector3(x_position, y_position, z_position);
		var global_position = transform.TransformPoint(local_position);

		Debug.Log($"{x_position}, {y_position}");
		//Debug.Log(global_position.x);
		switch(_side)
		{

			case Side.left:
				x_position = x_position > _constrain_coordinate ?
					Mathf.Lerp(x_position, x_position - _step, time) : x_position;
				break;
			case Side.right:
				x_position = x_position < _constrain_coordinate ? 
					Mathf.Lerp(x_position, x_position + _step, time) : x_position;
				break;
			case Side.top:
				y_position = y_position < _constrain_coordinate ? 
					Mathf.Lerp(y_position, y_position + _step, time) : y_position;
				break;
			case Side.bottom:
				y_position = y_position > _constrain_coordinate ? 
					Mathf.Lerp(y_position, y_position - _step, time) : y_position;
				break;
		}


		_camera.transform.position = new Vector3(x_position, y_position, z_position);

	//	_camera.transform.position = new Vector3(10, 0, 0);
	}
}
