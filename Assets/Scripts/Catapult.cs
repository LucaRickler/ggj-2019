using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
public class Catapult : CurveMover {

    public bool IsFireable {
        get {
            return IsCharged && companion.IsCharged;
        }
    }
    public Catapult companion;

    public override void Move() {
        if (IsFireable && !animator.GetBool("Moving")) {
            companion.Move();
            StartCoroutine(MoveCicle());
        }
    }
}