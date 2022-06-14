using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirbalController : MonoBehaviour
{
    ScoreDisplay scoreDisplay;
    protected Rigidbody rb;

    protected float speed = 700f;
    private float pushForce = 2.0f; 
    public int candyCollected;

    void Start()
    {
        scoreDisplay = GameObject.Find("EventSystem").GetComponent<ScoreDisplay>();
        rb = GetComponent<Rigidbody>();
    }

    protected void MoveBirbal(float hInput, float vInput)
    {
        rb.AddForce(new Vector3(hInput, 0, vInput) * speed * Time.deltaTime, ForceMode.Acceleration);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            candyCollected++;
            other.gameObject.SetActive(false);
            // Debug.Log("Candy collected: " + candyCollected);
            scoreDisplay.UpdateScore(name, candyCollected);
            // Debug.Log("Did anything happen?" + name + candyCollected);
            if (candyCollected >= 5) 
            {
                GameObject.Find("Game Manager").GetComponent<GameManager>().InitialiseGameOver(gameObject.name);
            }
        }
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Birbal"))
        {
            // Calculate push forces and direction
            Rigidbody collisionRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.transform.position - transform.position;
            Vector3 awayFromRival = transform.position - collision.transform.position;

            // Apply forces
            collisionRb.AddForce(awayFromPlayer * pushForce, ForceMode.Impulse);
            rb.AddForce(awayFromRival * pushForce, ForceMode.Impulse);
            Debug.Log("Collision! Push force = " + pushForce);

            // Lose candy if available
            if (candyCollected > 0)
            {
                candyCollected--;
            }

            // Tell UI System to update score
            scoreDisplay.UpdateScore(name, candyCollected);
        }
    }
}

// backlog:
// fix motion so that birbals can only spin sideways or tumble forward
// add momentum which builds up as the birbals continue to move
// spawn candy after bumping