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
    public int latestDirection = 1;

    public Animator animator;
    public GameObject face;
    public GameObject clockFlipTransform;

    void Start()
    {
        
    }

    void Update()
    {
        float targetVelocityX = 0;
        float targetVelocityY = 0;

        DirectionalFaceDisappearing();
        DetermineDirection();
        Vector2 input = new Vector2(xDir, yDir);
        
        targetVelocityX = (input.x * moveSpeed);
        targetVelocityY = (input.y * moveSpeed);

        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, accelerationTime);
        velocity.y = Mathf.SmoothDamp(velocity.y, targetVelocityY, ref velocityYSmoothing, accelerationTime);
        
        transform.Translate(velocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftArrow)) {
            if (latestDirection == 1) {
                transform.localScale = new Vector3(-1, 1, 1);
                clockFlipTransform.transform.localScale = new Vector3(-1, 1, 1);
            }
            latestDirection = -1;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            if (latestDirection == -1) {
                transform.localScale = new Vector3(1, 1, 1);
                clockFlipTransform.transform.localScale = new Vector3(1, 1, 1);
            }
            latestDirection = 1;
        }
        
        //animator.SetFloat("KeysDirectionX", KeysDirectionX = Mathf.SmoothDamp(KeysDirectionX, xDir, ref keysDirectionXSmoothing, 0.2f));
        animator.SetFloat("KeysDirectionXAndFacing", KeysDirectionXAndFacing = Mathf.SmoothDamp(KeysDirectionXAndFacing, xDirAndFacing, ref keysDirectionXAndFacingSmoothing, 0.1f));
        animator.SetFloat("KeysDirectionY", KeysDirectionY = Mathf.SmoothDamp(KeysDirectionY, yDir, ref keysDirectionYSmoothing, 0.2f));
        if (KeysDirectionY < 0.1f) {
            KeysDirectionY = 0;
        }
        
        //KeysDirectionXAndFacing = Mathf.Sign(velocity.y);
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
        if (Input.GetKey(KeyCode.UpArrow)) {
            yDir = 1;
        }
        if ((!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow)) || 
        (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow))) {
            yDir = 0;
        }
    }

    void DirectionalFaceDisappearing() {
        if (yDir == 1) {
            face.SetActive(false);
        } else {
            face.SetActive(true);
        }
    }
}
