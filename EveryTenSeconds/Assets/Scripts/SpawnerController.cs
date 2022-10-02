using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject[] missionObjects;
    public Spawner[] spawners;

    void Start() {
        Spawn();
    }

    public void Spawn() {
        StartCoroutine(SpawnCo(Random.Range(1f, 1.5f)));
    }

    IEnumerator SpawnCo(float waitTime) {
        yield return new WaitForSeconds(waitTime);

        int objIndex = Random.Range(0, missionObjects.Length);

        spawners[Random.Range(0, spawners.Length)].Spawn(objIndex);

        StartCoroutine(SpawnCo(Random.Range(1f, 1.5f)));
    }
}
