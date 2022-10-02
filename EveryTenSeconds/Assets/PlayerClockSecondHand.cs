using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClockSecondHand : MonoBehaviour
{
    public void StartClock() {
        transform.eulerAngles = new Vector3(0, 0, 0);
        LeanTween.rotateAround(gameObject, Vector3.forward, -360f, 10f);
    }

    public void StopClock() {
        LeanTween.cancel(gameObject);
    }
}
