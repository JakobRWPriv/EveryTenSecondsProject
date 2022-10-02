using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public PlayerController player;
    public SpawnerController spawnerController;
    public bool isUpperSpawner;

    public void Spawn(int objIndex) {
        GameObject go = Instantiate(spawnerController.missionObjects[objIndex], transform.position, Quaternion.identity);

        if (objIndex < 4 && isUpperSpawner) {
            Destroy(go);
        }

        if (objIndex > 3 && objIndex < 8) {
            int ran = Random.Range(0, 2);
            if (ran == 1) {
                Destroy(go);
            }
            
            go.GetComponent<Enemy>().player = player;
            go.SetActive(true);
        }
    }
}
