using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Striker : MonoBehaviour
{
    Rigidbody2D rigidbody;
    Transform selftrans;
    Vector2 startPos; // Vector pos for 2D objects
    public Slider slider;
    Vector2 direction;
    Vector3 mousePos;
    Vector3 mousePos2;
    public LineRenderer line;
    bool hasStriked = false;
    bool positionSet = false;
    public GameObject board, strikerAI;
    public int playerPoint = 0;
    public bool playerHasScored = false;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the game object
        selftrans = transform; // Get the Transform component attached to the game object
        startPos = transform.position; // Store the initial position of the striker
    }

    public void shoot()
    {
        float x = 0;
        if (positionSet && rigidbody.velocity.magnitude == 0)
        {
            x = Vector2.Distance(transform.position, mousePos); // Calculate the distance between the striker and the mouse position
        }
        direction = (Vector2)(mousePos2 - transform.position); // Calculate the direction of shooting
        direction.Normalize(); // Normalize the direction vector
        rigidbody.AddForce(direction * x * 400); // Add force to the striker in the given direction with a magnitude based on the distance
        hasStriked = true;
        playerHasScored = false;
    }

    void Update()
    {
        line.enabled = false; // Disable the LineRenderer component

        // Convert the mouse position to world coordinates
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the inverse mouse position
        Vector3 inverseMousePos = new Vector3(Screen.width - Input.mousePosition.x, Screen.height - Input.mousePosition.y, Input.mousePosition.z);

        // Convert the inverse mouse position to world coordinates
        mousePos2 = Camera.main.ScreenToWorldPoint(inverseMousePos);
        mousePos2.y = mousePos2.y - 3;

        if (selftrans.position.x != 0)
        {
            mousePos2.x = mousePos2.x + (selftrans.position.x * 2); // Adjust the x-coordinate of the mouse position based on the striker's x-coordinate
        }

        // Clamp the mouse position within specific bounds
        if (mousePos2.y > 1.351466f)
        {
            mousePos2.y = 1.351466f;
        }
        if (mousePos2.x < -2.52f)
        {
            mousePos2.x = -2.52f;
        }
        if (mousePos2.y < -2.953f)
        {
            mousePos2.y = -2.953f;
        }
        if (mousePos2.x > 2.501031f)
        {
            mousePos2.x = 2.501031f;
        }

        if (!hasStriked && !positionSet)
        {
            selftrans.position = new Vector2(slider.value, startPos.y); // Move the striker horizontally based on the value of the slider
        }

        // Check if the left mouse button is released, the striker is not moving, and the position is set
        if (Input.GetMouseButtonUp(0) && rigidbody.velocity.magnitude == 0 && positionSet)
        {
            shoot(); // Perform the shooting action
        }

        // Cast a ray from the camera to the mouse position and check if it hits any collider
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!positionSet)
                {
                    positionSet = true; // Set the position of the striker if it's not already set
                }
            }
        }

        if (positionSet && rigidbody.velocity.magnitude == 0)
        {
            line.enabled = true; // Enable the LineRenderer component
            line.SetPosition(0, selftrans.position); // Set the first point of the line to the striker's position
            line.SetPosition(1, mousePos2); // Set the second point of the line to the adjusted mouse position
        }

        if (rigidbody.velocity.magnitude < 0.1f && rigidbody.velocity.magnitude != 0 && hasStriked)
        {
            Invoke("strikerReset", 2f); // Reset the striker after a delay
            hasStriked = false;
        }
    }

    public void strikerReset()
    {
        if (!playerHasScored)
        {
            board.GetComponent<gameManager>().count++; // Increment the count variable in the gameManager script
        }

        rigidbody.velocity = Vector2.zero; // Set the velocity of the striker to zero
        positionSet = false; // Reset the position set flag
        strikerAI.GetComponent<AIController>().MakeMove(); // Call the MakeMove method of the AIController script attached to the strikerAI game object
    }
}
