using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirarArma : MonoBehaviour {
    EnemigoAtacado atacado;
    public float timer = 2.5f;
    Vector3 direction;
    Rigidbody2D rid;
    GameObject jugador;

	// Use this for initialization
	void Start () {
        jugador = GameObject.FindGameObjectWithTag("Jugador");
        rid = this.GetComponent<Rigidbody2D>();
        rid.AddForce(direction * 200);
	}
    void FixedUpdate()
    {       
        transform.rotation = Quaternion.Slerp(this.transform.rotation, new Quaternion(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z - (-2), this.transform.rotation.w), Time.deltaTime * timer);
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            rid.isKinematic = false;
            Destroy(this);
        }
    }
    public void setDirection(Vector3 dir) {
        direction = dir;
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemigo") {
            atacado = col.gameObject.GetComponent<EnemigoAtacado>();
            atacado.NoqueadoEnemigo();
            rid.isKinematic = false;
            //Destroy(this);
        }
        if (col.gameObject.tag == "Noqueado")
        {
            rid.isKinematic = false;
            //Destroy(this);
        }
        else if (col.gameObject.tag=="Jugador") {

        }
        else{
            rid.isKinematic = true;
            //Destroy(this);
        }
    }
}
