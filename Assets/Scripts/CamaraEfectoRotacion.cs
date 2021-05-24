using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraEfectoRotacion : MonoBehaviour {

Movimiento mov;
float timer = 0.1f;
float mod = 0.1f;
float zVal = 0.0f;

	// Use this for initialization
	void Start () {
		mov = GameObject.FindGameObjectWithTag("Jugador").GetComponent<Movimiento>();
	}
	
	// Update is called once per frame
	void Update () {
		if(mov.moviendo == true){

			Vector3 rotacion = new Vector3 (0,0,zVal);
			this.transform.eulerAngles = rotacion;

			timer -= Time.deltaTime;
			if(timer <= 0){
				zVal += mod;
			}

			if(transform.eulerAngles.z >= 5f && transform.eulerAngles.z <= 10f ){
				mod = -0.1f;
			}
			else if(transform.eulerAngles.z < 355f && transform.eulerAngles.z > 350f ){
				mod = 0.1f;		
			}

		}
	}
}
