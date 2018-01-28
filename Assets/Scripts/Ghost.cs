using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {

    public float speed;

    float originalSpeed;
    // Use this for initialization
    void Start() {
        originalSpeed = speed;
        //TempoClock.Instance.Quarter += Pause;
    }

    private void Pause(object sender, TempoClock.BeatEventArgs args) {
        if ((TempoClock.Instance.quartercount - 3) % 4 == 0) {
            speed = 0;
        }
        if (TempoClock.Instance.quartercount % 4 == 0) {
            speed = originalSpeed;
        }
    }

    // Update is called once per frame
    void Update() {
        transform.position += Vector3.right * speed * Time.deltaTime;
        if (transform.position.x > 5)
            Destroy(gameObject);
    }

    public void Summoned() {
        //GameManager.instance.nacromancerSpawner.currentNacromancer.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
        GameManager.instance.nacromancerSpawner.currentNacromancer.GetComponent<Animator>().enabled = false;
        GameManager.instance.nacromancerSpawner.currentNacromancer.GetComponent<SpriteRenderer>().sprite
            = GetComponent<SpriteRenderer>().sprite;
        GameManager.instance.nacromancerSpawner.currentNacromancer.GetComponent<SpriteRenderer>().color = Color.white;
        GameManager.instance.nacromancerSpawner.currentNacromancer.SummonSuccecced();
    }
}
