using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour {

    public float forcePower;

    Rigidbody2D rigid;

    Color originColor;

    public bool loadingAlien;
    public bool loadingNecromancer;

    // Use this for initialization
    void Start() {
        originColor = GetComponent<SpriteRenderer>().color;
        rigid = GetComponent<Rigidbody2D>();
        GameManager.instance.OnGameOver += GameOver;
    }

    // Update is called once per frame
    void Update() {

        if (GameManager.instance.currentGameState == GameManager.GameState.play) {

            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            if (x != 0 || y != 0) {
                var dir = new Vector2(x, y);
                transform.rotation = Quaternion.Lerp(transform.rotation,
                    Quaternion.Euler(0, 0, x < 0 ? Vector2.Angle(Vector2.up, dir) : -Vector2.Angle(Vector2.up, dir)), 0.5f);
            }

            rigid.AddForce(transform.up * forcePower);

            if (Input.GetKeyDown(KeyCode.J)) {
                rigid.AddForce(transform.up * forcePower, ForceMode2D.Impulse);
            }

            if (transform.position.x < -3 || transform.position.x > 3) {
                GameManager.instance.GameOver();
            }

        }

    }


    public void LoadAlien() {
        loadingAlien = true;
    }

    void LoadNecromancer(GameObject necromancer) {
        GetComponent<SpriteRenderer>().color = necromancer.GetComponent<SpriteRenderer>().color;
        loadingNecromancer = false;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Planet") {
            GameManager.instance.GameOver();
        }
        if (collision.tag == "Necromancer") {
            if (loadingAlien) {
                Match();
            } else {
                LoadNecromancer(collision.gameObject);
            }
            Destroy(collision.gameObject);
        }
    }

    void Match() {
        GetComponent<SpriteRenderer>().color = originColor;
        loadingAlien = false;
        loadingNecromancer = false;
        GameManager.instance.AddMatchPair();
    }

    void OnDestoy() {
        GameManager.instance.OnGameOver -= GameOver;
    }

    void GameOver() {
        Destroy(gameObject);
    }
}
