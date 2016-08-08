using UnityEngine;
using System.Collections;

public class Destroy_Script : MonoBehaviour {
	public bool kill_bullet = false;
	
	// Update is called once per frame
	void Update () {
		Destroy (this.gameObject, 5.0f);
		if (kill_bullet){
			Destroy (this.gameObject, 0.25f);
		}
	}

	void OnCollisionEnter (Collision other){
		kill_bullet = true;
	}
}
