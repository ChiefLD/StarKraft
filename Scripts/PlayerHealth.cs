using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public bool human;
	public bool alien;
	public bool creature;

	DisableScript disable_script;
	Eldar_Powers eldar_powers;

	public int max_hp = 100;
	public int hp;

	public bool alive;

	void Awake (){
		if (alien){
			eldar_powers = GetComponent<Eldar_Powers> () as Eldar_Powers;
			disable_script = GetComponent <DisableScript> () as DisableScript;
		}
		else if (human){
			eldar_powers = null;
			disable_script = null;
		}
	}

	void Start (){
		Respawn ();
	}
	public void Respawn () {
		hp = max_hp;
		alive = true;

		if (alien){
			eldar_powers.alive = true;
			disable_script.Enable ();	
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.L)){
			TakeDamage (100);
		}
	}

	void OnCollisionEnter (Collision other){
		if (alien){
			if (other.gameObject.tag == "TerranAttack"){
				TakeDamage (10);
			}
		}
	}

	public void TakeDamage (int dmg){
		hp -= dmg;

		if (hp <= 0) {
			alive = false;

			if (alien) {
				eldar_powers.PowerLoss ();
				eldar_powers.alive = false;
				disable_script.Disable ();
			}
		}
	}
}
