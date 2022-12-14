using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerBar : MonoBehaviour
{
    public GameHandler gameHandler;

    public RectTransform energyBar;
    public float remainingTime = 10;
    public Image barImage;
    public Color barGreen, barYellow, barRed;
    public TextMeshProUGUI threeSecCountdown;
    public GameObject sound1, sound2, sound3;

    void Start()
    {
        
    }

    void Update() {
        if (!gameHandler.missionIsActive) return;

        remainingTime -= Time.deltaTime;
        UpdateBarFill();

        if (remainingTime > 6) {
            barImage.color = barGreen;
        } else if (remainingTime >= 3 && remainingTime <= 6) {
            barImage.color = barYellow;
        } else if (remainingTime < 3) {
            barImage.color = barRed;
        }

        if (remainingTime > 3) {
            threeSecCountdown.text = "";
        } else if (remainingTime <= 3 && remainingTime > 2) {
            threeSecCountdown.text = "3";
            sound1.SetActive(true);
        } else if (remainingTime <= 2 && remainingTime > 1) {
            threeSecCountdown.text = "2";
            sound2.SetActive(true);
        } else if (remainingTime <= 1 && remainingTime > 0) {
            threeSecCountdown.text = "1";
            sound3.SetActive(true);
        } else if (remainingTime <= 0) {
            threeSecCountdown.text = "";

            if (gameHandler.mission == GameHandler.Mission.DoNotTouch) {
                gameHandler.EndRoundWin();
            } else if (gameHandler.mission == GameHandler.Mission.WatchOut) {
                gameHandler.EndRoundWin();
                gameHandler.stompIsActive = false;
            } else {
                gameHandler.EndRoundFail();
            }
        }
    }

    public void RemoveCountDownText() {
        threeSecCountdown.text = "";
    }

    public void UpdateBarFill() {
        energyBar.sizeDelta = new Vector2(remainingTime * (787 / 10), energyBar.sizeDelta.y);
    }

    public void SetBarFillToFull() {
        energyBar.sizeDelta = new Vector2(787, energyBar.sizeDelta.y);
        barImage.color = barGreen;
    }

    public void SetBarFillToEmpty() {
        energyBar.sizeDelta = new Vector2(0, energyBar.sizeDelta.y);
        remainingTime = 0;
    }

    public void InactivateSounds() {
        sound1.SetActive(false);
        sound2.SetActive(false);
        sound3.SetActive(false);
    }
}
