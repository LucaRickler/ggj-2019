using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Current : MonoBehaviour {

    [SerializeField]
    private float _step = 2;
    public float Step {
        get { return _step; }
    }

    [SerializeField]
    private Vector3 _direction;
    public Vector3 Direction {
        get { return _direction; }
    }

    private void Start()
    {
        _direction = transform.forward;
    }

    public void SwapDirection() {
        _direction = -_direction;
    }
}


