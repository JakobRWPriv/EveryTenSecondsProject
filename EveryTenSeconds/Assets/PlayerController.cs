using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 velocity;
    float velocityXSmoothing;
    float velocityYSmoothing;
    public int xDir = 0;
    public int yDir = 0;
    public float xDirAndFacing = 0.1f;
    float KeysDirectionX, KeysDirectionXAndFacing, KeysDirectionY;
    float keysDirectionXSmoothing, keysDirectionXAndFacingSmoothing, keysDirectionYSmoothing;

    public float moveSpeed = 8f;
    public float accelerationTime = 0.2f;
    public int latestDirectionX = 1;
    public int latestDirectionY = -1;

    public Animator animator;
    public GameObject face;
    public GameObject clockFlipTransform;
    public SpriteRenderer[] clockSRs;

    public GameObject bubbleShot;
    public Transform bubbleShotTransform;

    public int bubbleDirection = 0;

    void Start()
    {
        
    }

    void Update()
    {
        float targetVelocityX = 0;
        float targetVelocityY = 0;

        Shoot();
        DirectionalFaceDisappearing();
        DetermineDirection();
        Vector2 input = new Vector2(xDir, yDir);
        
        targetVelocityX = (input.x * moveSpeed);
        targetVelocityY = (input.y * moveSpeed);

        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, accelerationTime);
        velocity.y = Mathf.SmoothDamp(velocity.y, targetVelocityY, ref velocityYSmoothing, accelerationTime);
        
        transform.Translate(velocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftArrow)) {
            if (latestDirectionX == 1) {
                if (!Input.GetKey(KeyCode.RightArrow)) {
                    transform.localScale = new Vector3(-1, 1, 1);
                    clockFlipTransform.transform.localScale = new Vector3(-1, 1, 1);
                    animator.SetTrigger("Flip");
                    latestDirectionX = -1;
                }
            }
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            if (latestDirectionX == -1) {
                if (!Input.GetKey(KeyCode.LeftArrow)) {
                    transform.localScale = new Vector3(1, 1, 1);
                    clockFlipTransform.transform.localScale = new Vector3(1, 1, 1);
                    animator.SetTrigger("Flip");
                    latestDirectionX = 1;
                }
            }
        }
        
        animator.SetFloat("KeysDirectionXAndFacing", KeysDirectionXAndFacing = Mathf.SmoothDamp(KeysDirectionXAndFacing, xDirAndFacing, ref keysDirectionXAndFacingSmoothing, 0f));
        animator.SetFloat("KeysDirectionY", KeysDirectionY = Mathf.SmoothDamp(KeysDirectionY, yDir, ref keysDirectionYSmoothing, 0f));
        
        //KeysDirectionXAndFacing = Mathf.Sign(velocity.y);

        DetermineBubbleDirection();
    }

    void Shoot() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            BubbleShot bubble = Instantiate(bubbleShot, bubbleShotTransform.position, Quaternion.identity).GetComponent<BubbleShot>();
            bubble.gameObject.SetActive(true);
            bubble.Shoot(bubbleDirection);
        }
    }

    void DetermineDirection() {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            xDir = -1;
            xDirAndFacing = 1;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            xDir = 1;
            xDirAndFacing = 1;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            xDirAndFacing = 0.1f;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow)) {
            xDirAndFacing = 0.1f;
        }
        if ((!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) || 
        (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))) {
            xDir = 0;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            yDir = -1;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow)) {
            yDir = 0;
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            yDir = 1;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            yDir = 0;

            face.SetActive(true);
            foreach(SpriteRenderer sr in clockSRs) {
                sr.sortingOrder = sr.sortingOrder - 200;
            }
            latestDirectionY = -1;
        }
        if ((!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow)) || 
        (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow))) {
            yDir = 0;
        }
    }

    void DirectionalFaceDisappearing() {
        if (yDir == 1 && latestDirectionY == -1) {
            face.SetActive(false);
            foreach(SpriteRenderer sr in clockSRs) {
                sr.sortingOrder = sr.sortingOrder + 200;
            }
            latestDirectionY = 1;
        } else if (yDir == -1 && latestDirectionY == 1) {
            face.SetActive(true);
            foreach(SpriteRenderer sr in clockSRs) {
                sr.sortingOrder = sr.sortingOrder - 200;
            }
            latestDirectionY = -1;
        }
    }

    void DetermineBubbleDirection() {
        if (xDir > 0 && yDir < 0) {
            bubbleDirection = 1;
        } else if (xDir < 0 && yDir < 0) {
            bubbleDirection = 3;
        } else if (xDir < 0 && yDir > 0) {
            bubbleDirection = 5;
        } else if (xDir > 0 && yDir > 0) {
            bubbleDirection = 7;
        } else if (xDir > 0) {
            bubbleDirection = 0;
        } else if (yDir < 0) {
            bubbleDirection = 2;
        } else if (xDir < 0) {
            bubbleDirection = 4;
        } else if (yDir > 0) {
            bubbleDirection = 6;
        } 
    }
}
