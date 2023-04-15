using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsClimb : MonoBehaviour
{
    BoxCollider2D boxCollider2D;

    void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            playerController.CanClimb = true;
            playerController.ladderPlayerClimb = boxCollider2D;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            playerController.CanClimb = false;
            playerController.IsClimbing = false;
        }
    }
}
