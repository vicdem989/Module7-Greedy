Implementing LARGE and XL_LARGE was something I for some reason did not manage to get done. I am close with LARGE, with a small bug. But I do think I was on the right way. Also, I did Console.WindowWidth / 100 * 75 because I could not get the float to work. I would prefer to have done Console.WindowWidth * 0.75. Large is just large, kinda large tbh.

Multilingual support utilizes a config file where the language is saved locally. Meaning, if the player chooses english as language. The language will stay english until it is changed to norwegian. Pretty neat if u ask me

Using timer is not something new to me. But in c#, it was a different beast. A timer is usually an easy task. I got to show the time of day, with the increments by 10 minutes. However, I wanted it to show the seconds that the player has played the current game. Something I was not allowed to in the TimerCallback().



- Multilingual Support: Extend your program to support multiple languages.
# DONE - There is a bug in the code that places the player on the board. Find and fix. 
# Create a gameover screen that summarizes the players achivment (this new screen should redirect to the menu in some whay when the player presses anny key)
# DONE - Currently the player can backtrack, i.e. go where they have been befor, this is not allowed. If this happens the game is over. 
# DONE- Currently the game only suports vertical movment, add the code that is required for hotisontal movment.
- Currently the player can atampt to go outside the bounds of the game board, if this happens the game crashes. Make it so that it is simply game over. NB! being next to a boarder for instance the left edge and selecting left should do nothing. i.e. there needs to be a number to the left in order to go left and fail. 
# The greedy games Update function has a bit of duplication, refactor the code to avoid as much of this duplication as possible.
- Implement LARGE and XL_LARGE game modes.
- HUD update: Show time in mm:ss (minutes and secods) of play.
# DONE - HUD update: Show the percentage of max score achived
# DONE - If the player presses "r", restart the level.
# DONE - If the player presses "q", exit to menu.

### Challenge Requirements (Higher Grades):

- If the players have noe more legal moves, the game shoudld go to gameover.

- Each game mode (s,l,xl) should be possible to combine with a challenge mode time triale. i.e the player has a set amunt of time. Note that the player should still be able to play in normal mode. 

- Implement a High Score System: From the main menu it should be possible to go into a higscore view and see previous results. Remember that scores from diffrent game modes can not be directly compared. After having played the player should be able to save their initals and score.

- Create a game mode based on XL that has "death" spaces (holes) in the map. Thes spaces should at minimum be 2x2 (2 rows and 2 columns)

Throughout this module, you should keep in mind that the ultimate goal is not only to build these features but also to learn from the process. You should aim to understand how different parts of a program interact, how to troubleshoot effectively, and how to write clean, effective code.

Evryone: In your README file write about what you learnt from this module.

All your code and related files should be neatly organized in a Zip file, with the following internal structure

module_7_Greedy  
├── .vscode  
├── module_7_hangman.csproj  
├── module_7_hangman.sln  
├── \*.\*  
└── README.md  

About assesment:

Assessment for this project will be based not just on feature completion, but also on your attention to detail, the cleanliness and readability of your code, and the thoughtfulness of your README file reflections. This is your opportunity to demonstrate not just what you've learned, but how you can apply it creatively and effectively in a real programming project.


