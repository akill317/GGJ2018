using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour {

    public float forcePower;

    Rigidbody2D rigid;

    Color originColor;

    public bool loadingAlien;
    public bool loadingGhost;

    public GameObject loveBubblePrefab;

    public GameObject alienRoom;
    public GameObject ghostRoom;

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


    public void LoadAlien(GameObject alien) {
        alienRoom.SetActive(true);
        alienRoom.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = alien.GetComponent<SpriteRenderer>().sprite;
        loadingAlien = true;
        if (loadingGhost)
            Match(alien.GetComponent<SpriteRenderer>().sprite, ghostRoom.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite);
        Destroy(alien);
    }

    void LoadGhost(GameObject ghost) {
        GetComponent<SpriteRenderer>().color = ghost.GetComponent<SpriteRenderer>().color;
        ghostRoom.SetActive(true);
        ghostRoom.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = ghost.GetComponent<SpriteRenderer>().sprite;
        loadingGhost = true;
        if (loadingAlien)
            Match(alienRoom.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite, ghost.GetComponent<SpriteRenderer>().sprite);
        Destroy(ghost);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Planet") {
            GameManager.instance.GameOver();
        }
        if (collision.tag == "Necromancer") {
            LoadGhost(collision.gameObject);
        }
    }

    void Match(Sprite alien, Sprite ghost) {
        GetComponent<SpriteRenderer>().color = originColor;
        loadingAlien = false;
        loadingGhost = false;
        alienRoom.SetActive(false);
        ghostRoom.SetActive(false);
        GameManager.instance.AddMatchPair();
        var loveBubble = Instantiate(loveBubblePrefab, transform.position, Quaternion.identity);
        loveBubble.GetComponent<LoveBubble>().OnLoveBubbleCreate(alien, ghost);
    }

    void OnDestoy() {
        GameManager.instance.OnGameOver -= GameOver;
    }

    void GameOver() {
        Destroy(gameObject);
    }
}
