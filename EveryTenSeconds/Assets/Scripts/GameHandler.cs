using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameHandler : MonoBehaviour
{
    public int roundsPassed;
    public TextMeshProUGUI roundsPassedText;
    public int activeMissionIndex;
    public int activeMissionObject;
    public bool missionIsActive;
    public bool gameIsOver;
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
    public GameObject failCross;
    public GameObject winCheck;
    
    public GameObject getReadyText;
    public GameObject doNotTouchText;
    public GameObject touchText;
    public GameObject defeatText;

    public int lives = 3;
    public GameObject life1, life2, life3;
    public GameObject life1Gone, life2Gone, life3Gone;

    public GameObject gameOverScreen;
    public TextMeshProUGUI roundsLastedText;
    public TextMeshProUGUI highScoreText;

    void Start() {
        StartRound();
    }


    void Update() {
        if (mission == Mission.DoNotTouch) {

        }
    }

    void StartRound() {
        if (gameIsOver) return;

        StartCoroutine(StartRoundCo());
    }

    IEnumerator StartRoundCo() {
        InactivateAllMissionTexts();
        getReadyText.SetActive(true);
        InactivateAllMissionImages();

        RandomizeMission();
        ActivateRelevantMissionImage();

        startRoundCountdown.gameObject.SetActive(true);
        startRoundCountdown.text = "3";
        yield return new WaitForSeconds(0.66f);
        startRoundCountdown.text = "2";
        yield return new WaitForSeconds(0.66f);
        startRoundCountdown.text = "1";
        yield return new WaitForSeconds(0.66f);
        startRoundCountdown.text = "GO!";

        ActivateRelevantMissionText();
        getReadyText.SetActive(false);
        timerBar.SetBarFillToFull();
        timerBar.remainingTime = 10;
        missionIsActive = true;
        player.canDash = true;
        dashActive.SetActive(true);
        dashInactive.SetActive(false);

        yield return new WaitForSeconds(0.66f);
        startRoundCountdown.gameObject.SetActive(false);
    }

    void RandomizeMission() {
        activeMissionIndex = Random.Range(0, 3);
        mission = (Mission)activeMissionIndex;
    }

    public void EndRoundFail() {
        missionIsActive = false;
        StartCoroutine(WaitToStartNewRound());
        timerBar.SetBarFillToEmpty();

        roundsPassed++;
        roundsPassedText.text = "ROUNDS PASSED: " + roundsPassed;

        lives--;

        if (lives == 2) {
            life1.SetActive(false);
            life1Gone.SetActive(true);

            failCross.SetActive(true);
            LeanTween.scale(failCross, new Vector3(1.3f, 1.3f, 1), 0.5f).setEasePunch();
        } else if (lives == 1) {
            life2.SetActive(false);
            life2Gone.SetActive(true);

            failCross.SetActive(true);
            LeanTween.scale(failCross, new Vector3(1.3f, 1.3f, 1), 0.5f).setEasePunch();
        } else if (lives == 0) {
            life3.SetActive(false);
            life3Gone.SetActive(true);

            GameOver();
        }

        StartCoroutine(InactivateObject(failCross));
    }

    IEnumerator InactivateObject(GameObject go) {
        yield return new WaitForSeconds(1f);
        LeanTween.scale(go, new Vector3(0f, 0f, 1), 0.3f).setEaseInCubic();
        yield return new WaitForSeconds(0.5f);
        go.SetActive(false);
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
        } else if (mission == (Mission)1) {
            touchText.SetActive(true);
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
        missionImages[activeMissionObject].SetActive(true);
    }

    public void GameOver() {
        gameIsOver = true;
        gameOverScreen.SetActive(true);

        if (PlayerPrefs.GetInt("ROUNDS_HIGHSCORE", 0) < roundsPassed) {
            PlayerPrefs.SetInt("ROUNDS_HIGHSCORE", roundsPassed);
        }

        highScoreText.text = PlayerPrefs.GetInt("ROUNDS_HIGHSCORE", 0) + " ROUNDS";

        roundsLastedText.text = roundsPassed + " ROUNDS";

        LeanTween.scaleY(gameOverScreen, 1, 0.6f).setEaseOutCubic();
    }
}
