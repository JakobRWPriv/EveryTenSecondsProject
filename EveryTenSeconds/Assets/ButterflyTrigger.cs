using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyTrigger : MonoBehaviour
{
    public Butterfly butterfly;
    
    void OnTriggerStay2D(Collider2D otherCollider) {
        if (otherCollider.tag == "Player" && otherCollider.isTrigger) {
            butterfly.HitPlayer();
        }
    }
}
