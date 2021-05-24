using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoAtacado : MonoBehaviour {
    public Sprite srnoqueado, apuñalada, heridaBala, backUp;
    public GameObject piscinaSangre, chorroSangre;
    SpriteRenderer sr;
    bool EnemigoNoqueado = false;
    bool BoleanoKillBullet = false;
    bool BoleanoKillFaka = false;
    float TiempoNoqueo = 3.0f;
    GameObject jugador;
    // Use this for initialization
    void Start() {
        sr = this.GetComponent<SpriteRenderer>();
        jugador = GameObject.FindGameObjectWithTag("Jugador");

	}
	
	// Update is called once per frame
	void Update () {
        if (EnemigoNoqueado == true) {
            noqueado();
        }
        if (BoleanoKillBullet == true) { KillBala(); }
        if (BoleanoKillFaka == true) { KillMelee(); }
    }
    public void NoqueadoEnemigo() {
        EnemigoNoqueado = true;
    }
    public void KillBullet() {
        BoleanoKillBullet = true;
    }
    public void KillFaka()
    {
        BoleanoKillFaka = true;
    }
    void noqueado() {
        TiempoNoqueo -= Time.deltaTime;
        sr.sprite = srnoqueado;
        this.GetComponent<BoxCollider2D>().enabled = false;
        sr.sortingOrder = 2;
        this.gameObject.tag = "Noqueado";

        if (TiempoNoqueo<=0) {
            this.gameObject.tag = "Enemigo";
            EnemigoNoqueado = false;
            sr.sprite = backUp;
            this.GetComponent<BoxCollider2D>().enabled = true;
            sr.sortingOrder = 5;
            TiempoNoqueo = 3.0f;
        }
        //disable ia
    }

    void KillBala() {
        sr.sprite = heridaBala;
        Instantiate(piscinaSangre, this.transform.position, this.transform.rotation);
        sr.sortingOrder = 2;
        //disable ia
        this.GetComponent<BoxCollider2D>().enabled = false;
        this.gameObject.tag = "Muerto";
    }
    void KillMelee()
    {
        sr.sprite = apuñalada;
        //Instantiate(piscinaSangre, this.transform.position, this.transform.rotation);
        //Instantiate(chorroSangre, this.transform.position, this.transform.rotation);
        sr.sortingOrder = 2;
        sr.renderingLayerMask = 1;
        //disable ia
        this.GetComponent<BoxCollider2D>().enabled = false;
        this.gameObject.tag = "Muerto";
    }

    }
