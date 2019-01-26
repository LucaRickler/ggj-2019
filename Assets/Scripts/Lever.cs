using UnityEngine;
using System.Collections.Generic;

public class Lever : MonoBehaviour {

    public List<Catapult> catapults = new List<Catapult>();

    public void Use() {
        foreach (Catapult c in catapults) {
            if (c.IsFireable)
                c.Move();
        }
    }

void OnTriggerEnter(Collider other) {
        if(other.tag == "drag-detector") {
            GameManager.Instance.Player.GetComponent<PlayerController>().UsableLever = this;
        }    
    }

    void OnTriggerExit(Collider other) {
        if(other.tag == "drag-detector") {
            GameManager.Instance.Player.GetComponent<PlayerController>().UsableLever = null;
        }
    }
}