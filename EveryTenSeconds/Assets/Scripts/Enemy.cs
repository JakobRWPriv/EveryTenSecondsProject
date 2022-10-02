using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MissionObject
{
    public PlayerController player;
    public float speed;

    public GameObject facePivot;
    public GameObject faceObj;
    public GameObject tentaclesPivot;

    public GameObject deathParticles;

    void Start()
    {
        
    }

    void Update() {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        if (transform.localScale.x > 0 && player.transform.position.x < transform.position.x) {
            transform.localScale = new Vector3(-1.5f, 1.5f, 1);
            facePivot.transform.localScale = new Vector3(-1f, 1f, 1);
            faceObj.transform.localScale = new Vector3(-1f, 1f, 1);
            tentaclesPivot.transform.localScale = new Vector3(-1f, 1f, 1);
        } else if (transform.localScale.x < 0 && player.transform.position.x > transform.position.x) {
            transform.localScale = new Vector3(1.5f, 1.5f, 1);
            facePivot.transform.localScale = new Vector3(1f, 1f, 1);
            faceObj.transform.localScale = new Vector3(1f, 1f, 1);
            tentaclesPivot.transform.localScale = new Vector3(1f, 1f, 1);
        }

        LookAtPlayer();
    }

    float angleToSet;
    float angleSmoothing;
    float offset;
    public float rotateSpeed;

    void LookAtPlayer() {
        Vector2 direction = (Vector2)player.transform.position - (Vector2)transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * (Mathf.Rad2Deg);

        Quaternion dir = Quaternion.Euler(Vector3.forward * (angle + offset));

        angleToSet = Mathf.SmoothDampAngle(angleToSet, dir.eulerAngles.z, ref angleSmoothing, rotateSpeed);
        facePivot.transform.eulerAngles = new Vector3(0, 0, angleToSet);
        tentaclesPivot.transform.eulerAngles = new Vector3(0, 0, angleToSet);
    }

    void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.tag == "BubbleShot") {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            Destroy(otherCollider.gameObject);
            Destroy(gameObject);
        }
    }
}
