using UnityEngine;

public class Draggable : MonoBehaviour {
    [SerializeField]
    private float drag_radius;
    public float DragRadius {
        get {
            return drag_radius;
        }
    }

    [SerializeField]
    private bool dragged;
    public bool IsDragged {
        get {
            return dragged;
        }
    }

    public bool IsDraggable () {
        return (Vector3.Distance(GameManager.Instance.Player.transform.position, transform.position) <= DragRadius) && !IsDragged;
    }

    public void ToggleDrag(bool state) {
        dragged = state;
    }
}