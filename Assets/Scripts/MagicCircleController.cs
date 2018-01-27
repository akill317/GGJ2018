using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MagicCircleController : MonoBehaviour {


    GameObject passByGhost;
    bool enableSummon;

    public Sprite originSprite;
    public Sprite canNotSummonSprite;

    // Use this for initialization
    void Start() {
        enableSummon = true;
    }

    // Update is called once per frame
    void Update() {
        if (enableSummon && Input.GetKeyDown(KeyCode.K)) {
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
        enableSummon = false;
        GetComponent<SpriteRenderer>().sprite = canNotSummonSprite;
        GetComponent<SpriteRenderer>().DOFade(0, 2).OnComplete(() => {
            GetComponent<SpriteRenderer>().DOFade(1, 0.2f).OnComplete(() => {
                GetComponent<SpriteRenderer>().sprite = originSprite;
                enableSummon = true;
            });
        });
    }
}
