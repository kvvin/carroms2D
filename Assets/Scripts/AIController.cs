using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public GameObject[] targetCoins; // The targets to shoot at
    public GameObject board;
    public float shootingForce = 200f; // The force applied to shoot the targets
    public float minShootDelay = 1f; // Minimum delay between each shot
    public float maxShootDelay = 3f; // Maximum delay between each shot
    public float minYPos = 1.223f; // Minimum y position of the AI striker
    public float maxYPos = 1.256f; // Maximum y position of the AI striker

    public bool isMyTurn = false, isMyTurn2 = false; // Indicates if it's the AI's turn to shoot
    private float shootDelayTimer = 0f; // Timer for delay between shots

    private Rigidbody2D rb; // Reference to the AI striker's Rigidbody2D component
    public GameObject objectToMove; // The object whose position will be changed
    public Vector3 newPosition;
    public int aiPoint = 0;
    public bool hasStriked = false;

    public bool AiHasScored = false;
    public bool dirAllow;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the AI striker
    }

    void Update()
    {
        if (isMyTurn)
        {
            shootDelayTimer -= Time.deltaTime; // Update the shoot delay timer

            if (shootDelayTimer <= 0f)
            {
                isMyTurn = false; // It's no longer the AI's turn to shoot
                dirAllow = true;
                StartCoroutine(settupPos()); // Set up the AI's position and shoot

                shootDelayTimer = Random.Range(minShootDelay, maxShootDelay); // Set a random delay for the next shot
            }
        }

        if (rb.velocity.magnitude < 0.1f && rb.velocity.magnitude != 0 && hasStriked)
        {
            Invoke("reset", 2f); // Reset the AI striker's state after a delay
            hasStriked = false;
        }
    }

    public IEnumerator settupPos()
    {
        float randomX = Random.Range(-1.24f, 1.36f); // Select a random x-coordinate for the AI striker's position

        transform.position = new Vector3(randomX, transform.position.y, transform.position.z); // Set the AI striker's position
        dirAllow = false;

        yield return new WaitForSeconds(2.0f); // Wait for 2 seconds before shooting
        Shoot(); // Shoot at the target
    }

    public void MakeMove()
    {
        isMyTurn = true; // Set it to the AI's turn to shoot
        isMyTurn2 = true;
        shootDelayTimer = Random.Range(minShootDelay, maxShootDelay); // Set a random delay for the first shot
    }

    private void Shoot()
    {
        if (targetCoins.Length > 0)
        {
            hasStriked = true;
            AiHasScored = false;

            int randomIndex = Random.Range(0, targetCoins.Length); // Select a random target coin
            Transform selectedCoin = targetCoins[randomIndex].transform;

            Vector2 direction = selectedCoin.position - transform.position; // Calculate the direction to the target
            direction.Normalize(); // Normalize the direction vector

            rb.AddForce(direction * shootingForce); // Apply force to shoot the target
        }
    }

    public void reset()
    {
        if (!AiHasScored)
        {
            board.GetComponent<gameManager>().count++; // Increment the count variable in the gameManager script
        }

        rb.velocity = Vector2.zero; // Set the velocity of the AI striker to zero
        isMyTurn2 = false;
        objectToMove.transform.position = newPosition; // Reset the position of the objectToMove to the specified newPosition
    }
}
