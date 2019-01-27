using UnityEngine;

public class BaseTrigger : MonoBehaviour {
    
    [SerializeField]
    private bool triggered;
    public bool IsTriggered {
        get {
            return triggered;
        }
    }

    public BaseTrigger Companion;

    public string TargetName;

    public Animator DoorTarget;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == TargetName) {
            triggered = true;
            if (Companion != null) {
                if (Companion.triggered) 
                    DoorTarget.SetTrigger("Open");
            } else
                DoorTarget.SetTrigger("Open");
        }
    }
}