using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {

	[SerializeField]
	Behaviour[] components_to_disable;

	[SerializeField]
	string remote_layer_name = "RemotePlayer";

	Camera scene_camera;

	void Start (){
		if (!isLocalPlayer) {
			DisableComponents ();
			AssignRemoteLayer ();
		} else {
			scene_camera = Camera.main;
			if (scene_camera != null) {
				scene_camera.gameObject.SetActive (false);
			}
		}

        GetComponent<Player>().Setup();
	}

	public override void OnStartClient (){
		base.OnStartClient ();
		string _netID = GetComponent<NetworkIdentity> ().netId.ToString();
		Player _player = GetComponent<Player> ();
		GM.RegisterPlayer (_netID, _player);
	}

	void AssignRemoteLayer(){
		gameObject.layer = LayerMask.NameToLayer (remote_layer_name);
	}

	void DisableComponents ()
	{
		for (int i = 0; i < components_to_disable.Length; i++)
		{
			components_to_disable[i].enabled = false;
		}
	}

	void OnDisable (){
		if (scene_camera != null){
			scene_camera.gameObject.SetActive (true);
		}

		GM.UnRegisterPlayer (transform.name);
	}

}
