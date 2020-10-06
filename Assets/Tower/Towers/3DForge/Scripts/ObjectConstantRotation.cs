using UnityEngine;
using System.Collections;

/// <summary>
/// ObjectConstantRotation.
/// Writen by Nico Schoeman on 7/23/2013.
/// Email: info@3dforge.co.za
/// 
/// rotates all objects defined in the to_rotate array at a defined speed on a defined axis
/// </summary>

public class ObjectConstantRotation : MonoBehaviour {
	public Transform[] to_rotate = new Transform[0];
	public axis_to_rotate_on rotation_axis;
	public float rotation_speed;
	private Vector3 axis;
		
	public enum axis_to_rotate_on {
		x_axis,y_axis,z_axis
	}
	
	void Start () {
		if (rotation_axis == axis_to_rotate_on.x_axis) {
			axis = Vector3.right;
		}else if (rotation_axis == axis_to_rotate_on.y_axis) {
			axis = Vector3.up;
		}else if (rotation_axis == axis_to_rotate_on.z_axis){
			axis = Vector3.forward;
		}
	}
	
	void Update () {
		foreach (Transform objecttorotate in to_rotate) {
			objecttorotate.Rotate(axis * rotation_speed * Time.deltaTime);
		}
	}
}
