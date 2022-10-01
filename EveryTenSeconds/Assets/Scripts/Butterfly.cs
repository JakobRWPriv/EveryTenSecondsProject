using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly : MonoBehaviour
{
    public float xSpeed;
    public float ySpeed;

    public float yMoveDistance;
    public float yMoveTime;

    public string butterFlyColor;

    public GameObject upDownObj;

    void Start() {
        xSpeed = Random.Range(2.5f, 4f);

        if (transform.position.y > 0) {
            ySpeed = Random.Range(-2f, 0f);
            yMoveDistance = Random.Range(-1.5f, 0.5f);
        } else {
            ySpeed = Random.Range(0f, 2f);
            yMoveDistance = Random.Range(0.5f, 1.5f);
        }

        yMoveTime = Random.Range(0.8f, 1f);

        LeanTween.moveLocalY(upDownObj, yMoveDistance, yMoveTime).setLoopPingPong().setEaseInOutCubic();
    }

    void Update() {
        Vector2 speed = new Vector2(xSpeed, ySpeed);

        transform.Translate(speed * Time.deltaTime);
    }
}
