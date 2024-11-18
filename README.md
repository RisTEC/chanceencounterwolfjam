# Alone? in the Dark
This project was made by Brandon Jeon, Rishit Roshan, and Nick Will

This game, developed during the Wolf Jaw Studio's Wolfjam, revolves around the theme of connection. The project collects data on people's experiences with loneliness through a web app and integrates that data into the game, where these personal stories are brought to life and displayed within the gameplay environment. 

How To Use:

Access the web application from a web browser on a device (preferably a laptop but not required) at https://chance-encounter-web-app-b8362524d7c2.herokuapp.com/

(You should see a webpage with the project title with a series of questions and a submit button at the bottom)

Answer the 4 questions according to your personal experience at whatever level of depth you feel comfortable.
Questions 1 and 3 are written response questions using text boxes that have a maximum of 300 characters.
Questions 2 and 4 require you to select an image of your choosing. You will know that you have selected an image because it will be highlighted.

Press the “Submit” button at the bottom of the page.
If the submission succeeds, you will see a green message below the button that indicates a successful submission and directs you to the Unity game.
If any of the 4 questions are unanswered, you will see a red error message indicating that all 4 questions need a response.
If there is an error with the server, you will see a red message that indicates so and instructs you to contact the developers.

Access the Unity game as the WebGL build on GitHub pages: https://ristec.github.io/chanceencounterwolfjam/
The game will automatically load and show the necessary intro screens.
Turn on your system audio to hear the game’s background track.

You will know that the game has loaded into the combat phase when the player and enemy sprites are visible in a battle scene and the dialog box prompts you to “choose an action”.
The enemy and its information will be generated using random user input from the server.
The user input that generated the enemy will be briefly displayed in the dialog box before switching to the action options.

Select the option to attack, heal, or guard. The action will be reflected via text and a change in health bar, and then the enemy will automatically perform an action in response.
“Attack” will do damage to the enemy, “heal” will restore player health, and “guard” will reduce incoming damage by 50%.
Note: The action options have a percentage value associated with them that indicates the stamina cost. Your stamina is indicated by a pink number under the player name. 
Your stamina will increase after each turn. This increase gets larger as the game progresses, starting at a base value of 10%.
If you try to perform an action that you do not have enough stamina for, nothing will happen, and you will have to select a different action.

Continue this turn cycle until the enemy has been defeated. Another enemy will spawn right away, and the same turn cycle will commence.

For every 3rd enemy you defeat, a dialog message will appear indicating that you have received a power-up that was generated using a random user’s input to the server.
The flame power-up applies a permanent increase in player damage output between 5-10%.
The heart power-up applies an increase to the player’s health by 5-10%.
The star power-up applies an increase to the player’s stamina by 5-10%.
The user input that generated the power-up will be briefly displayed in the dialog box before displaying the power-up symbol and description, then switching back to the action options.

This battle cycle continues indefinitely to reflect ongoing appearances of these feelings in one’s life, with high and low points reflected by the player health bar.
If the player somehow exhausts all options in the server’s data set, the game will recycle previous data.

 The user and others can continue to use the web app and game to engage with the experience and connect with others.

 Links:
Repo Links:
Web App Repo: https://github.com/ndw1117/chance-encounter_connection-portal 
Unity Game Repo: https://github.com/RisTEC/chanceencounterwolfjam/tree/main 

Asset Credits:

Heart Icon - Goblin Mode Games https://goblin-mode-games.itch.io/pixel-art-heart-capsules
Star Icon - Narik https://soulofkiran.itch.io/pixel-art-animated-star
Flame Icon - Kai Gidwani
Player and enemy assets are modifications of Szadi art’s asset performed by the team member Rishit Roshan. https://szadiart.itch.io/rpg-main-character
Game background track is “Broken Inside” by David Robson. https://www.fesliyanstudios.com/royalty-free-music/download/broken-inside/3314

Other Links:
https://github.com/RisTEC/chanceencounterwolfjam/tree/main



