using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomp : MissionObject
{
    public Animator animator;
    public Vector3 playerFollowVector;
    public SpriteRenderer sr;
    
    void Start() {
        StartCoroutine(StompCo());
    }

    void Update() {
        if (player.xDir > 0) {
            playerFollowVector.x = 4f;
        }
        if (player.xDir < 0) {
            playerFollowVector.x = -4f;
        }
        if (player.xDir == 0) {
            playerFollowVector.x = 0;
        }
        if (player.yDir > 0) {
            playerFollowVector.y = 4f;
        }
        if (player.yDir < 0) {
            playerFollowVector.y = -4f;
        }
        if (player.yDir == 0) {
            playerFollowVector.y = 0;
        }

        if (player.transform.position.y >= transform.position.y - 1) {
            sr.sortingOrder = 500;
        } else {
            sr.sortingOrder = 0;
        }
    }

    IEnumerator StompCo() {
        transform.position = player.transform.position + playerFollowVector;
        if (transform.position.y < -5.28f) {
            transform.position = new Vector3(transform.position.x, -5.28f, 1);
        }
        animator.SetTrigger("Stomp");
        yield return new WaitForSeconds(2.1f);
        if (!gameHandler.missionIsActive) {
            Destroy(gameObject);
        }
        animator.ResetTrigger("Stomp");
        yield return new WaitForSeconds(Random.Range(0.4f, 1f));
        transform.position = player.transform.position + playerFollowVector;
        if (transform.position.y < -5.28f) {
            transform.position = new Vector3(transform.position.x, -5.28f, 1);
        }
        animator.SetTrigger("Stomp");
        yield return new WaitForSeconds(2.1f);
        if (!gameHandler.missionIsActive) {
            Destroy(gameObject);
        }
        animator.ResetTrigger("Stomp");
        yield return new WaitForSeconds(Random.Range(0.4f, 1f));
        transform.position = player.transform.position + playerFollowVector;
        if (transform.position.y < -5.28f) {
            transform.position = new Vector3(transform.position.x, -5.28f, 1);
        }
        animator.SetTrigger("Stomp");
        yield return new WaitForSeconds(2.1f);
        Destroy(gameObject);
    }
}
