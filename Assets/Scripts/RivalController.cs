using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalController : BirbalController
{
    private Transform target; // Closest candy
    private float delay = 0.5f; // Time before identifying new candy

    void Update()
    {
        if (GameObject.Find("Game Manager").GetComponent<GameManager>().isGameActive)
        {
            // Find target, chase found target
            if (target == null || !target.gameObject.activeInHierarchy)
            {
                StartCoroutine(GetTargetDelay());
            }
            if (target && target.gameObject.activeInHierarchy)
            {
                MoveRivalBirbal();
            }
        }
    }

    // Move towards declared target (nearest candy)
    private void MoveRivalBirbal()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        rb.AddForce(direction * speed * Time.deltaTime);
    }

    // Wait before picking target candy
    public IEnumerator GetTargetDelay()
    {
        yield return new WaitForSeconds(delay);
        target = GetNearestCandy();
    }

    // Return the closest active candy as the Transform target
    private Transform GetNearestCandy()
    {
        // Put all candies in an array
        GameObject[] allCandies = GameObject.FindGameObjectsWithTag("Pickup");
        Transform bestTarget = null;
        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < allCandies.Length; i++)
        {
                float distance = Vector3.Distance(allCandies[i].transform.position, transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    bestTarget = allCandies[i].transform;
                }
            }
        return bestTarget;
    }
}


// Make GetTargetDelay more secure