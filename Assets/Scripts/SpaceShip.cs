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

    public ParticleSystem N2O;
    public ParticleSystem Explode;

    bool justPressJ = false;
    // Use this for initialization
    void Start() {
        originColor = GetComponent<SpriteRenderer>().color;
        rigid = GetComponent<Rigidbody2D>();
        GameManager.instance.OnGameOver += GameOver;
        GameManager.instance.serialHandler.OnDataReceived += SerialHandler_OnDataReceived;
    }

    private void SerialHandler_OnDataReceived(string message) {
        var data = message.Split(new string[] { "\t" }, System.StringSplitOptions.None);
        if (data.Length < 4) { return; }
        if (GameManager.instance.currentGameState == GameManager.GameState.play) {

            var angle = Mathf.Lerp(180, -180, (1023 - float.Parse(data[0])) / 1023);
            Debug.Log(data[0]);
            Debug.Log(angle);
            transform.rotation = Quaternion.Euler(0, 0, angle);

            rigid.AddForce(transform.up * forcePower);

            if (data[1] == 1.ToString() && !justPressJ && Time.frameCount > GameManager.instance.gameStartFrame + 5) {
                rigid.AddForce(transform.up * forcePower, ForceMode2D.Impulse);
                N2O.Play();
                N2O.GetComponent<AudioSource>().Play();
                justPressJ = true;
            }
            if (data[1] == 0.ToString()) {
                justPressJ = false;
            }

        }
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

            if (Input.GetKeyDown(KeyCode.J) && Time.frameCount > GameManager.instance.gameStartFrame + 5) {
                rigid.AddForce(transform.up * forcePower, ForceMode2D.Impulse);
                N2O.Play();
                N2O.GetComponent<AudioSource>().Play();
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
        else
            GetComponent<AudioSource>().Play();
        Destroy(alien);
    }

    void LoadGhost(GameObject ghost) {
        GetComponent<SpriteRenderer>().color = ghost.GetComponent<SpriteRenderer>().color;
        ghostRoom.SetActive(true);
        ghostRoom.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = ghost.GetComponent<SpriteRenderer>().sprite;
        loadingGhost = true;
        if (loadingAlien)
            Match(alienRoom.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite, ghost.GetComponent<SpriteRenderer>().sprite);
        else
            GetComponent<AudioSource>().Play();
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
        //GameManager.instance.OnGameOver -= GameOver;
    }

    void GameOver() {
        Explode.transform.position = transform.position;
        Explode.Play();
        Explode.GetComponent<AudioSource>().Play();
        gameObject.SetActive(false);
        GameManager.instance.OnGameOver -= GameOver;
    }
}
