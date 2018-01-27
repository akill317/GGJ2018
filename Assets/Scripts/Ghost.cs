using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {

    public float speed;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        transform.position += Vector3.right * speed * Time.deltaTime;
        if (transform.position.x > 5)
            Destroy(gameObject);
    }

    public void Summoned() {
        GameManager.instance.nacromancerSpawner.currentNacromancer.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
        GameManager.instance.nacromancerSpawner.currentNacromancer.SummonSuccecced();
    }
}
