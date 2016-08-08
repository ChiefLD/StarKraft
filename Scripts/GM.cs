using UnityEngine;
using System.Collections.Generic;

public class GM : MonoBehaviour {

    public static GM instance;

    public MatchSettings match_setting;

    void Awake() {
        if (instance != null)
        {
            Debug.Log("More than one GM in scene.");
        }else
        instance = this;
    }

    #region Player Registering

    private const string PLAYER_ID_PREFIX = "Player";

	private static Dictionary<string, Player> players = new Dictionary<string, Player> ();

	public static void RegisterPlayer (string _netID, Player _player){
		string _playerID = PLAYER_ID_PREFIX + _netID;
		players.Add (_playerID, _player); 
		_player.transform.name = _playerID;
	}

	public static void UnRegisterPlayer (string _playerID){
		players.Remove (_playerID);
	}

	public static Player GetPlayer (string _playerID){ 
		return players[_playerID]; 
	}
    /*
        void OnGUI (){
            GUILayout.BeginArea (new Rect (200, 200, 200, 500));
            GUILayout.BeginVertical ();

            foreach (string player_id in players.Keys){
                GUILayout.Label (player_id + " - " + players[player_id].transform.name);
            }

            GUILayout.EndVertical ();
            GUILayout.EndArea ();
        }
    */

    #endregion
}
