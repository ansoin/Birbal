using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BirbalController
{
    void Update()
    {
        if (GameObject.Find("Game Manager").GetComponent<GameManager>().isGameActive)
        {
            MovePlayer();
        }
    }

    void MovePlayer() // Control movement
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        MoveBirbal(hInput, vInput); // Send instructions to movement controller in parent class
    }

}