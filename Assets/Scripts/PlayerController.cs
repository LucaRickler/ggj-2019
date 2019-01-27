using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float SpeedSpirit = 1;
    [SerializeField]
    private float SpeedAlive = 2;
    public float Speed{
        get{
            if (GameManager.Instance.PlayerIsPhantom)
                return SpeedSpirit;
            else
                return SpeedAlive;
        }
    }

    [SerializeField]
    private float _speedRotation = 5f;
    public float SpeedRotation {
        get { return _speedRotation; }
    }
    
    [SerializeField]
    private bool _unmovable = false;
    public bool Unmovable {
        get { return _unmovable; }
    }

    [SerializeField]
    private float _angle;
    public float Angle {
        get { return _angle; }
    }

    public Draggable dragged;
    public Draggable Draggable;

    public Lever UsableLever;

    public Transform DragHandle;
    public Transform DropPoint;

    Vector3 LastDirection = new Vector3(0,0,1);

    Animator animator;
    Rigidbody body;

    void Awake() {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
    }

    void Update() {
        //Aggiornamento movimenti
        Vector3 x = Vector3.zero;
        Vector3 z = Vector3.zero;

        animator.SetBool("Walk",(InputManager.Up() || InputManager.Down() || InputManager.Right() || InputManager.Left()&&!Unmovable));
        animator.SetBool("Falling", body.velocity.y < -0.2f);

        if(!Unmovable){
            if (InputManager.Up()) z = new Vector3(0, 0, 1);
            else if (InputManager.Down()) z = new Vector3(0, 0, -1);

            if (InputManager.Right()) x = new Vector3(1, 0, 0);
            else if (InputManager.Left()) x = new Vector3(-1, 0, 0);

            Vector3 result = (x + z).normalized;
            if (result != Vector3.zero) {
                transform.position += result * Speed * Time.deltaTime;
                
                // Correzione caso limite asse X e Z
                if (Mathf.Approximately(Mathf.Abs(Vector3.Dot(result,LastDirection)),1))
                    transform.forward += 4.0f*result * SpeedRotation * Time.deltaTime;
                else
                    transform.forward += result * SpeedRotation * Time.deltaTime;
                //Camera.main.transform.LookAt(transform.position);
                LastDirection = result;
            }
        }
        if (InputManager.DragDrop()) DragDrop();

        if (dragged != null) {
            dragged.transform.position = DragHandle.position;
            //TODO: rotation
        }
    }

    void DragDrop() {
        if(Draggable != null && dragged == null && Vector3.Angle(transform.forward, Draggable.transform.position - transform.position) < Angle && Draggable.Grabbable) {
            GameManager.Instance.SwitchPlayerState(Draggable);
            animator.SetTrigger("Grab");
        } else if(dragged != null) {
            GameManager.Instance.SwitchPlayerState(dragged);
            animator.SetTrigger("Drop");
        }

        if (UsableLever != null && dragged == null) {
            UsableLever.Use();
            animator.SetTrigger("Grab");
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

    public void SetUnmovable(bool value){
        _unmovable = value;
    }
}