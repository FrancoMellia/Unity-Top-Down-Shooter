using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour {
    public Vector3 direction;
    string creator;
    EnemigoAtacado atacado;
    public GameObject sangreImpacto, paredImpacto;
    // Use this for initialization
    GameObject pared;
    GameObject sangre;
    public float timer = 10.0f;

	void Start () {
        pared = GameObject.FindGameObjectWithTag("ParedImpacto");
        sangre = GameObject.FindGameObjectWithTag("SangredImpacto");
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(direction * 67 * Time.deltaTime);
        timer -= Time.deltaTime;
        if (timer <= 0) {
            Destroy(pared.gameObject);
            Destroy(this.gameObject);
        }
	}
    public void setVals(Vector3 dir, string nombre) {
        direction = dir;
        creator = nombre;
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemigo")
        {
            atacado = col.gameObject.GetComponent<EnemigoAtacado>();
                    atacado.KillBullet();
            Instantiate(sangre, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
        else if (col.gameObject.tag == "Jugador") { }
        else {
            Instantiate(pared, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
                }
    }
