using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*
public class Enemigo : MonoBehaviour
{
    #region CAMPOS

    #region ESTADOS DEL ENEMIGO

    private enum EstadoEnemigo
    {
        Guardia,
        Buscando,
        Cubriendose,
        Atacando
    };

    [SerializeField]
    private EstadoEnemigo estadoActual;

    [SerializeField]
    private bool enAlerta;

    [SerializeField]
    private Sprite iconoGuardia;
    [SerializeField]
    private Sprite iconoBuscando;
    [SerializeField]
    private Sprite iconoCubriendose;
    [SerializeField]
    private Sprite iconoAtacando;

    [SerializeField]
    private SpriteRenderer icono;

    private Vector3 posicionObjetivo;
    [SerializeField]
    private GameObject[] puntosDeCobertura;

    [SerializeField]
    private GameObject puntoDeCoberturaMasCercano;

    #endregion

    #region SALUD

    //Salud 
    [SerializeField]
    private int puntosVidaMaxima;
    [SerializeField]
    private int puntosVida;

    #endregion

    #region MUERTE
    //Muerte

    [SerializeField]
    private GameObject objetoMuerte;
    [SerializeField]
    private bool muerte;

    #endregion

    #region VISIÓN
    // Para la Visión del enemigo considero una circunferencia con un determinado radio
    // 

    [SerializeField]
    private float radioVisionNormal;
    [SerializeField]
    private float radioVisionAlerta;
    [SerializeField]
    private float anguloVision;

    private float radioVision;

    private Vector3 puntoConoIzquierda;
    private Vector3 puntoConoDerecha;
    private Vector3 puntoConoFrente;


    #endregion

    #region ARMA
    //Arma

    private Arma arma;
    private bool habilitarDisparo = true;

    #endregion

    #region MOVIMIENTO
    //Movimiento

    private NavMeshAgent agent;
    private bool aCubierto = false;
    private Vector3 posicionInicial;
    private bool enPosicion = false;

    #endregion

    #region EXTRAS
    //Extras

    private GameObject player;

    private Vector3 yOffSet = new Vector3(0, 1, 0);

    #endregion

    #endregion

    #region MÉTODOS

    #region INICIALIZACIÓN

    void Start()
    {
        //Encontramos todos los objetos necesarios
        puntosVida = puntosVidaMaxima;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        arma = GetComponent<Arma>();
        posicionInicial = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        puntosDeCobertura = GameObject.FindGameObjectsWithTag("PuntoCobertura");
        posicionObjetivo = posicionInicial;
    }

    #endregion

    #region MÉTODOS UPDATE

    private void FixedUpdate()
    {
        switch (estadoActual)
        {
            case EstadoEnemigo.Guardia:
                GuardiaFixedUpdate();
                break;
            case EstadoEnemigo.Buscando:
                BuscandoFixedUpdate();
                break;
            case EstadoEnemigo.Cubriendose:
                CubriendoseFixedUpdate();
                break;
            case EstadoEnemigo.Atacando:
                AtacandoFixedUpdate();
                break;
        }
    }

    private void Update()
    {

        DibujarZonaVision();

        switch (estadoActual)
        {
            case EstadoEnemigo.Guardia:
                icono.sprite = iconoGuardia;
                GuardiaUpdate();
                break;
            case EstadoEnemigo.Buscando:
                icono.sprite = iconoBuscando;
                BuscandoUpdate();
                break;
            case EstadoEnemigo.Cubriendose:
                icono.sprite = iconoCubriendose;
                CubriendoseUpdate();
                break;
            case EstadoEnemigo.Atacando:
                icono.sprite = iconoAtacando;
                AtacandoUpdate();
                break;
        }

        if (enAlerta)
        {
            radioVision = radioVisionAlerta;
        }
        else
        {
            radioVision = radioVisionNormal;
        }

        if (muerte)
        {
            Muerte();
        }
    }

    #endregion

    #region MÉTODOS SALUD

    public void RecibeDisparo()
    {
        //Me han dado!
        float distanceWithPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
        if ( distanceWithPlayer< 10f) {

            if (distanceWithPlayer < 2f)
            {
                puntosVida -= 5;
            }
            else
            {
                puntosVida -= 2;
            }

        }
        else
        {
            puntosVida--;

        }
        if (puntosVida <= 0)
        {
            //Adiós mundo cruel
            Muerte();
        }
        enAlerta = true;
        
        if (estadoActual != EstadoEnemigo.Atacando)
        {
            //No sé de dónde vienen los disparos! retirada!!
            BuscarCoberturaMasCercana();
            estadoActual = EstadoEnemigo.Cubriendose;
        }


    }

    private void Muerte()
    {
        Instantiate(objetoMuerte, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    #endregion

    #region ESTADO DE GUARDIA 
    //En esta región se colocan todos los métodos relacionados al estado de guardia del enemigo


    private void GuardiaFixedUpdate()
    {

    }

    private void GuardiaUpdate()
    {
       
        if (EnemigoEnRangoDeVision())
        {
            //Estoy viendo la amenaza!
            enAlerta = true;
            posicionObjetivo = player.transform.position;
            estadoActual = EstadoEnemigo.Atacando;
        }


        

    }

    private void Volver()
    {
        enPosicion = false;
        estadoActual = EstadoEnemigo.Guardia;
        posicionObjetivo = posicionInicial;
        agent.SetDestination(posicionObjetivo);
    }

    #endregion

    #region ESTADO BUSCANDO
    //En esta región se colocan todos los métodos relacionados al estado buscando del enemigo

    private void BuscandoFixedUpdate()
    {

    }

    private void BuscandoUpdate()
    {
        

        if (arma.HayBalas())
        {
            //Mi arma tiene balas
            
            if (Vector3.Distance(gameObject.transform.position,posicionObjetivo)<2.5f)
            {
                //Llegué a la última posición del jugador pero no está :/
               
                if (!enPosicion)
                {
                    enPosicion = true;
                    Invoke("Volver", 5f);
                }

            }
            else
            {
                agent.SetDestination(posicionObjetivo);//Voy a donde lo vi por última vez
                enPosicion = false;
            }
            if (EnemigoEnRangoDeVision())
            {
                //Estoy viendo la amenaza
                enAlerta = true;
                estadoActual = EstadoEnemigo.Atacando;
            }
        }
        else
        {
            //No tengo balas
            arma.Recargar();
        }
    }

    #endregion

    #region ESTADO CUBRIÉNDOSE
    //En esta región se colocan todos los métodos relacionados al estado cubriéndose del enemigo

    private void CubriendoseFixedUpdate()
    {

    }

    private void CubriendoseUpdate()
    {
        if (!aCubierto)
        {
            //No estoy a cubierto
            agent.SetDestination(puntoDeCoberturaMasCercano.transform.position);
            if(Vector3.Distance(gameObject.transform.position, puntoDeCoberturaMasCercano.transform.position) < 2.5f)
            {
                aCubierto = true;

            }
        }else
        {
            //Estoy a salvo
            estadoActual = EstadoEnemigo.Buscando;
        }

    }

    public void BuscarCoberturaMasCercana()
    {
        puntoDeCoberturaMasCercano = puntosDeCobertura[0];
        
        for(int i = 1; i < puntosDeCobertura.Length; i++)
        {
            float distanciaPuntoElegido = Vector3.Distance(gameObject.transform.position, puntoDeCoberturaMasCercano.transform.position);

            float distanciaPuntoActual = Vector3.Distance(gameObject.transform.position, puntosDeCobertura[i].transform.position);

            if (distanciaPuntoActual < distanciaPuntoElegido)
            {
                puntoDeCoberturaMasCercano = puntosDeCobertura[i];
            }
        }
       
        
    }

    #endregion

    #region ESTADO ATACANDO 

    //En esta región se colocan todos los métodos relacionados al estado atacando del enemigo


    private void AtacandoFixedUpdate()
    {

    }

    private void AtacandoUpdate()
    {
        //Cambié el orden de ejecución de los métodos a un orden que consideré más apropiado
        //La pregunta de si debería disparar ahora se hace si el enemigo está en rango de visión

        if (EnemigoEnRangoDeVision())
        {
            gameObject.transform.LookAt(player.transform);
            posicionObjetivo = player.transform.position;
            //Estoy viendo al enemigo
            if (DeberiaPerseguir())
            {
                //Tengo que perseguirlo
                agent.SetDestination(posicionObjetivo);

            }
            else
            {
                //Mejor no lo persigo
                agent.SetDestination(gameObject.transform.position);

            }
            if (DeberiaDisparar())
            {
                //Debo disparar
                if (arma.Disparar(gameObject.transform.forward))
                {
                    //El arma se disparó
                    habilitarDisparo = false;
                   
                    Invoke("HabilitarDisparo", Random.value * 3);
                }
                else
                {
                    //Shit! No tengo balas!!
                    BuscarCoberturaMasCercana();
                    estadoActual = EstadoEnemigo.Cubriendose;
                    aCubierto = false;
                    agent.SetDestination(puntoDeCoberturaMasCercano.transform.position);
                    return;
                }
            }
        }
        else
        {
            //No estoy viendo al enemigo
            estadoActual = EstadoEnemigo.Buscando;
        }
        
        
        
    }

    private bool DeberiaPerseguir()
    {
        bool res = false;
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) > 5)
        {
            res = true;

        }

        return res;
    }

    private bool DeberiaDisparar()
    {
        bool res = false;

        if (habilitarDisparo && (Random.value > 0.1f))
        {
            res = true;
        }

        return res;
    }

    private void HabilitarDisparo()
    {
        habilitarDisparo = true;
    }

    #endregion

    #region MÉTODOS AUXILIARES

    private bool EnemigoEnRangoDeVision()
    {
        bool res = false;

        if (ChequearSiPersonajeEnCirculoDeVision())
        {
            if (ChequearSiPersonajeEnConoDeVision())
            {
                if (ChequearSiHayObstaculos())
                {

                    res = true;
                }
            }
        }

        return res;
    }

    private bool ChequearSiPersonajeEnCirculoDeVision()
    {
        return Vector3.Distance(gameObject.transform.position, player.transform.position) < radioVision;
    }

    private bool ChequearSiPersonajeEnConoDeVision()
    {

        float anguloEntreVectores;

        Vector3 vectorEnemigoPersonaje = player.transform.position - gameObject.transform.position;
        vectorEnemigoPersonaje.y = 0;   

        Vector3 vectorEnemigoFrente= puntoConoFrente - gameObject.transform.position;
        vectorEnemigoFrente.y = 0;
        
        anguloEntreVectores = Mathf.Rad2Deg * AnguloEntreDosVectores(vectorEnemigoFrente, vectorEnemigoPersonaje);
            
        

        bool res=false;

        if (anguloEntreVectores < anguloVision / 2)
        {
            res = true;
        }

        return res;
    }

    private bool ChequearSiHayObstaculos()
    {
        //Tiramos un rayo desde el enemigo hasta el personaje y vemos con qué chocamos
        bool res = false;
        Vector3 origenRayo = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+1, gameObject.transform.position.z);
        
        //Agrego este pequeño desplazamiento para evitar que el rayo colisione con el propio enemigo
        origenRayo += gameObject.transform.forward;
        //

        Vector3 direccionRayo = new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z);
        direccionRayo -= origenRayo;
        Ray ray = new Ray(origenRayo,direccionRayo);
        Debug.DrawRay(origenRayo, direccionRayo);

        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && hit.collider.tag.Equals("Player"))
            {
               
                
                res = true;
            }
          
        }
        return res;
    }

    private float AnguloEntreDosVectores(Vector3 v1, Vector3 v2)
    {
        float productoEscalar = Vector3.Dot(v1, v2);
        float normas = Vector3.Magnitude(v1) * Vector3.Magnitude(v2);
        return Mathf.Acos(productoEscalar / normas);
    }


    private void DibujarZonaVision()
    {
       //Dirección frontal

        Vector3 frente = gameObject.transform.position+ gameObject.transform.forward * radioVision;

        puntoConoFrente = frente + yOffSet;

        Debug.DrawLine(gameObject.transform.position+yOffSet, puntoConoFrente, Color.red);
   
        //Círculo

        DibujarCirculoDeVision();

        //Cono de vision

        DibujarConoDeVision(frente);
   
    }

    private void DibujarCirculoDeVision()
    {

        int divisionesCirculo = 30;
        float angleStep = 360f / (divisionesCirculo);
        for (int i = 0; i < divisionesCirculo; i++)
        {

            float x, z;
            float x2, z2;

            x = radioVision * Mathf.Sin(Mathf.Deg2Rad * angleStep * i);
            z = radioVision * Mathf.Cos(Mathf.Deg2Rad * angleStep * i);

            x2 = radioVision * Mathf.Sin(Mathf.Deg2Rad * angleStep * (i + 1));
            z2 = radioVision * Mathf.Cos(Mathf.Deg2Rad * angleStep * (i + 1));

            Vector3 inicio = new Vector3(x, 1, z);
            Vector3 fin = new Vector3(x2, 1, z2);
    
            Color c = Color.white;
            if (Vector3.Distance(gameObject.transform.position, player.transform.position) < radioVision)
            {
                c = Color.red;
            }


            Debug.DrawLine(gameObject.transform.position + inicio, gameObject.transform.position + fin, c);

        }
    }

    private void DibujarConoDeVision(Vector3 frente)
    {

        float angulo1 = -Mathf.Deg2Rad * anguloVision / 2;
        float angulo2 = Mathf.Deg2Rad * anguloVision / 2;
        Vector3 frente2 = frente - gameObject.transform.position;
        Vector3 limiteIzquierdo = new Vector3(frente2.x * Mathf.Cos(angulo1) + frente2.z * Mathf.Sin(angulo1), 1f, -frente2.x * Mathf.Sin(angulo1) + frente2.z * Mathf.Cos(angulo1));
        Vector3 limiteDerecho = new Vector3(frente2.x * Mathf.Cos(angulo2) + frente2.z * Mathf.Sin(angulo2), 1f, -frente2.x * Mathf.Sin(angulo2) + frente2.z * Mathf.Cos(angulo2));

        puntoConoIzquierda = gameObject.transform.position + limiteIzquierdo;
        puntoConoDerecha = gameObject.transform.position + limiteDerecho;

        Debug.DrawLine(gameObject.transform.position + yOffSet, puntoConoIzquierda , Color.blue);
        Debug.DrawLine(gameObject.transform.position + yOffSet, puntoConoDerecha, Color.blue);


    }


    public void DisparosEnLasCercanias(Vector3 pos)
    {
        if (estadoActual != EstadoEnemigo.Atacando)
        {
            enAlerta = true;    
            posicionObjetivo = pos;
            estadoActual = EstadoEnemigo.Buscando;
        }

    } 

    #endregion

    #endregion

}*/
