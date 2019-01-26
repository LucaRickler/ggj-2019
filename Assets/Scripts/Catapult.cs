using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
public class Catapult : CurveMover {
    public Catapult companion;

    public override void Move() {
        if (IsCharged && companion.IsCharged) {
            StartCoroutine(MoveCicle());
        }
    }
}