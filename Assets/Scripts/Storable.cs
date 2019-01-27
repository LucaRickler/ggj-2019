using UnityEngine;

[RequireComponent(typeof(Draggable))]
public class Storable : MonoBehaviour {
    private Draggable drag;

    public string TargetName;

    void Awake() {
        drag = GetComponent<Draggable>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == TargetName && !drag.IsDragged) {
            transform.parent = other.transform;
            transform.position = other.GetComponent<ObjectBase>().Target.position;
            drag.Grabbable = false;
        }    
    }
}