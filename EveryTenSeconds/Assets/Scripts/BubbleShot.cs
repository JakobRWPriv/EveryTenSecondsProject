using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleShot : MonoBehaviour
{
    Vector2 moveDir;
    public float speed;
    float speedSmoothing;

    public void Shoot(int dir = 0) {
        if (dir == 0) {
            moveDir = new Vector2(1, 0);
        } else if (dir == 1) {
            moveDir = new Vector2(1, -1);
            speed = speed * 0.75f;
        } else if (dir == 2) {
            moveDir = new Vector2(0, -1);
        } else if (dir == 3) {
            moveDir = new Vector2(-1, -1);
            speed = speed * 0.75f;
        } else if (dir == 4) {
            moveDir = new Vector2(-1, 0);
        } else if (dir == 5) {
            moveDir = new Vector2(-1, 1);
            speed = speed * 0.75f;
        } else if (dir == 6) {
            moveDir = new Vector2(0, 1);
        } else if (dir == 7) {
            moveDir = new Vector2(1, 1);
            speed = speed * 0.75f;
        }
        StartCoroutine(DestroyCo());
    }

    void Update() {
        speed = Mathf.SmoothDamp(speed, 0, ref speedSmoothing, 0.3f);

        transform.Translate(moveDir * (speed * Time.deltaTime));
    }

    IEnumerator DestroyCo() {
        yield return new WaitForSeconds(0.6f);
        AudioHandler.Instance.PlaySound(AudioHandler.Instance.BubbleBurst, 0.5f, Random.Range(1f, 1.3f));
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
