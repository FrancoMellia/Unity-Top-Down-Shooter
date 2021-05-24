using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DireccionPiernas : MonoBehaviour {

	Vector3 rot;
	Vector3 pose;
	public Transform target;
	public bool WD;
	public bool WA;
	public bool AS;
	public bool SD;
	float vel = 1f;


	// Use this for initialization
	void Start () {
		rot = new Vector3(0,0,0);
		pose = new Vector3(0,0,0);
		WD = false;
		WA = false;
		AS = false;
 		SD = false;
	}
	
	// Update is called once per frame
	void Update () {
		//Target
		//target.transform.position = new Vector3(this.transform.position.x,this.transform.position.y);
		
		
		//W y D
		if( Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) && WA == false && AS == false && SD == false ){
			rot = new Vector3(0,0,45);
			transform.eulerAngles = rot;
			pose = new Vector3(10,10,0);
			target.transform.position = pose;
			Apuntar();
			WA = false;
			WD = true;
			AS = false;
			SD = false;
		}
		//W
		 else TeclaW(); TeclaD();

		//-----------------------------------
		//WA
		if( Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)&& WD == false && AS == false && SD == false){
			rot = new Vector3(0,0,135);
			transform.eulerAngles = rot;
			pose = new Vector3(-10,10,0);
			target.transform.position = pose;
			Apuntar();
			WA = true;
			WD = false;
			AS = false;
			SD = false;
		}
		else TeclaW(); TeclaA();
		//-----------------------------------
		//AS
		if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)&& WD == false && WA == false && SD == false){
			rot = new Vector3(0,0,225);
			transform.eulerAngles = rot;
			pose = new Vector3(-10,-10,0);
			target.transform.position = pose;
			Apuntar();
			WA = false;
			WD = false;
			AS = true;
			SD = false;
		}
		else TeclaS(); TeclaA();
		//-----------------------------------
		//S y D
		if( Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) && WD == false && WA == false && AS == false){
			rot = new Vector3(0,0,315);
			transform.eulerAngles = rot;
			pose = new Vector3(10,-10,0);
			target.transform.position = pose;
			Apuntar();
			WA = false;
			WD = false;
			AS = false;
			SD = true;
		}
		else TeclaS(); TeclaD();
		EnFalso();
	}
	void EnFalso(){
		WD = false;
		WA = false;
		AS = false;
		SD = false;
	}
	//--------------------------------------------------------------------------------------------------------------------------------------------
	void TeclaW(){
	//W
	if(Input.GetKey(KeyCode.W) && WD == false && WA == false && AS == false && SD == false)
		{
			rot = new Vector3(0,0,90);
			transform.eulerAngles = rot;
			pose = new Vector3(0,10,0);
			target.transform.position = pose;
			Apuntar(); 
			WA = false;
			WD = false;
			AS = false;
			SD = false;
		}
	}
	void TeclaA(){
	//A
		if(Input.GetKey(KeyCode.A) && WD == false && WA == false && AS == false && SD == false)
		{
			rot = new Vector3(0,0,180);
			transform.eulerAngles = rot;
			pose = new Vector3(-10,0,0);
			target.transform.position = pose;
			Apuntar(); 
			WA = false;
			WD = false;
			AS = false;
			SD = false;
		}
	}
	void TeclaS(){
	//S
	if(Input.GetKey(KeyCode.S) && WD == false && WA == false && AS == false && SD == false)
		{
			Apuntar();
			rot = new Vector3(0,0,270);
			transform.eulerAngles = rot;
			pose = new Vector3(0,-10,0);
			target.transform.position = pose;
			WA = false;
			WD = false;
			AS = false;
			SD = false;
		}
	}
	void TeclaD(){
	//D
	if(Input.GetKey(KeyCode.D) && WD == false && WA == false && AS == false && SD == false)
		{
			Apuntar();
			rot = new Vector3(0,0,0);
			transform.eulerAngles = rot;
			pose = new Vector3(10,0,0);
			target.transform.position = pose;
			WA = false;
			WD = false;
			AS = false;
			SD = false;
		}
	}
	void Apuntar() {

	Vector3 direc = target.position - transform.position;
	Quaternion rotacion = Quaternion.LookRotation(direc);
	transform.rotation = Quaternion.Lerp(transform.rotation,rotacion,vel * Time.deltaTime);
	}
}