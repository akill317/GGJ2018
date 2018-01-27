using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveBubble : MonoBehaviour {

    public SpriteRenderer alienRoom;
    public SpriteRenderer ghostRoom;

    public void OnLoveBubbleCreate(Sprite alien, Sprite ghost) {
        alienRoom.sprite = alien;
        ghostRoom.sprite = ghost;
    }
}
