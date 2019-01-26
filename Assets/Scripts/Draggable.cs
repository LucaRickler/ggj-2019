using UnityEngine;

public class Draggable : MonoBehaviour {
    [SerializeField]
    private bool dragged;
    public bool IsDragged {
        get {
            return dragged;
        }
    }

    public bool Grabbable = true;

    public void ToggleDrag(bool state) {
        dragged = state;
    }

    void OnTriggerEnter(Collider other) {
        if(other.tag == "drag-detector") {
            GameManager.Instance.Player.GetComponent<PlayerController>().Draggable = this;
        }    
    }

    void OnTriggerExit(Collider other) {
        if(other.tag == "drag-detector") {
            GameManager.Instance.Player.GetComponent<PlayerController>().Draggable = null;
        }
    }
}