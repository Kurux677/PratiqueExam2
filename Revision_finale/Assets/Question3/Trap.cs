using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Trap : NetworkBehaviour
{
    private GameObject owner;
    public Renderer rend;

    [ClientRpc]
    public void RpcSetOwner(GameObject newOwner)
    {
        owner = newOwner;
        print(owner.name);
        if (owner.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            rend = GetComponent<Renderer>();
            rend.enabled = true;
            print("True");
        }
        else
        {
            rend = GetComponent<Renderer>();
            rend.enabled = false;
            print("False");
        }
    }



    private bool TrapShouldTrigger(Collider2D collision) {

        if (collision.gameObject == owner) {
            return false;
        }
        return true;
    }
    
    private void OnTriggerEnter2D(Collider2D collision) {
    
        if (TrapShouldTrigger(collision)) {
            CmdTriggertrap();
        }
        
    }

    [Command]
    public void CmdTriggertrap()
    {

        print("Faire du dégat");
        Destroy(gameObject);
    }
}
