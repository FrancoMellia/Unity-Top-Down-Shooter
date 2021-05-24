using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgarrarArma : MonoBehaviour {
	public string nombreA;
	public float rangoFuego;
	ArmaAtaques aa;
	public bool arma,unaMano;
	//public AudioClip sfx;
	
	// Use this for initialization
	void Start () {
		aa = GameObject.FindGameObjectWithTag("Jugador").GetComponent<ArmaAtaques>();
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		
		if(coll.gameObject.tag == "Jugador" && Input.GetMouseButtonDown(1)){
            //Agregar arma al jugador
            Debug.Log("Arma: "+ nombreA);
            //Debug.Log("Collisiono");
            if (aa.GetCur () != null){
				aa.SoltarArma();
			}
			aa.SetArma(this.gameObject,nombreA,rangoFuego,arma,unaMano);
			//Destroy(this.GameObject);
			this.gameObject.SetActive(false);
		}

	}
}
