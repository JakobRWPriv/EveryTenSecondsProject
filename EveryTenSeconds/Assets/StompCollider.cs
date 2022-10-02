using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompCollider : MonoBehaviour
{
    public Stomp stomp;
    void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.tag == "Player" && otherCollider.isTrigger) {
            gameObject.SetActive(false);
            stomp.gameHandler.EndRoundFail();
            stomp.player.TakeDamage();
        }
    }
}
