using UnityEngine;

public static class InputManager {
  public static bool Up() {
    return Input.GetKey("w");
  }
  
  public static bool Down() {
    return Input.GetKey("s");
  }

  public static bool Left() {
    return Input.GetKey("a");
  }

  public static bool Right() {
    return Input.GetKey("d");
  }

  public static bool DragDrop() {
    return Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space);
  }

}