using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionGenerica : MonoBehaviour {
	public Sprite[] sprites;
	public bool loop,destruirAlFinal,MientrasEstaCerca,needActivate;
	bool activado = false;
	SpriteRenderer sr;
	float timer = 0.05f;
	public float distancia = 5.0f,timerReset = 0.05f;
	int contador = 0;
	GameObject Jugador;
	// Use this for initialization
	void Start () {
		Jugador = GameObject.FindGameObjectWithTag("Jugador");
		sr = Jugador.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(MientrasEstaCerca){
			AnimMientrasEstaCerca();
		}
		else if (needActivate && activado == false){
			if(Input.GetKeyDown(KeyCode.E) && Vector3.Distance(Jugador.transform.position,this.transform.position)< 2.0f ){
				activado = true;
			}	
		}
		else if (needActivate && activado){
			ActivacionNormal();
			if(Input.GetKeyDown(KeyCode.E) && Vector3.Distance(Jugador.transform.position,this.transform.position)< 2.0f ){
				activado = false;
			}	
		}
		else ActivacionNormal();
	}

	void AnimMientrasEstaCerca(){

		sr.sprite = sprites[contador];
		if(Vector3.Distance(Jugador.transform.position,this.transform.position)< distancia ){
			timer -= Time.deltaTime;
			if(timer <= 0){
				if(contador < sprites.Length -1){
					contador++;
				}
				else {
					if(loop){
						contador = 0;
					} else if (destruirAlFinal){
						Destroy(this.gameObject);
					}
				}
				timer = timerReset;
			}
		}
	}

	void ActivacionNormal(){

		sr.sprite = sprites[contador];
		if(Vector3.Distance(Jugador.transform.position,this.transform.position)< distancia ){
			timer -= Time.deltaTime;
			if(timer <= 0){
				if(contador < sprites.Length -1){
					contador++;
				}
				else {
					if(loop){
						contador = 0;
					} else if (destruirAlFinal){
						Destroy (this.gameObject);
					}
				}
				timer = timerReset;
			}
		}

	}
}
