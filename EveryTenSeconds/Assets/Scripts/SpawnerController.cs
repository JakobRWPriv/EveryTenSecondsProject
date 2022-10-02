using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject[] missionObjects;
    public Spawner[] sideSpawners;
    public Spawner[] upperSpawners;

    void Start() {
        Spawn();
    }

    public void Spawn() {
        StartCoroutine(SpawnCo(Random.Range(1f, 1.5f)));
    }

    IEnumerator SpawnCo(float waitTime) {
        yield return new WaitForSeconds(waitTime);

        int objIndex = Random.Range(0, missionObjects.Length - 1);

        int ran = Random.Range(0, 4);

        if (objIndex < 4) {
            sideSpawners[Random.Range(0, sideSpawners.Length)].Spawn(objIndex, false);
        } else if (ran == 1 || ran == 2) {
            sideSpawners[Random.Range(0, sideSpawners.Length)].Spawn(objIndex, false);
        } else if (ran == 3) {
            upperSpawners[Random.Range(0, upperSpawners.Length)].Spawn(objIndex, false);
        }

        StartCoroutine(SpawnCo(Random.Range(1f, 1.5f)));
    }

    public void ForceRelevantSpawn(int index) {
        StartCoroutine(ForceRelevantSpawnCo(index));
    }

    IEnumerator ForceRelevantSpawnCo(int index) {
        print("FORCE SPAWN INDEX " + index);
        yield return new WaitForSeconds(Random.Range(0.3f, 0.8f));

        if (index != 8) {
            int ran = Random.Range(0, 3);

            if (index < 4) {
                sideSpawners[Random.Range(0, sideSpawners.Length)].Spawn(index, true);
                print("FORCE SPAWN 1");
            } else if (ran == 0 || ran == 1) {
                sideSpawners[Random.Range(0, sideSpawners.Length)].Spawn(index, true);
                print("FORCE SPAWN 2");
            } else if (ran == 2) {
                upperSpawners[Random.Range(0, upperSpawners.Length)].Spawn(index, true);
                print("FORCE SPAWN 3");
            }
        } else {
            yield return new WaitForSeconds(2.2f);
            sideSpawners[Random.Range(0, sideSpawners.Length)].Spawn(8, true);
        }
    }
}
