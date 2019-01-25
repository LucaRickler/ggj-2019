using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class PlayerController : MonoBehaviour {

  private Draggable dragged;
  public Draggable Draggable;

  public Transform DragHandle;
  public Transform DropPoint;

  void Awake() {
  }
  
  void Update() {
      
  }

  void Drag() {
      if(Draggable != null) {
          dragged = Draggable;
          Draggable = null;
          dragged.ToggleDrag(true);
          dragged.transform.parent = DragHandle;
          dragged.transform.position = DragHandle.position;
      }
  }

  void Drop() {
      if(dragged != null) {
          Draggable = dragged;
          dragged = null;
          dragged.transform.parent = null;
          dragged.transform.position = DropPoint.position;
          dragged.ToggleDrag(false);
      }
  }
}