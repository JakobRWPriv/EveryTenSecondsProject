using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameHandler : MonoBehaviour
{
    public int roundsPassed;
    public int activeMissionIndex;
    public bool missionIsActive;
    public TimerBar timerBar;
    public PlayerController player;
    public GameObject dashActive;
    public GameObject dashInactive;

    public enum Mission {
        DoNotTouch, Touch, Defeat
    };
    public Mission mission;

    public MissionObject[] missionObjects;
    public MissionObject[] missionObjectsDefeatable;

    public GameObject[] missionImages;

    public TextMeshProUGUI startRoundCountdown;
    
    public GameObject getReadyText;
    public GameObject doNotTouchText;
    public GameObject touchText;
    public GameObject defeatText;

    void Start() {
        StartRound();
    }


    void Update() {
        
    }

    void StartRound() {
        StartCoroutine(StartRoundCo());
    }

    IEnumerator StartRoundCo() {
        InactivateAllMissionTexts();
        getReadyText.SetActive(true);
        InactivateAllMissionImages();

        startRoundCountdown.gameObject.SetActive(true);
        startRoundCountdown.text = "3";
        yield return new WaitForSeconds(0.66f);
        startRoundCountdown.text = "2";
        yield return new WaitForSeconds(0.66f);
        startRoundCountdown.text = "1";
        yield return new WaitForSeconds(0.66f);
        startRoundCountdown.text = "GO!";

        getReadyText.SetActive(false);
        timerBar.SetBarFillToFull();
        timerBar.remainingTime = 10;
        missionIsActive = true;
        player.canDash = true;
        dashActive.SetActive(true);
        dashInactive.SetActive(false);
        RandomizeMission();

        yield return new WaitForSeconds(0.66f);
        startRoundCountdown.gameObject.SetActive(false);
    }

    void RandomizeMission() {
        activeMissionIndex = Random.Range(0, 3);
        mission = (Mission)activeMissionIndex;
        ActivateRelevantMissionText();
    }

    public void EndRoundFail() {
        missionIsActive = false;
        StartCoroutine(WaitToStartNewRound());

        roundsPassed++;
    }

    public void EndRoundWin() {
        roundsPassed++;
    }

    IEnumerator WaitToStartNewRound() {
        yield return new WaitForSeconds(2);
        StartRound();
    }

    public void InactivateAllMissionTexts() {
        doNotTouchText.gameObject.SetActive(false);
        touchText.gameObject.SetActive(false);
        defeatText.gameObject.SetActive(false);
    }

    public void ActivateRelevantMissionText() {
        if (mission == (Mission)0) {
            doNotTouchText.SetActive(true);
            ActivateRelevantMissionImage();
        } else if (mission == (Mission)1) {
            touchText.SetActive(true);
            ActivateRelevantMissionImage();
        } else if (mission == (Mission)2) {
            defeatText.SetActive(true);
        }
    }

    public void InactivateAllMissionImages() {
        foreach (GameObject go in missionImages) {
            go.SetActive(false);
        }
    }

    public void ActivateRelevantMissionImage() {
        missionImages[activeMissionIndex].SetActive(true);
    }
}
