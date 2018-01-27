using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Necromancer : MonoBehaviour {

    public float moveSpeed = 1;

    // Use this for initialization
    void Start() {
        transform.DOMoveX(2, 1);
    }

    void Update() {
        if (transform.position.y < -8)
            Destroy(gameObject);
    }

    public void SummonSuccecced() {
        GameManager.instance.nacromancerSpawner.currentNacromancer = null;
        var launcherPos = GameManager.instance.Launcher.transform.position;
        transform.DOMove(launcherPos, moveSpeed).OnComplete(() => {
            GameManager.instance.Launcher.Launch(gameObject);
        });
    }
}
