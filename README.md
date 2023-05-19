# carroms2D
 Single player carrom game made for Nudge
 By Kevin Manjooran
1.	Arrange the assets into Unity:
•	Import the necessary assets such as carrom board, striker, coins, UI elements, etc., into your Unity project.
•	Place the assets appropriately to create the carrom game scene.
2.	Create the Player Striker script:
•	Create a new script called "Striker" and attach it to the player striker GameObject.
•	Declare the required variables to store references and track game state.
•	Implement the necessary methods for shooting the striker, updating its position, and handling collisions.
•	Use rigidbody physics and mouse input to control the striker's movement and shooting direction.
3.	Implement the AI Striker script:
•	Create a new script called "AIController" and attach it to the AI striker GameObject.
•	Define variables to manage shooting behavior, shooting delay, target coins, and shooting force.
•	Implement the logic to make the AI striker take turns shooting at random targets.
•	Use physics forces to shoot the striker towards the target coins.
4.	Add the Line Renderer for the shooting direction:
•	Attach a Line Renderer component to the player striker GameObject.
•	Enable and disable the Line Renderer based on game conditions.
•	Set the Line Renderer positions to visualize the shooting direction.
5.	Introduce the Timer:
•	Create a new script called "Timer" and attach it to a Timer GameObject.
•	Set the total game time in seconds and a reference to the UI text displaying the timer.
•	Implement a countdown mechanism using a coroutine to update the timer text and handle game over logic.
6.	Create the Coin Collection script:
•	Create a new script called "coinCollection" and attach it to a GameManager GameObject.
•	Define variables to track player and AI points, and references to the AIController and Striker scripts.
•	Implement methods to increment points, update the score UI text, and handle coin and queen collisions.
•	Attach triggers to the coins and queen objects and detect collisions with the striker to update the points.
7.	Implement the GameManager script:
•	Create a new script called "gameManager" and attach it to a GameManager GameObject.
•	Declare variables for tracking game state and references to game objects.
•	Implement the logic to alternate between player and AI turns by enabling and disabling game objects.
8.	Set up the UI:
•	Create a Canvas GameObject and add UI elements like text components for player points, AI points, and the timer.
•	Associate the UI elements with the appropriate scripts to update their values during gameplay.
9.	Adjust and Fine-tune:
•	Test and adjust various parameters such as shooting force, delays, striker movement, collider sizes, etc.
•	Iterate and refine the gameplay experience by making changes based on testing and feedback.
Explanation of Scripts:
1.	gameManager
This script represents the game manager for a carrom game. It keeps track of the turns and updates the turn indicators accordingly. Here's a breakdown of the script:
•	The count variable is used to keep track of the turns. It starts with a value of 0.
•	The x and y variables are GameObjects representing the turn indicators for player X and player Y, respectively.
•	The Start() function is called at the start of the game. It can be used for any initialization code but is empty in this case.
•	The Update() function is called once per frame. It checks the value of count to determine which player's turn it is.
•	If count is divisible evenly by 2 (i.e., an even number), it means it's player X's turn. In this case, it activates the x GameObject and deactivates the y GameObject.
•	If count is not divisible evenly by 2 (i.e., an odd number), it means it's player Y's turn. In this case, it deactivates the x GameObject and activates the y GameObject.
•	This script is responsible for managing the turn indicators and keeping track of the players' turns during the carrom game.
2.	Timer
This script represents a timer for a game with a specific duration. It counts down from the given gameTimeInSeconds value and updates a UI text element (timerText) to display the remaining time. Here's a breakdown of the script:
•	The gameTimeInSeconds variable holds the total duration of the game in seconds.
•	The timerText variable is a reference to the UI text component that will display the timer.
•	The currentTime variable holds the current time remaining in the game.
•	In the Start() function, the currentTime is set to the initial game time, and the Countdown() coroutine is started using StartCoroutine().
•	The Countdown() coroutine runs continuously while currentTime is greater than 0.
•	Inside the loop, the timerText is updated with the current time using the FormatTime() function.
•	The currentTime is decreased by 1 second.
•	The coroutine pauses execution for 1 second using yield return new WaitForSeconds(1f) before continuing with the next iteration of the loop.
•	Once the countdown finishes (i.e., currentTime reaches 0), the game over logic is executed.
•	In this example, the scene with the name "gameOver" is loaded using SceneManager.LoadScene(). Replace "gameOver" with the name of your desired game over scene.
•	The FormatTime() function takes a time value in seconds and converts it to a formatted string in the "mm:ss" format.
•	The minutes and seconds components are extracted from the time value using Mathf.FloorToInt().
•	The string.Format() function is used to create a formatted string with leading zeros for minutes and seconds.
This script can be used to implement a countdown timer in a game and perform game over logic when the timer reaches 0.
3.	Striker 
This script represents a Striker in a game. It allows the player to control the striker's movement and shooting. Some of the major functionalities and variables are:
•	rigidbody: Rigidbody2D component attached to the game object representing the striker.
•	selftrans: Transform component attached to the game object representing the striker.
•	startPos: The initial position of the striker.
•	slider: Slider UI element used for controlling the striker's horizontal movement.
•	mousePos and mousePos2: Mouse positions in world coordinates used for shooting calculations.
•	line: LineRenderer component used for visualizing the shooting direction.
•	hasStriked: Flag indicating if the striker has performed a shot.
•	positionSet: Flag indicating if the striker's position has been set.
•	board and strikerAI: References to the gameManager and AIController game objects, respectively.
•	shoot(): Method for performing the shooting action of the striker.
•	Update(): Handles input, updates the striker's position and shooting visuals, and triggers the shooting action.
•	strikerReset(): Resets the striker's state, increments the count variable in gameManager, and calls the MakeMove method of the AIController.
4.	AiController
This script represents an AI controller for the game. It controls the behavior of the AI striker, including shooting at target coins. Some of the major functionalities and variables are:
•	targetCoins: Array of target coins for the AI striker to shoot at.
•	board: Reference to the gameManager game object.
•	shootingForce: The force applied to shoot the target coins.
•	minShootDelay and maxShootDelay: Minimum and maximum delays between each shot by the AI striker.
•	minYPos and maxYPos: Minimum and maximum y positions of the AI striker.
•	isMyTurn and isMyTurn2: Flags indicating if it's the AI's turn to shoot.
•	shootDelayTimer: Timer for the delay between shots.
•	rb: Reference to the Rigidbody2D component attached to the AI striker.
•	objectToMove and newPosition: Object whose position will be changed and the new position to set.
•	aiPoint: The AI's score.
•	hasStriked and AiHasScored: Flags indicating if the AI striker has performed a shot or scored a point.
•	dirAllow: Flag indicating if the direction is allowed for shooting.
The script includes methods to shoot at the target, set up the AI striker's position, make a move for the AI, and reset the AI striker's state.
5.	coinCollection
This script handles the collection of coins and scoring points for the player and AI in the game. Some key elements of the script are:
•	Instance: Singleton instance of the coinCollection script.
•	playerPointsText and aiPointsText: Text components to display the player and AI points, respectively.
•	aiController: Reference to the AIController script.
•	striker: Reference to the Striker script.
•	UpdateScoreTextP(): Updates the player points text with the current player's score.
•	UpdateScoreTextA(): Updates the AI points text with the current AI's score.
•	IncrementPlayerPoint(): Increments the player's score by the specified number of points and updates the score text.
•	IncrementAIPoint(): Increments the AI's score by the specified number of points and updates the score text.
•	OnTriggerEnter2D(): Handles the collision events when the player or AI striker collides with coins or the queen. It updates the scores based on the collision tags and destroys the collided objects.
Overall, this script manages the scoring system and updates the UI to reflect the current scores of the player and AI.

