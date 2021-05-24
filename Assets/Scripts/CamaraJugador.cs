using UnityEngine;

public class CamaraJugador : MonoBehaviour {
	public GameObject jugador;
	Movimiento mov;
	public bool seguirJugador = true;
	Vector3 mousePos;
	Camera cam;
	void start(){
		jugador = GameObject.FindGameObjectWithTag("Jugador");
		mov = jugador.GetComponent<Movimiento>();
		cam = Camera.main;

	}
	void Update(){

		if(Input.GetKey(KeyCode.LeftShift)){
			seguirJugador = false;
			mov.setMoving(false);
		}
		else seguirJugador = true;

		if(seguirJugador == true){
			CamSeguirJugador();
		}
		else LookAhead ();
	}
	public void SetSeguirJugador(bool val){

		seguirJugador = val;

	}

	void CamSeguirJugador(){
		Vector3 newPos = new Vector3(jugador.transform.position.x,jugador.transform.position.y,this.transform.position.z);
		this.transform.position = newPos;
	}
	void LookAhead(){
		Vector3 camPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y));
		camPos.z = -10;
		Vector3 dir = camPos-this.transform.position;
		if(jugador.GetComponent<SpriteRenderer>().isVisible == true){
			transform.Translate(dir*2*Time.deltaTime);
		}
	}
}


	/*public GameObject jugador;
	
	void FixedUpdate(){
	float posX = jugador.transform.position.x;
	float posY = jugador.transform.position.y;

	transform.position = new Vector3(posX,posY,transform.position.z);
	}*/