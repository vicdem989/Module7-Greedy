Implementing LARGE and XL_LARGE was something I for some reason did not manage to get done. I am close with LARGE, with a small bug. But I do think I was on the right way. Also, I did Console.WindowWidth / 100 * 75 because I could not get the float to work. I would prefer to have done Console.WindowWidth * 0.75. Large is just large, kinda large tbh.

Multilingual support utilizes a config file where the language is saved locally. Meaning, if the player chooses english as language. The language will stay english until it is changed to norwegian. Pretty neat if u ask me

Using timer is not something new to me. But in c#, it was a different beast. A timer is usually an easy task. I got to show the time of day, with the increments by 10 minutes. However, I wanted it to show the seconds that the player has played the current game. Something I was not allowed to in the TimerCallback().

Fixing the bug where the player can go outside of the map, leading to a crash, was something I could not figure out. It sounds simple, you find the functions where moves are handled, meaning SetMoves() and DoMoves(). Then set in an if(player exeeds array length(map length)) or something similar. I also couldn't undestand how you would check if the spots in the direction you want to move is empty or not. Probably something like if(delta_x > 0) check left and right, else if(delta_y > 0) check up and down. 

Since I did not manage to do the requirements, I did not have time to even think about the higher grade ones.
