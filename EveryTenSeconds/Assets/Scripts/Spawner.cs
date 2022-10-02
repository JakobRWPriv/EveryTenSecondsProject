using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public PlayerController player;
    public GameHandler gameHandler;
    public SpawnerController spawnerController;

    public void Spawn(int objIndex, bool isForcedSpawn) {
        GameObject go = Instantiate(spawnerController.missionObjects[objIndex], transform.position, Quaternion.identity);

        MissionObject mo = go.GetComponent<MissionObject>();
        mo.player = player;
        mo.gameHandler = gameHandler;

        if (!isForcedSpawn && objIndex > 3) {
            int ran = Random.Range(0, 2);
            if (ran == 1) {
                Destroy(go);
            }
        }

        go.SetActive(true);
    }
}
