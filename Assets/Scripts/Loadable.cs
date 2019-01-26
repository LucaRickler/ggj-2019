using UnityEngine;

[RequireComponent(typeof(Draggable))]
public class Loadable : MonoBehaviour {

    public CurveMover Mover;
    private Draggable drag;

    void Awake() {
        drag = GetComponent<Draggable>();
    }

    void Update() {
        if (drag.IsDragged) {
            Mover.Empty();
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.tag == "mover") {
            Mover = other.GetComponent<CurveMover>();
            Mover.Charge(gameObject);
        }
    }
}