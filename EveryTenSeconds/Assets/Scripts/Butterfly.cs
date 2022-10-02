using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly : MissionObject
{
    public float xSpeed;
    public float ySpeed;

    public float yMoveDistance;
    public float yMoveTime;

    public GameObject upDownObj;

    void Start() {
        StartCoroutine(DestroyCo());
        StartCoroutine(SortingOrderAdd());
        if (transform.position.x > 0) {
            xSpeed = Random.Range(-4f, -2.5f);
            transform.localScale = new Vector3(1, 1, 1);
        } else {
            xSpeed = Random.Range(2.5f, 4f);
        }

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
    IEnumerator SortingOrderAdd() {
        yield return new WaitForSeconds(0.2f);
        foreach(SpriteRenderer sr in allSprites) {
            sr.sortingOrder = sr.sortingOrder + gameHandler.globalCreatureSortingOrderAddition;
        }
    }

    void Update() {
        Vector2 speed = new Vector2(xSpeed, ySpeed);

        transform.Translate(speed * Time.deltaTime);
    }

    public void HitPlayer() {
        if (!gameHandler.missionIsActive) return;
        
        if (gameHandler.mission == GameHandler.Mission.DoNotTouch) {
            if (missionObjectIndex == gameHandler.activeMissionObject) {
                gameHandler.EndRoundFail();
            }
        } else if (gameHandler.mission == GameHandler.Mission.Touch) {
            if (missionObjectIndex == gameHandler.activeMissionObject) {
                gameHandler.EndRoundWin();
            }
        }
    }

    IEnumerator DestroyCo() {
        yield return new WaitForSeconds(30);
        Destroy(gameObject);
    }
}
