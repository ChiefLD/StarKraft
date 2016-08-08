using UnityEngine;
using System.Collections;

public class DisableScript : MonoBehaviour {
	public GameObject[] disable_on_death;

	public void Disable(){
		foreach (GameObject d in disable_on_death) {
			d.SetActive (false);
		}
	}
	public void Enable(){
		foreach (GameObject d in disable_on_death) {
			d.SetActive (true);
		}
	}
}
