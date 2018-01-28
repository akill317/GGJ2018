using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour {

    public float maxAngle;

    public float increaseSpeed;

    public float launchPower;

    float value;

    bool flip;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (GameManager.instance.currentGameState == GameManager.GameState.play) {
            if (flip) {
                if (value < 1) {
                    value += increaseSpeed;
                } else {
                    value = 1;
                }
            } else {
                if (value > 0) {
                    value -= increaseSpeed;
                } else {
                    value = 0;
                }
            }

            if (Input.GetKeyDown(KeyCode.L))
                flip = !flip;



            transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(-maxAngle, maxAngle, value));
        }
    }

    public void Launch(GameObject nacromancer) {
        nacromancer.transform.position = transform.position + transform.up;
        var nacroRigid = nacromancer.GetComponent<Rigidbody2D>();
        nacroRigid.simulated = true;
        nacroRigid.AddForce(transform.up * launchPower, ForceMode2D.Impulse);
    }
}
