using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteContenedor : MonoBehaviour {
	public Sprite[] pPiernas, pDisarmedWalk,pGolpeMano,pMac10Caminar,pMac10Atacar,pCuchilloComunCaminar,pCuchilloComunAtacar;

	public Sprite[] getJugadorPiernas(){
		return pPiernas;
	}
	public Sprite[] getJugadorDisarmedWalk(){
		return pDisarmedWalk;
	}
	public Sprite[] getJugadorGolpeMano(){
		return pGolpeMano;
	}
	public Sprite[] GetArma(string arma){
		switch( arma ){
			default:
				return pGolpeMano;
				break;
			case "Mac10":
				return pMac10Atacar;
				break;
	
			case "CuchilloComun":
				return pCuchilloComunAtacar;
				break;
		}
	}
	
	public Sprite[]	GetArmaCaminar(string arma){
		switch( arma ){
			default:
				return pDisarmedWalk;
				break;
			case "Mac10":
				return pMac10Caminar;
				break;
	
			case "CuchilloComun":
				return pCuchilloComunCaminar;
				break;
		}
	}
}
