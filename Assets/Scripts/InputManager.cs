using UnityEngine;

public static class InputManager {
  public static bool Up() {
    return Input.GetKey("w") || Input.GetAxis("Vertical") > 0;
  }
  
  public static bool Down() {
    return Input.GetKey("s") || Input.GetAxis("Vertical") < 0;
  }

  public static bool Left() {
    return Input.GetKey("a") || Input.GetAxis("Horizontal") < 0;
  }

  public static bool Right() {
    return Input.GetKey("d") || Input.GetAxis("Horizontal") > 0;
  }

  public static bool DragDrop() {
    return Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0);
  }

}