using UnityEngine;
using System.Collections.Generic;

public class Lever : MonoBehaviour {

    public void Use() {
        GetComponent<Animator>().SetTrigger("Pull");
        foreach (GameObject c in GameManager.Instance.Catapults) {
            if (c.GetComponent<Catapult>().IsFireable)
                c.GetComponent<Catapult>().Move();
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