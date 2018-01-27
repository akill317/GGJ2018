using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerSpawner : MonoBehaviour {

    public List<GameObject> necromancertList = new List<GameObject>();

    [HideInInspector]
    public Necromancer currentNacromancer;

    void Start() {
    }

    void Update() {
        if (currentNacromancer == null) {
            SpawnNacromancer();
        }
    }

    void SpawnNacromancer() {
        var nacromancer = Instantiate(necromancertList[Random.Range(0, necromancertList.Count)], transform.position, transform.rotation, transform);
        currentNacromancer = nacromancer.GetComponent<Necromancer>();
    }
}
