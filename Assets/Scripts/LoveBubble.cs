using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveBubble : MonoBehaviour {

    public SpriteRenderer alienRoom;
    public SpriteRenderer ghostRoom;

    void Update() {
        if (transform.position.y > 10)
            Destroy(gameObject);
    }

    public void OnLoveBubbleCreate(Sprite alien, Sprite ghost) {
        alienRoom.sprite = alien;
        ghostRoom.sprite = ghost;
    }
}
