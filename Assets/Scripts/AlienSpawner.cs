using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawner : MonoBehaviour {


    public float sendInterval = 1.0f;
    public List<GameObject> alientList = new List<GameObject>();

    Transform rotator;
    // Use this for initialization
    void Start() {
        //GameManager.instance.OnGameStart += BeginSpawnAlien;
        //GameManager.instance.OnGameOver += StopSpawnAlien;
        TempoClock.Instance.Half += SpawnAlien;
        rotator = transform.parent.Find("Rotator");
    }

    private void SpawnAlien(object sender, TempoClock.BeatEventArgs args) {
        Instantiate(alientList[Random.Range(0, alientList.Count)], transform.position, transform.rotation, rotator);
    }

    //void BeginSpawnAlien() {
    //    StartCoroutine(SpawnAlienRoutine());
    //}

    //void StopSpawnAlien() {
    //    StopAllCoroutines();
    //}


    IEnumerator SpawnAlienRoutine() {
        yield return new WaitForSeconds(sendInterval);
        Instantiate(alientList[Random.Range(0, alientList.Count)], transform.position, transform.rotation, rotator);
        StartCoroutine(SpawnAlienRoutine());

    }
}
