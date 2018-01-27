using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour {

    bool isJumping;

    float beginSpeed;

    public float jumpSpeed;
    public float marsGravity;

    void Start() {
        beginSpeed = jumpSpeed;
    }

    // Update is called once per frame
    void Update() {

        if (isJumping) {
            transform.position += (transform.position - transform.parent.position).normalized * jumpSpeed * Time.deltaTime;
            jumpSpeed -= marsGravity * Time.deltaTime;
        }

        if (jumpSpeed < 0 && jumpSpeed < -beginSpeed && isJumping) {
            jumpSpeed = 0;
            isJumping = false;
        }

        if (transform.position.x < -5)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Trigger") {
            isJumping = true;
        }

        if (collision.name == "SpaceShip" && isJumping) {
            collision.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
            collision.GetComponent<SpaceShip>().LoadAlien(gameObject);
        }
    }

}
