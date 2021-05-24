using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirarAlCursor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		MirarAlCursor1();
	}
	void MirarAlCursor1(){
		var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
		var angle = Mathf.Atan2(dir.y,dir.x)* Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
	}
}
