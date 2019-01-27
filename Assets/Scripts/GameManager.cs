using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField]
    GameObject playerSolid;
    [SerializeField]
    GameObject playerPhantom;

    [SerializeField]
    GameObject solidEnvironment;

    [SerializeField]
    GameObject phantomEnvironment;

    [SerializeField]
    Current[] listCurrents;

    [SerializeField]
    private Camera MainCamera;
    private Vector3 cameraOffset;
    [SerializeField]
    GameObject[] catapults;

    [SerializeField]
    CanvasGroup fadeImage;

    public GameObject Player {
        get {
            if (PlayerIsPhantom)
                return playerPhantom;
            else
                return playerSolid;
        }
    }

    [SerializeField]
    private bool is_phantom;
    public bool PlayerIsPhantom {
        get {
            return is_phantom;
        }
    }

    public Current[] ListCurrents 
    {
        get
        {
            return listCurrents;
        }
    }
    
    public GameObject[] Catapults
    {
        get
        {
            return Catapults;
        }
    }

    private bool is_change = false;

    private bool follow_player = true;

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
        cameraOffset = MainCamera.transform.position - Player.transform.position;
    }

    void Update() {
        if(PlayerIsPhantom) {
            playerSolid.transform.position = playerPhantom.transform.position;
            playerSolid.transform.rotation = playerPhantom.transform.rotation;
        } else {
            playerPhantom.transform.position = playerSolid.transform.position;
            playerPhantom.transform.rotation = playerSolid.transform.rotation;
        }

        if (follow_player)
            MainCamera.transform.position = Player.transform.position + cameraOffset;
    }

    public void SwitchPlayerState(Draggable obj) {
        StartCoroutine(FadeCycle(obj));
    }
    private IEnumerator FadeCycle(Draggable obj) {
        while (!Mathf.Approximately(fadeImage.alpha, 1.0f))
        {
            fadeImage.alpha += 2 * Time.deltaTime;
            yield return null;
        }

        is_phantom = !is_phantom;
        playerPhantom.GetComponent<PlayerController>().Draggable = null;
        playerSolid.GetComponent<PlayerController>().dragged = null;
        playerPhantom.SetActive(PlayerIsPhantom);
        phantomEnvironment.SetActive(PlayerIsPhantom);
        playerSolid.SetActive(!PlayerIsPhantom);
        solidEnvironment.SetActive(!PlayerIsPhantom);

        if (PlayerIsPhantom) {
            playerPhantom.GetComponent<PlayerController>().SetDraggable(obj);
        } else {
            playerSolid.GetComponent<PlayerController>().SetDragged(obj);
        }

        while (!Mathf.Approximately(fadeImage.alpha, 0.0f))
        {
            fadeImage.alpha -= 2 * Time.deltaTime;
            yield return null;
        }
        is_change = false;
    }

    public void ToggleCameraFollow() {
        follow_player = !follow_player;
    }
}