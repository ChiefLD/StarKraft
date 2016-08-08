using UnityEngine;
using System.Collections;

public class Eldar_Powers : MonoBehaviour {
	[HideInInspector]
	public bool alive;

	PlayerHealth eldar_health;

	public float max_mp = 100.0f;
	public float mp;

	void Awake (){
		eldar_health = GetComponent<PlayerHealth> () as PlayerHealth;
	}
	void Start () {
		mp = max_mp;
	}
	
	// Update is called once per frame
	void Update () {
		if (mp < max_mp){
			mp += 0.1f;
		}
		if (mp < 0) {
			mp = 0;
		} else if (mp > 100) {
			mp = 100;
		}
		if (!alive && Input.GetButtonDown ("Jump")){
			if (mp >= 100){
				eldar_health.Respawn ();
				mp = 0;
			}
		}
	}

	public void PowerLoss(){
		mp -= 50.0f;
	}
}
