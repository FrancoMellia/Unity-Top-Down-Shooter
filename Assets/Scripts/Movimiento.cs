using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movimiento : MonoBehaviour {

	public bool moviendo = false;
	float speed = 5.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
//		MirarAlCursor();
//		if(moviendo){
		MovimientoPj();
	/*	}
		else MovCheck();*/
	}

	/*void MirarAlCursor(){
		var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
		var angle = Mathf.Atan2(dir.y,dir.x)* Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
	}*/

	public void setMoving(bool val){
		moviendo = val;
	}

	void MovimientoPj(){
		
		if(Input.GetKey(KeyCode.W)){
			this.transform.Translate(Vector3.up*speed*Time.deltaTime,Space.World);
			moviendo = true;
		}
		if(Input.GetKey(KeyCode.A)){
			this.transform.Translate(Vector3.left*speed*Time.deltaTime,Space.World);
			moviendo = true;
		}
		if(Input.GetKey(KeyCode.S)){
			this.transform.Translate(Vector3.down*speed*Time.deltaTime,Space.World);
			moviendo = true;
		}
		if(Input.GetKey(KeyCode.D)){
			this.transform.Translate(Vector3.right*speed*Time.deltaTime,Space.World);
			moviendo = true;
		}	
		if(Input.GetKey(KeyCode.D) != true && Input.GetKey(KeyCode.A) != true && Input.GetKey(KeyCode.W) != true && Input.GetKey(KeyCode.S) != true)
		{
			moviendo = false;
		}
	}

 /*	void MovCheck(){
		if(Input.GetKey(KeyCode.D) != false && Input.GetKey(KeyCode.A) != false && Input.GetKey(KeyCode.W) != false && Input.GetKey(KeyCode.S) != false)
		{
			moviendo = false;
		}
		else moviendo = true;
	}*/
}
