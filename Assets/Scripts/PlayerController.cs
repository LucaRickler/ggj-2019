using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float SpeedSpirit = 1;
    [SerializeField]
    private float SpeedAlive = 2;
    [SerializeField]
    private float _speedRotation = 5f;
    public float SpeedRotation {
        get { return _speedRotation; }
    }


    [SerializeField]
    private bool _isAlive;
    public bool IsAlive {
        get { return _isAlive; }
    }

    public float Speed {
      get {
        if (IsAlive)
            return SpeedAlive;
        else
            return SpeedSpirit;
        }
    }

    public Draggable dragged;
    public Draggable Draggable;

    public Transform DragHandle;
    public Transform DropPoint;

    void Awake() {
    }

    void Update() {

        //Aggiornamento movimenti
        Vector3 x = Vector3.zero;
        Vector3 z = Vector3.zero;
        if (InputManager.Up()) z = new Vector3(0, 0, 1);
        else if (InputManager.Down()) z = new Vector3(0, 0, -1);

        if (InputManager.Right()) x = new Vector3(1, 0, 0);
        else if (InputManager.Left()) x = new Vector3(-1, 0, 0);

        Vector3 result = (x + z).normalized;
        if (result != Vector3.zero) {
            transform.position += result * Speed * Time.deltaTime;
            transform.forward += result * SpeedRotation * Time.deltaTime;
        }

        if (InputManager.DragDrop()) DragDrop();

        if (dragged != null) {
            dragged.transform.position = DragHandle.position;
            //TODO: rotation
        }
    }

    void DragDrop() {
        if(Draggable != null) {
            GameManager.Instance.SwitchPlayerState(Draggable);
        } else if(dragged != null) {
            GameManager.Instance.SwitchPlayerState(dragged);
        }
    }

    public void SetDragged(Draggable obj) {
        dragged = obj;
        dragged.ToggleDrag(true);
        dragged.transform.parent = DragHandle;
        dragged.transform.position = DragHandle.position;
    }

    public void SetDraggable(Draggable obj) {
        Draggable = obj;
        Draggable.transform.parent = null;
        Draggable.transform.position = DropPoint.position;
        Draggable.ToggleDrag(false);

    }
}