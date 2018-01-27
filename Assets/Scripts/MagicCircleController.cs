using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircleController : MonoBehaviour {


    GameObject passByGhost;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.K)) {
            if (passByGhost)
                SuccessSummon();
            else
                FailSummon();
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name.Contains("Ghost")) {
            passByGhost = collision.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.name.Contains("Ghost")) {
            passByGhost = null;
        }
    }

    void SuccessSummon() {
        passByGhost.GetComponent<Ghost>().Summoned();
        Destroy(passByGhost);
    }

    void FailSummon() {

    }
}
