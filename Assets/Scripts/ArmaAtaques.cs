using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaAtaques : MonoBehaviour {
    public GameObject oneManoSpawn, twoManoSpawn, Enemigo;
	public GameObject Bala;
    public GameObject CurArma;
    public GameObject torso;
    public LayerMask layerMask;
    bool arma = false;
	float timer = 0.1f, timerReset= 0.1f;
	JugadorAnimacion ja;
	SpriteContenedor sc;

	float armaGet = 0.5f;
	bool gettingArma = false;
    bool unaMano = false;
    // Use this for initialization
    void Start() {
        layerMask = 8;
        ja = this.GetComponent<JugadorAnimacion>();
		sc = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpriteContenedor>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0)){
			Atacar();
		}
		if(Input.GetMouseButtonDown(0)){
			ja.ResetContador();
		}
		if(Input.GetMouseButtonUp(0)){
			ja.ResetContador();
		}
		if(Input.GetMouseButtonDown(1) && gettingArma == false){
			SoltarArma();
		}
		if(gettingArma == true){
			armaGet -= Time.deltaTime;
			if(armaGet <= 0){
				gettingArma = false;
			}
		}
	}

	public void SetArma(GameObject cur, string nombreA,float rangoFuego,bool arma,bool unaMano){
		gettingArma = true;
		CurArma = cur;
		ja.SetNuevoTorso(sc.GetArmaCaminar(nombreA),sc.GetArma(nombreA));
		this.arma = arma;
		timerReset = rangoFuego;
		timer = timerReset;
	}
    public void Atacar() {
        ja.Atacar();
        if (arma == true) {
            ja.Atacar();
            Bala bl = Bala.GetComponent<Bala>();
            Vector3 dir;
            dir.x = torso.transform.right.x;
            dir.y = torso.transform.right.y;
            dir.z = 0;
            bl.setVals(dir, "Jugador");
            if (unaMano == true)
            {
                Instantiate(Bala, oneManoSpawn.transform.position, this.transform.rotation);
            }
            else {
                Instantiate(Bala, twoManoSpawn.transform.position, this.transform.rotation);
            }
            timer = timerReset;
            if (Input.GetMouseButtonUp(0)) {
                ja.ResetContador();
            }
        }
        /*else
            //cuchillo ataque
            ja.Atacar();*/
      /*  Physics2D.queriesHitTriggers = true;
       RaycastHit2D ray = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(torso.transform.right.x, torso.transform.right.y),30);
        Debug.DrawRay(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(torso.transform.right.x, torso.transform.right.y), Color.green);

        if (CurArma == null && ray.collider.gameObject.CompareTag("Enemigo"))
            {
                Debug.Log("Se Ejecuta");
                EnemigoAtacado ea = GameObject.FindGameObjectWithTag("Enemigo").GetComponent<EnemigoAtacado>();
                ea.NoqueadoEnemigo();
            }
            else if (ray.collider.gameObject != null)
            {
                if (CurArma != null && ray.collider.gameObject.CompareTag("Enemigo"))
                {
                    EnemigoAtacado ea = ray.collider.gameObject.GetComponent<EnemigoAtacado>();
                    ea.KillFaka();
                }
            }*/
        
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) {
            ja.Atacar();
            if ((CurArma == null && col.gameObject.tag == "Enemigo"))
            {
                EnemigoAtacado ea = GameObject.FindGameObjectWithTag("Enemigo").GetComponent<EnemigoAtacado>();
                ea.NoqueadoEnemigo();
            }
            else if ((CurArma != null && col.gameObject.tag == "Enemigo")) {
                EnemigoAtacado ea = GameObject.FindGameObjectWithTag("Enemigo").GetComponent<EnemigoAtacado>();
                ea.KillFaka();
            } } 
    }


    public GameObject GetCur(){
		return CurArma;
	}
	public void SoltarArma(){
        if (CurArma == null) { }
        else {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            CurArma.AddComponent<TirarArma>();
            Vector3 dir;
            dir.x = mousePos.x - this.transform.position.x;
            dir.y = mousePos.y - this.transform.position.y;
            dir.z = 0;
            CurArma.GetComponent<Rigidbody2D>().isKinematic = false;
            CurArma.GetComponent<TirarArma>().setDirection(dir);
            CurArma.transform.position = oneManoSpawn.transform.position;
            CurArma.transform.eulerAngles = this.transform.eulerAngles;
            CurArma.SetActive(true);
            SetArma(null, "", 0.5f, false, false);
            ja.ResetSprites();
        }
	}
}
