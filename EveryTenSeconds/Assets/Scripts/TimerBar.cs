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
        } else if (remainingTime <= 2 && remainingTime > 1) {
            threeSecCountdown.text = "2";
        } else if (remainingTime <= 1 && remainingTime > 0) {
            threeSecCountdown.text = "1";
        } else if (remainingTime <= 0) {
            threeSecCountdown.text = "";

            gameHandler.EndRoundFail();
        }
    }

    public void UpdateBarFill() {
        energyBar.sizeDelta = new Vector2(remainingTime * (787 / 10), energyBar.sizeDelta.y);
    }

    public void SetBarFillToFull() {
        energyBar.sizeDelta = new Vector2(787, energyBar.sizeDelta.y);
        barImage.color = barGreen;
    }
}
