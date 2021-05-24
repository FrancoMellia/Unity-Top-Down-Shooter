using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorAnimacion : MonoBehaviour {

	Sprite [] caminando,atacando,spritePiernas;
	int contador = 0,contadorPiernas = 0;
	Movimiento mov;
	float timer = 0.03f,piernasTimer = 0.05f;
	public SpriteRenderer torso,piernas;
	SpriteContenedor sc;

	bool atacandoB = false;

	// Use this for initialization
	void Start () {
		mov = this.GetComponent<Movimiento>();
		sc = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpriteContenedor>();
		caminando = sc.getJugadorDisarmedWalk();
		atacando = sc.getJugadorGolpeMano();
		spritePiernas = sc.getJugadorPiernas();
	}
	
	// Update is called once per frame
	void Update () {
		AnimPiernas();
		if(atacandoB == false){
		AnimTorso();
		}
		else AnimAtacar();
	}

	void AnimAtacar(){
		torso.sprite = atacando[contador];

		timer -= Time.deltaTime;
		if(timer <= 0){
				if(contador < atacando.Length -1){
					contador++;
				} else { 
				if(atacandoB){
					atacandoB = false;
				}
				contador = 0;
				}
			timer = 0.05f;
		}
	}

	void AnimTorso(){

		if(mov.moviendo == true){
			torso.sprite = caminando[contador];
			timer -= Time.deltaTime;
			if(timer <= 0){
		/**/		if( contador < caminando.Length - 1){
					contador++;
				}
				else {contador = 0;}
				timer = 0.1f;
			}
		}
	}
  	void AnimPiernas(){
		if(mov.moviendo == true){
			piernas.sprite = spritePiernas[contadorPiernas];
			piernasTimer -= Time.deltaTime;
		
			if(piernasTimer <= 0){
		/**/		if(contadorPiernas < spritePiernas.Length - 1){
					contadorPiernas++;
				}
				else contadorPiernas = 0;
				piernasTimer = 0.05f;
			}
		}
	else piernas.sprite = spritePiernas[0];
	}
    void CheckAtaque(){
        if(atacandoB == false){
            torso.sprite = caminando[0];
        }
    }

	public void Atacar(){
		atacandoB = true;
	}
	public void ResetContador(){
		contador = 0;
	}
    public void ResetSprites() {
        contador = 0;
        caminando = sc.getJugadorDisarmedWalk();
        atacando = sc.getJugadorGolpeMano();
        torso.sprite = caminando[0];
    }
	public bool GetAtaque(){
		return atacandoB;
	}
	public void SetNuevoTorso(Sprite [] caminar,Sprite [] atacar){
		contador = 0;
		atacando = atacar;
		caminando = caminar;
	}
}
