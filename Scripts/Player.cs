using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour {

    [SyncVar]
    private bool _isDead = false;
    public bool isDead {
        get { return _isDead; }
        protected set { _isDead = value; }
    }

	[SerializeField]
	private int max_health = 100;

	[SyncVar]
	private int current_health;

    [SerializeField]
    private Behaviour[] disable_on_death;
    private bool[] was_enabled;

	public void Setup (){
        was_enabled = new bool[disable_on_death.Length];
        for (int i = 0; i < was_enabled.Length; i++)
        {
            was_enabled[i] = disable_on_death[i].enabled;
        }

		SetDefaults ();
	}

    /*
    void Update() {
        if (!isLocalPlayer)
            return;
        if (Input.GetKeyDown(KeyCode.K))
        { 
            RpcTakeDamage(99999);
        }
    }
    */

    [ClientRpc]
	public void RpcTakeDamage (int _amount){
        if (isDead)
        {
            return;
        }

		current_health -= _amount;

		Debug.Log (transform.name + " now has " + current_health + "health.");

        if (current_health <= 0)
        {
            Die();
        }
	}

    private void Die()
    {
        isDead = true;

        for (int i = 0; i < disable_on_death.Length; i++) 
        {
            disable_on_death[i].enabled = false;
        }

        Collider _col = GetComponent<Collider>();
        if (_col != null)
            _col.enabled = false; 

        //Dissable components, no move, no colliders
        Debug.Log(transform.name + "is dead");

        //call respawn method
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GM.instance.match_setting.respawn_time);

        SetDefaults();
        Transform _spawn_point = NetworkManager.singleton.GetStartPosition();
        transform.position = _spawn_point.position;
        transform.rotation = _spawn_point.rotation;

        Debug.Log(transform.name + " has respawned.");
    }

    public void SetDefaults()
    {
        isDead = false;

        current_health = max_health;

        for (int i = 0; i < disable_on_death.Length; i++)
        {
            disable_on_death[i].enabled = was_enabled[i];
        }

        Collider _col = GetComponent<Collider>();
        if (_col != null)
            _col.enabled = true;
    }
}
