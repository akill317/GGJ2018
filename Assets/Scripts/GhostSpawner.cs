using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour {

    public float sendInterval = 1.0f;
    public List<GameObject> ghostList = new List<GameObject>();

    // Use this for initialization
    void Start() {
        //GameManager.instance.OnGameStart += BeginSpawnGhost;
        //GameManager.instance.OnGameOver += StopSpawnGhost;
        TempoClock.Instance.Half += SpawnGhost; ;
    }

    private void SpawnGhost(object sender, TempoClock.BeatEventArgs args) {
        Instantiate(ghostList[Random.Range(0, ghostList.Count)], transform.position, transform.rotation, transform);
    }

    //void BeginSpawnGhost() {
    //    StartCoroutine(SpawnGhostRoutine());
    //}

    //void StopSpawnGhost() {
    //    StopAllCoroutines();
    //}

    IEnumerator SpawnGhostRoutine() {
        yield return new WaitForSeconds(sendInterval);
        Instantiate(ghostList[Random.Range(0, ghostList.Count)], transform.position, transform.rotation, transform);
        StartCoroutine(SpawnGhostRoutine());
    }
}
