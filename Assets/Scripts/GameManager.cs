using UnityEngine;

public class GameManager : MonoBehaviour {
  [SerializeField]
    GameObject player;
    public GameObject Player {
        get {
            return player;
        }
    }

    //----------------------------------------------------------------//
    private static GameManager instance;

    public static GameManager Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<GameManager> ();
            }

            return instance;
        }
    }

    //----------------------------------------------------------------//

    void Awake () {
        instance = FindObjectOfType<GameManager> ();
        if (instance != null && instance != this) {
            Destroy (gameObject);
        }
        instance = this;
    }
}