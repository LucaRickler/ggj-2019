using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Draggable))]
public class Teleportable : MonoBehaviour {

    private Draggable drag;

    private bool alreadyTeleported;

    void Awake() {
        drag = GetComponent<Draggable>();
    }
    void OnTriggerEnter(Collider other) {
        if(other.tag == "tube" && !alreadyTeleported && !drag.IsDragged) {
            StartCoroutine(Teleport(other.GetComponent<Teleporter>()));
        }
    }

    void Update() {
        if(drag.IsDragged) alreadyTeleported = false;
    }

    private IEnumerator Teleport(Teleporter porter) {
        alreadyTeleported = true;
        drag.Grabbable = false;
        yield return new WaitForSeconds(1.0f);
        transform.position = porter.Companion.Exit.position;
        drag.Grabbable = true;
        //TODO: sound effect

    }
}