using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class LayTrap : NetworkBehaviour
{
    public float reloadTime = 5;
    private float lastActivationTime;
    public GameObject trap;

    private void Start() {
        if(trap == null) {
            print("Le prefab du piege n'est défini");
        }
    }

    void Update() {
        if (Input.GetAxis("Fire1") > 0 && isLocalPlayer) {
            FireTrap();
        }
	}


    private void FireTrap() {
        if (CanFireTrap()) {
            CmdFire(gameObject);
            lastActivationTime = Time.time;
        }
    }

    private bool CanFireTrap() {
        if ((Time.time - lastActivationTime)< reloadTime) {
            return false;
        }
        return true;
    }

    [Command]
    private void CmdFire(GameObject owner)
    {
        GameObject trapObject = Instantiate(trap, this.transform.position, this.transform.rotation);

        NetworkServer.Spawn(trapObject);
        trapObject.GetComponent<Trap>().RpcSetOwner(owner);
    }

}
