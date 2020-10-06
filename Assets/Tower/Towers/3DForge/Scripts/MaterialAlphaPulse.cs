using UnityEngine;
using System.Collections;

/// <summary>
/// MaterialAlphaPulse.
/// Writen by Nico Schoeman on 7/24/2013.
/// Email: info@3dforge.co.za
/// 
/// fades and solidifies all objects defined in the to_alpha array at a defined speed. 
/// Use the min_alpha and max_alpha to limit the change range.
/// able to change material colour. fading only works with a transparent shader, 
/// will automatically revert to transparent/diffuse if chosen shader is not a transparent shader.
/// </summary>

[RequireComponent(typeof(Material))]
public class MaterialAlphaPulse : MonoBehaviour {
	public Transform[] to_alpha = new Transform[0];
	public bool allow_colour_change = true;
	public Color32 color_pick;
	public bool loop = true;
	public StartMode start_mode;
	public float change_speed = 0.01f;
	public float max_alpha = 1f;
	public float min_alpha = 0f;
	private float current_alpha = 0f;
	private bool isfading = false;
	
	public enum StartMode {
		faded, solid
	}
	
	void Start () {
		if (allow_colour_change == true) {
			foreach (Transform obj in to_alpha) {
				obj.gameObject.GetComponent<Renderer>().material.color = color_pick;
			}
		}
		
		if (start_mode == StartMode.solid) {
			current_alpha = 1f;
			isfading = true;
		}else{
			current_alpha = 0f;
			isfading = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Transform obj in to_alpha) {
			if (!obj.gameObject.GetComponent<Renderer>().material.shader.name.Contains("Transparent")) {
				if (!obj.gameObject.GetComponent<Renderer>().material.shader.name.Contains("Particles")) {
					obj.gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse");
				}
			}
			if (obj.gameObject.GetComponent<Renderer>().material.shader.name.Contains("Particles")) {
				Color32 color = new Color(obj.GetComponent<Renderer>().material.color.r, obj.GetComponent<Renderer>().material.color.g, obj.GetComponent<Renderer>().material.color.b, current_alpha);
				obj.gameObject.GetComponent<Renderer>().material.SetColor("_TintColor", color);
			}else{
				Color32 color = new Color(obj.GetComponent<Renderer>().material.color.r, obj.GetComponent<Renderer>().material.color.g, obj.GetComponent<Renderer>().material.color.b, current_alpha);
				obj.gameObject.GetComponent<Renderer>().material.SetColor("_Color", color);
			}
		}
		
		if (max_alpha > 1f) {max_alpha = 1f;}
		if (min_alpha < 0f) {min_alpha = 0f;}
		
		if (loop == true) {
			if (isfading == true && current_alpha >= min_alpha) {
				Fade();
			}else if (isfading == false && current_alpha <= max_alpha) {
				Solidify();
			}
		}
		
		if (current_alpha <= min_alpha) {isfading = false;}
		if (current_alpha >= max_alpha) {isfading = true;}
	}
	
	void Fade () {
		current_alpha = current_alpha - change_speed;
	}
	
	void Solidify () {
		current_alpha = current_alpha + change_speed;
	}
}
