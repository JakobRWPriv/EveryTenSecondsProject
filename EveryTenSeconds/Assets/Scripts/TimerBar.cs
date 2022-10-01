using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBar : MonoBehaviour
{
    public RectTransform energyBar;
    float remainingTime = 10;

    void Start()
    {
        
    }

    void Update() {
        remainingTime -= Time.deltaTime;
        UpdateBarFill();
    }

    public void UpdateBarFill() {
        energyBar.sizeDelta = new Vector2(remainingTime * (787 / 10), energyBar.sizeDelta.y);
    }
}
