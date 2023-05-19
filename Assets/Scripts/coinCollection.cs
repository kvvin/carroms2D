using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coinCollection : MonoBehaviour
{
    public static coinCollection Instance; // Singleton instance

    public Text playerPointsText; // Text component to display player points
    public Text aiPointsText; // Text component to display AI points

    public AIController aiController; // Reference to the AIController script
    public Striker striker;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateScoreTextP();
        UpdateScoreTextA();
    }

    // Update the player points text with the current player's score
    private void UpdateScoreTextP()
    {
        playerPointsText.text = striker.playerPoint.ToString();
    }

    // Update the AI points text with the current AI's score
    private void UpdateScoreTextA()
    {
        aiPointsText.text = aiController.aiPoint.ToString();
    }

    // Increment the player's score by the specified number of points and update the score text
    public void IncrementPlayerPoint(int points)
    {
        striker.playerPoint += points;
        UpdateScoreTextP();
    }

    // Increment the AI's score by the specified number of points and update the score text
    public void IncrementAIPoint(int points)
    {
        aiController.aiPoint += points;
        UpdateScoreTextA();
    }

    // Handle collision events when the player or AI striker collides with coins or the queen
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("coins") && !aiController.isMyTurn2)
        {
            striker.playerHasScored = true;
            Destroy(collision.gameObject);
            IncrementPlayerPoint(1); // Increment player score by 1 for collecting a coin
        }
        else if (collision.gameObject.CompareTag("queen") && !aiController.isMyTurn2)
        {
            striker.playerHasScored = true;
            Destroy(collision.gameObject);
            IncrementPlayerPoint(2); // Increment player score by 2 for collecting the queen
        }
        else if (collision.gameObject.CompareTag("coins") && aiController.isMyTurn2)
        {
            aiController.AiHasScored = true;
            Destroy(collision.gameObject);
            IncrementAIPoint(1); // Increment AI score by 1 for collecting a coin
        }
        else if (collision.gameObject.CompareTag("queen") && aiController.isMyTurn2)
        {
            aiController.AiHasScored = true;
            Destroy(collision.gameObject);
            IncrementAIPoint(2); // Increment AI score by 2 for collecting the queen
        }
    }
}
