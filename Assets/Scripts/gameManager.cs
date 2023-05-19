using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public int count = 0; // Counter to keep track of turns
    public GameObject x; // GameObject to represent player X's turn
    public GameObject y; // GameObject to represent player Y's turn

    void Start()
    {
        
    }

    void Update()
    {
        if (count % 2 == 0)
        {
            x.SetActive(true); // Activate player X's turn indicator
            y.SetActive(false); // Deactivate player Y's turn indicator

        }
        else
        {
            x.SetActive(false); // Deactivate player X's turn indicator
            y.SetActive(true); // Activate player Y's turn indicator

        }
    }
}
