# RoboFroggy

## Summary
RoboFroggy is an action-packed 2D side-scrolling adventure! Take control of a robot ninja frog as you navigate through a series of treacherous levels teeming with deadly traps and formidable obstacles. Brace yourself for menacing spikes and sawblades, and treacherous moving platforms. Stay on your toes as spike ball shooters, massive stone contraptions armed with deadly projectiles, try to thwart your progress. Master the art of precision and finesse with the game's fluid movement mechanics, allowing you to execute double jumps, wall jumps, and dashes. Try utilizing the unique "pogo" ability to bounce off different perilous obstacles. Only the most skilled players will be able to conquer RoboFroggy's daunting trials and emerge victorious.

## Gameplay Explanation ##
In RoboFroggy, the objective is simple: guide the robot ninja frog to reach the flag and advance to the next level. However, prepare to face a relentless barrage of dangerous obstacles and traps. To assist you on this adventure, we have equipped you with a range of movement mechanics that will help you traverse each level.

**Moving left and right**: Utilize the left and right arrow keys or the 'a' and 'd' keys to control the lateral movement of the ninja frog.

**Jump**: Press the space bar to jump. Remember, the duration you hold the space bar determines the height of your jump.

**Double Jump**: Take advantage of the ninja frog's ability to execute a second jump while airborne. After initiating the first jump, tap the space bar again to perform a double jump.

**Wall Slide**: When approaching a wall, your ninja frog can slide down its surface and slow its descent. This maneuver allows for a controlled descent and buys you time to plan your next move.

**Wall Jump**: While executing a wall slide, press the space bar to wall jump and gain extra height or distance.

**Dash**: Activate the dash ability by pressing the shift key. Experience a burst of horizontal velocity, allowing you to traverse hazardous gaps or evade fast-moving obstacles. 

**Pogo**: When confronted with landing on an obstacle during mid-air descent, invoke the pogo move by quickly pressing the left mouse button. This ability enables you to rebound off the obstacle and avoid taking damage.

Remember, success in RoboFroggy lies in mastering these movement mechanics. Strategize, and combine these abilities to overcome each level's challenges. Good luck and have fun!

# Main Roles #

Your goal is to relate the work of your role and sub-role in terms of the content of the course. Please look at the role sections below for specific instructions for each role.

Below is a template for you to highlight items of your work. These provide the evidence needed for your work to be evaluated. Try to have at least 4 such descriptions. They will be assessed on the quality of the underlying system and how they are linked to course content. 

*Short Description* - Long description of your work item that includes how it is relevant to topics discussed in class. [link to evidence in your repository](https://github.com/dr-jam/ECS189L/edit/project-description/ProjectDocumentTemplate.md)

Here is an example:  
*Procedural Terrain* - The background of the game consists of procedurally-generated terrain that is produced with Perlin noise. This terrain can be modified by the game at run-time via a call to its script methods. The intent is to allow the player to modify the terrain. This system is based on the component design pattern and the procedural content generation portions of the course. [The PCG terrain generation script](https://github.com/dr-jam/CameraControlExercise/blob/513b927e87fc686fe627bf7d4ff6ff841cf34e9f/Obscura/Assets/Scripts/TerrainGenerator.cs#L6).

You should replay any **bold text** with your relevant information. Liberally use the template when necessary and appropriate.

## User Interface
### Joyce

*Goals* - My main goal for the user interface was to make sure that everything in the game matched in terms of theme. I created UI components that matched the assets and vibe of the game, from the color palette to the pixelation. I didn't want things like the main menu and the buttons to take the player away from the game, and I felt that a matching user interface was the best way to keep a player immersed in the game. 

*Health Bar* - The main component of the heads-up display is the health meter. Our team decided that it would be best to give the player three lives to give them some leeway when navigating through each level. Every time the player is injured, they will lose one of the three lives, which are represented by hearts. To show the user how many hearts they were missing, I wanted to include some transparent hearts that would replace a lost heart. This way the user would always remember how many lives they started out with.

![HealthBar](https://github.com/ian-chuang/RoboFroggy/blob/joyce/media/HealthBar.png)

*Main Menu* - The main menu, or start page, is the first thing a user sees, so it is very important to create an eye-catching first impression. The start page is comprised of the main menu as well as decorative sneak peeks for the user to see what is coming next if they choose to click Play. The pixelated UI asset can be seen flanked by two Froggy characters. The background is the tutorial terrain background, which I chose because I didn't want to give to much away to the player.

![StartPage](https://github.com/ian-chuang/RoboFroggy/blob/joyce/media/StartPage.png)


## Movement/Physics

### Ian

*Physics Environment* - For managing the physics of player gravity and collisions, I utilized RigidBody 2D and Collider 2D. The player has a rigid body giving it physics properties for gravity and has a combination of a box and capsule collider for simulating contacts with the environment. The level terrain has a Tilemap Collider and most of the traps like the saws, moving platforms, and spike ball shooter have a collider so that the player will interact with them. The player has the "Slippery" material making it have no friction with the environment to prevent unwanted friction forces. However, the level terrain still has friction so that spike balls will roll on the ground. 

*Moving Left and Right* - Moving left and right is fairly simple and I referenced this [piece of code I found here](https://gist.github.com/bendux/b6d7745ad66b3d48ef197a9d261dc8f6#file-playermovement-cs-L53). The way it works is that it gets the horizontal axis input from the user (either left and right arrows or 'a' and 'd' keys) and then sets the player's velocity to match the corresponding lateral direction. 

![MoveHorizontally](https://github.com/ian-chuang/RoboFroggy/blob/main/media/MoveHorizontally.gif)

*Jumping* - To make the player jump is fairly simple and just requires setting an upward velocity for the player when the user presses the "Jump" input. However, you must also check that the player is touching the ground before they can jump. To do this, I [referenced this video](https://www.youtube.com/watch?v=FXpUb-H54Oc) and added a small visual rectangle that was located around the player's feet. Then, I could call the function "Physics2D.OverlapBox()" and check if the visual rectangle overlapped with the terrain layer to tell if the player was touching the ground. This way the player would only jump when grounded.

![JumpCut](https://github.com/ian-chuang/RoboFroggy/blob/main/media/JumpCut.gif)

*Double Jump* - To implement double jumping, I referenced the [code I found in this video](https://www.youtube.com/watch?v=QGDeafTx5ug). The way the code works is that you have a jump counter that keeps track of how many extra jumps you have. The player is then set to have an extra jump anytime it is detected to be grounded. If a player jumps while they are grounded we won't decrement the jump counter. However, if a player isn't grounded and the jump counter is greater than 0, they are allowed to double jump. The double jump is implemented the same as a jump where we set an upwards velocity on the player.

![DoubleJump](https://github.com/ian-chuang/RoboFroggy/blob/main/media/DoubleJump.gif)

*Wall Slide* - To implement wall sliding, I referenced the [code found here](https://gist.github.com/bendux/b6d7745ad66b3d48ef197a9d261dc8f6). Wall sliding requires a visual rectangle similar to the check if the grounded visual rectangle. This rectangle is placed to the side of the player in the direction it is facing. This way whenever the player is touching and facing a wall, I can cal "Physics2D.OverlapBox()" with the visual rectangle and the terrain layer. If a player was detected to be wall sliding, I coded it so that the minimum velocity of the player would be clamped, slowing down the descent of the player to a fixed speed, referencing [this code here](https://www.youtube.com/watch?v=O6VX6Ro7EtA). This simulated the player clinging to the wall and bracing their fall.

![WallSlide](https://github.com/ian-chuang/RoboFroggy/blob/main/media/WallSlide.gif)

*Wall Jump* - To implement wall jumping, I referenced the [code found here](https://gist.github.com/bendux/b6d7745ad66b3d48ef197a9d261dc8f6). The way the code works is that if you detect you are wall sliding and the user presses the jump input, the player will jump upwards and away from the wall. I do this by setting the velocity of the player to be upwards and in the opposite direction the player is facing. I will also flip the direction of the player when they jump. Another important feature of the wall jump is that you want to force the player in the wall jump direction and prevent them from cancelling the jump. This makes the timing the jump more important and makes the jump feel more responsive and impactful. To do this we set a flag for when the player is wall jumping. While this flag is set, the player won't be able to move laterally. Then we reset this flag some duration after the wall jump so that the player can move normally.

![WallJump](https://github.com/ian-chuang/RoboFroggy/blob/main/media/WallJump.gif)

*Dash* - To implement the dash, I referenced the [code found here](https://gist.github.com/bendux/aa8f588b5123d75f07ca8e69388f40d9). The way the dash works is that when the player presses the "shift" key, we will fix the player's horizontal velocity to some large value in the direction the player is facing for some duration. While the player dashes, we prevent other movement like jumping and changing directions. We also add a cooldown duration that prevents the player from continuously dashing. To do this, we call a dash coroutine which will manage setting the player velocity, enabling and disabling the dash flag, and waiting for the desired durations required for the dash itself and the dash cooldown. Another special feature with dashing is the ability to dash while wall sliding. This is a special case where we must flip the direction of the player before they dash otherwise they will dash towards the wall.

![Dash](https://github.com/ian-chuang/RoboFroggy/blob/main/media/Dash.gif)

### Jacob

*Pogo Jump* - For the pogo jump, I was originally inspired by the similar ability in *Hollow Knight*, where the player can attack downwards, avoiding damage and receiving a boost if you successfully hit an enemy. But due to lack of animations, I think the final implementation is much more similar to the parry ability found in *Cuphead*. It works by spawning a Physics Overlap Box for a certain amount of time (configured in the Inspector) while simultaneously triggering a flip animation to communicate to the player that they initiated the pogo jump. If a "pogo-able" object collides with the Overlap Box, the player receives a quick boost of upward velocity. 

![Pogo](https://github.com/ian-chuang/RoboFroggy/blob/main/media/pogo.gif)

## Animation and Visuals

### Ian

*Level Visuals* - For level visuals, I used [assets found here](https://assetstore.unity.com/packages/2d/characters/pixel-adventure-1-155360). I used this [video here](https://www.youtube.com/watch?v=QkbGr1rAya8&list=PLrnPJCHvNZuCVTz6lvhR81nnaf1a-b67U&index=2) to figure out how to use the tile palette and draw the levels. For level visuals, I tried to mix up the looks for each level. The tutorial level has a very common looking color scheme similar to that of Super Mario Bros. Level 1 has a pink and orange color scheme, Level 2 has a white and brown color scheme, and Level 3 has a underground Super Mario theme to it. All levels have two tile map objects, one for the physical terrain that the player interacts with and another for the background which the player can't interact with.

*Designing Tutorial Level (Scene0)* - I created the tutorial level which teaches the user the different movement mechanics of the player. The tutorial level is easier than other levels and has text in the background with instructions on how to use each movement mechanic, from moving left and right, jumping, dashing, wall jumping, and double jumping. The tutorial also introduces all the traps in the game to familiarize the user. Something special about the tutorial level is that it places traps in a way that requires the user to use all movement mechanics before advancing. For example, the user must learn how to double jump to cross a tall spike barrier. 

![TutorialLevel](https://github.com/ian-chuang/RoboFroggy/blob/main/media/TutorialLevel.png)

*Designing Level 1 (Scene1)* - Level 1 is the start of a series of pretty challenging levels that test the user's skill and dexterity. Level 1 has a very tall design where a player must ascend a series of platforms in order to reach the flag at the top of the level. It first starts out very simple as the player needs to jump through a few platforms and avoid some spikes, a spike ball shooter, and a moving saw. The level becomes more complicated as the player must utilize wall sliding and wall jumping in order to ascend a narrow passage way filled with spikes and moving saws. Finally, the most challenging part of the level includes many small platforms, spikes, and spike ball shooters scattered throughout an area. The player must use precise movements to keep ascending the small platforms while avoiding many spike ball projectiles.

![Level1](https://github.com/ian-chuang/RoboFroggy/blob/main/media/Level1.png)

*Designing Level 2 (Scene2)* - Level 2's focus is more on utilizing moving platforms and using precise movements to avoid different spikes and saws. In this level, the player must move laterally to the right to reach a flag at the end of the level. However, there are very few static platforms in this level and the player must primarily use moving platforms in order to travel. These moving platforms are more difficult to land on and if a player messes up their landing, they risk falling off the level and dying.

![Level2](https://github.com/ian-chuang/RoboFroggy/blob/main/media/Level2.png)

*Designing Level 3 (Scene3)* - Unlike the last three levels, Level 3 is a closed environment with very narrow spaces that the player must traverse. I designed this level to be like a maze where the player can choose different paths to make it to the flag at the end. The narrow path design forces the player to come into close contact with different traps and makes this level the hardest. The narrow path also highlights the challenge of evading spike ball shooters and their spike balls. In this level I put many spike ball shooters whose spike balls will keep rolling throughout the pathways of this level, increasing the difficulty. 

![Level3](https://github.com/ian-chuang/RoboFroggy/blob/main/media/Level3.png)

*Designing Victory Level (Scene4)* - This level serves as a congratulations to the user for finishing the game. I added a flag to this level to give the user an option to play the game all over again if they wanted to. This level has no traps and is meant to feel rewarding and lift any stress the user felt while playing the game.

![VictoryLevel](https://github.com/ian-chuang/RoboFroggy/blob/main/media/VictoryLevel.png)

*Player Visuals and Animations* - For player visuals, I used [the Pixel Adventure 1 asset](https://assetstore.unity.com/packages/2d/characters/pixel-adventure-1-155360). I used this [video here](https://www.youtube.com/watch?v=GChUpPnOSkg&list=PLrnPJCHvNZuCVTz6lvhR81nnaf1a-b67U&index=4) to figure out how to use Animations and the Animator. The player animation state machine is pretty complicated and includes states for running, falling, jumping, double jumping, wall sliding, dashing, getting hit, and dying. There are eight different animations being used for the frog and are all from the Pixel Adventure 1 asset. In addition to player animation sprites, I also added a trail renderer that will be displayed whenever the player is dashing. This gives a nice responsive visual cue to the player that they initiated the dash.

![PlayerAnimator](https://github.com/ian-chuang/RoboFroggy/blob/main/media/PlayerAnimator.png)

*Trap/Obstacles Visuals and Animations* - For obstacle visuals, I used [the Pixel Adventure 1 asset](https://assetstore.unity.com/packages/2d/characters/pixel-adventure-1-155360). This helped provide me with the necessary sprites for spikes, spike balls, the spike ball shooter, moving saws, and moving platforms. Spikes and spike balls didn't require any animations. The moving saws and platforms required a single animation state for a rotating effect that gave them more life. Finally the spike ball shooter had two states, one which was an idle state where it would just blink and another that was triggered when it fired a spike ball. 

### Jacob

*Particle Effects* - I created a particle system following [this tutorial](https://www.youtube.com/watch?v=1CXVbCbqKyg). I configured the particles to look like a burst of dust in the inspector and then passed a reference to the particle system in the `PlayerMovement` script. I then triggered the particle system when the player switches directions, when they jump, and when they dash on the ground. Since the particle system is a child of the player prefab, the different positions and velocities of the player in each of these scenarios produces a unique effect each time the particle system plays.

![Particle1](https://github.com/ian-chuang/RoboFroggy/blob/main/media/particles1.gif)

![Particle2](https://github.com/ian-chuang/RoboFroggy/blob/main/media/particles2.gif)

## Input

### Ian

*Player Movement Input* - The default input is for the keyboard. I organized the inputs in the input manager so that they can be generalized to other platforms in the future. "Horizontal" is for the player's lateral movement and can be controlled with the left and right arrow keys. "Fire3" is for the player dash and can be controlled with the shift key. Finally "Jump" is for the player jump, double jump, and wall jump which can be controlled with the space bar. Inputs for RoboFroggy were designed to be simple and easy to learn.

### Christian
_Potential Controller Input_ - There were plans to make controller support, but due to time constraints, this was not a priority. The input manager would follow the same procedure that I had found [here](https://www.youtube.com/watch?v=p-3S73MaDP8&ab_channel=Brackeys).I would have binded _leftStickGamepad_ for the player's lateral movement. _buttonSouth_ would have been binded to jumping, and _buttonWest_ would have been binded to Dash. I think that the controller input should've been a higher priority due to the crisp feeling that is given to play a platformer with a controller.

## Game Logic

### Ian - Game Logic

*Spike Ball Shooter and Spike Ball Logic* - The code for the spike ball shooter was actually more complicated than imagined. The first step was rotating the spike ball shooter to follow the player. This involved finding the angle of the vector between the player transform and the spike ball shooter transform. I didn't want the spike ball shooter to always instaneously stick to the player's direction so I used Unity's RotateTowards() function which rotates the spike ball shooter with some rotational speed. The next step was firing spikeballs which would come out with a specified velocity in the direction the spike ball shooter was facing. This required some more math of calculating the spike ball's velocity vector and setting its magnitude. However, to fire a spike ball required that the spike ball come out of the spike ball shooter. This was a problem because both the spike ball and the spike ball shooter have a 2D collider so I needed to disable this collision. However, I didn't want to completely disable all collisions with either the spike ball or spike ball shooter since I wanted both game objects to be interactive with each other and the player. So what I did instead was code the spike ball shooter to disable collisions with itself and its fired spike ball for a brief duration. That way the spike ball would come cleanly out of the shooter and then I would reenable that collision so that the spike ball would continue interacting with the shooter in case they ever touched afterwards. Another problem with the spike ball was that if the shooter fired too many spike balls, over time there would be too many spike balls in the game which would ruin the experience. To resolve this problem I would remove every spike ball that was fired after some period. I also included a fade out effect that would decrease the alpha color value of the spike ball over time.

![SpikeBallShooter](https://github.com/ian-chuang/RoboFroggy/blob/main/media/SpikeBallShooter.gif)

*Sticky Platform Logic* - I referenced [this video here](https://www.youtube.com/watch?v=UlEE6wjWuCY&list=PLrnPJCHvNZuCVTz6lvhR81nnaf1a-b67U&index=9) to implement the logic that allows players to stick to a platform while it moves. The way it works is that I add a trigger collider at the top of the platform that will trigger whenever a player lands on it. Once triggered, the player's transform parent will be set to that of the platform so that the player will move along with the plaform. Once the player leaves the platform, I will reset its transform parent, "unsticking" it from the platform. 

![StickyPlatform](https://github.com/ian-chuang/RoboFroggy/blob/main/media/StickyPlatform.gif)

*Waypoint Follower Logic* - I referenced [this video here](https://www.youtube.com/watch?v=UlEE6wjWuCY&list=PLrnPJCHvNZuCVTz6lvhR81nnaf1a-b67U&index=9) to implement the logic that allows obstacles to move and follow a set of waypoints. To implement this I created a script that takes in an array of waypoints, which are just empty game objects with a desired transform. Then the script will control the moving saw or platform to move towards each waypoint one by one. Once the saw or platform has covered each waypoint, it goes back to the first waypoint and restarts.

![MovingSaw](https://github.com/ian-chuang/RoboFroggy/blob/main/media/MovingSaw.gif)

*Trap Collisions/Triggers and Player Hit Detection* - To implement hit detection, I added an OnTrigger to the PlayerMovement script that will detect for any collisions with game objects that have the tag "Trap". All the traps in this game, including saws, spikes, and spike balls have the tag "Trap" and a trigger collider. If a player gets hit, their health is decremented.

![Hurt](https://github.com/ian-chuang/RoboFroggy/blob/main/media/Hurt.gif)

*Death Mechanic* - If a player's health decremented all the way to zero, I would trigger a death function and animation that would reset the scene. However, I didn't want this transition to be immediate so I started a coroutine that would play the death animation and disable the PlayerController script for a specified duration before resetting the scene.

![Death](https://github.com/ian-chuang/RoboFroggy/blob/main/media/Death.gif)

*Camera Controller Logic* - I referenced [this video here](https://www.youtube.com/watch?v=Uv5tfMSKlnU&list=PLrnPJCHvNZuCVTz6lvhR81nnaf1a-b67U&index=3) to create a position locked camera that will lock its position to the player. The way it works is that it sets the transform of the camera to match that of the player. I also added a feature so that the camera won't go below a set minimum value. This is valuable because if a player falls off the edge of the level, we don't want the camera to continuously follow the player, but instead stop at some y-value to indicate to the user that the player has fallen off the screen.

*Out of Bounds Death/Reset* - When the player goes out of bounds and falls off the level, I don't want them to keep falling, but instead for them to die and reset the scene/level. I added a third tilemap collider behind "Background" and "Terrain" called "Death". This tilemap collider is very simple and is present in the Tutorial Level, Level 1, and Level 2. Essentially, it is a tilemap that covers the bottom of the level so that when the player collides with it, we can restart the scene.

![OutOfBounds](https://github.com/ian-chuang/RoboFroggy/blob/main/media/OutOfBounds.gif)

**Document what game states and game data you managed and what design patterns you used to complete your task.**

# Sub-Roles

## Cross-Platform

**Describe the platforms you targeted for your game release . For each, describe the process and unique actions taken for each platform. What obstacles did you overcome? What was easier than expected?**

## Joyce - Audio

*Theme Music* - The main theme for RoboFroggy is called "Music for Arcade Style Game" from [Pixabay](https://pixabay.com/music/video-games-music-for-arcade-style-game-146875/), a site for sharing stock images, videos, and music. The song's style is very upbeat and energetic to match the adventurous nature of the game, which subliminaly influences the user to move and react fast to complete each level. The theme music uses heavy synths to reflect the computerized and pixelized look of the game and has a simple tune to match the more elementary assets and background. Like the name promises, the song is perfect for an arcade-style game such as RoboFroggy. To create the best gameplay experience, the music plays from the beginning and loops with a minimal pause in between each play. There is a sound played to indicate the start and end of each level to indicate that gameplay has begun and that success has been reached, respectively. The licensing for the song is listed [here](https://pixabay.com/service/terms/), but in summary, it is able to be used for free and without crediting the artist.

*Sound Effects* -All sound effects were sourced from Mixkit's collection of [free sound effects](https://mixkit.co/free-sound-effects). The most important sound effects were the jump and button sound effects, as they were used the most throughout creating the game and they indicate to the user that there is a reaction to their input. I felt that it is important to have a sound effect for a button click to show that the user's input was logged, especially since there was sometimes a lag in loading different scenes. The licensing for these SFX clips from Mixkit state that they "are royalty-free and can be used without attribution" for "commercial and personal projects".

*Implementation* - To implement the music and sound effects, I referenced [this tutorial on YouTube](https://www.youtube.com/watch?v=JnbDxG04i7c). At first I attempted to mimic what was provided in the class exercises, but I ultimately decided that the implementation was too complicated for our needs. The tutorial provides a simple yet effective way to link certain movements and user inputs with the needed sounds.

**Describe the implementation of your audio system.**

**Document the sound style.** 

## Gameplay Testing

+[Link to findings](https://docs.google.com/document/d/1U1cBg2RcuDuVaV_p_CDA2pZ1C0IwrwMI_J3BfK9Wvdk/edit?usp=sharing)

Christian - I had interviewed a variety of people going from people who lots of experience playing games to people who do not play many games. There were also some people who only play console games.

- The UI will need to be updated heavily. In the earlier interviews, we didn't have a UI so lots of questions were asked about how many lives they had.
- Another point that will need to be updated is the lack of transparency in the tutorial level. There were many participants that didn't have a good experience due to the tutorial not explaining key factors such as holding spacebar to jump further and how wall jumping worked.

### Gameplay

- The gameplay may need some updating as there were some inconsistencies with the gameplay. One participant asked me how the gravity in the game worked as it felt very floaty and in the moon.
- Some mechanics may need to be more strict as well as we have found many bugs where the frog can wall jump off anything wall, including the spikeball shooter. Unsure if we want this to be a feature.
- Achievements and unlockables were something that we should include in the game. Due to the game being short, there's no incentive for replayability.
- One participant had fallen off the map and was felt like the game had froze. Perhaps adding a death screen would make the game more clear as to when you died.

### Input

- The input had a surprisingly good feedback, as most participants had enjoyed the butter smooth movement of RoboFroggy.
- A small gripe was that maybe the frog felt as if it was going too fast. This to the player making mistakes.

## Narrative Design

### Jacob
Unfortunately, due to time constraints, we weren't able to have much of a narrative. But [here is a link to the game design design document]([url](https://www.figma.com/file/KN2d0mevoSnULkFggCLRlO/Bunny-Game-Design-Doc?type=design&node-id=0%3A1&t=5E5ucLoHHEkzUZe8-1)) that shows what our game's narrative _would have_ been if we had more time.

## Press Kit and Trailer

### Jacob
I created a [trailer]([url](https://drive.google.com/file/d/1qDPHVOmNPxSrldNZmfdx_WIIqfr9TMZX/view?usp=sharing)) for RoboFroggy. Press kit coming soon...

**Describe how you showcased your work. How did you choose what to show in the trailer? Why did you choose your screenshots?**



## Game Feel

**Document what you added to and how you tweaked your game to improve its game feel.**

### Ian - Game Feel

*Coyote Time* - I implemented the feature Coyote Time using [this video as reference](https://www.youtube.com/watch?v=RFix_Kg2Di0). Coyote Time gives the player a brief duration to jump after it has left a platform and is not grounded. This is valuable for game feel because if a player is moving quickly, jumping from platform to platform, Coyote Time gives more leeway to the player on timing jumps and results in a less frustrating experience. To implement Coyote Time, I have a timer counter that resets when the player is grounded and decrements when the player is in the air. Using this counter, I allow the player to jump so long as the timer hasn't run out of time. 

![CoyoteTime](https://github.com/ian-chuang/RoboFroggy/blob/main/media/CoyoteTime.gif)

*Jump Buffer* - I implemented the feature Jump Buffer using [this video as reference](https://www.youtube.com/watch?v=RFix_Kg2Di0). Jump Buffer will buffer the user's jump input for a brief duration and trigger a jump even if the user didn't immediately press the jump button when the player landed. Similar to Coyote Time this gives the player more leeway and makes jumping consecutively easier and smoother. To implement the Jump Buffer, I have a decrementing timer counter that resets when the jump input is pressed. With this, I can trigger a jump if this timer is greater than 0 and the player is grounded (Coyote Timer is greater than zero).

*Fall Gravity* - I implemented the feature Fall Gravity using [this video as reference](https://www.youtube.com/watch?v=2S3g8CgBG1g). Fall Gravity will increase the player's gravity when the player is falling. This is helpful for the player because it makes landing more fluid and precise as players can first jump to a desired height and then quickly fall downwards to a target platform. Implementing this involved increasing the player's rigidbody gravity scale whenever it is falling (its y-velocity is negative).

![FallGravity](https://github.com/ian-chuang/RoboFroggy/blob/main/media/FallGravity.gif)

*Jump Cut* - I implemented the Jump Cut feature using [this code as reference](https://gist.github.com/bendux/b6d7745ad66b3d48ef197a9d261dc8f6). The Jump Cut will slow down a player's upwards velocity when they are jumping if the user lets go of the jump button. This is extremely important for game feel because it gives player's control over their jump height. If they want to jump high, they keep holding the space bar, whereas if they want a short jump, they quickly let go of the space bar. To implement the jump cut, I check whether the player is not holding down the jump input and the player is moving upwards. If they are then I cut the player's velocity by some factor each update.

![JumpCut](https://github.com/ian-chuang/RoboFroggy/blob/main/media/JumpCut.gif)

*Hurt/Damaged Mechanic* - When the player gets hurt by a trap, I wanted the user to really feel the effects of this event. In addition to playing a "hurt" animation, I added a feature so that the player would be knocked back by whatever it was hit with. I also didn't want the user to be able to move the player for a brief duration after being hit to emphasize the hit knockback. I also added a hit cooldown where the player won't take damage for a brief period after being hit to help out the player in case they were in an area with many traps around them. When the player is hit, I would start a coroutine that would set a knockback velocity in the direction opposite to the trap. In the coroutine, I had a hit duration for which the user can not control the player and a hit cooldown for where the player cannot take damage for a brief duration. 

![Hurt](https://github.com/ian-chuang/RoboFroggy/blob/main/media/Hurt.gif)

*Screen Freeze & Shake* - I added to the `CameraController` script to include the ability to momentarily "freeze" time and shake the screen. For this, I employed the help of ChatGPT. This works by setting `Time.timeScale` to `0` for a split second. The amount of time the screen freezes and the magnitude of the screen shake can be changed in the Inspector. I set the screen freeze and shake to occur the moment the player successfully pogo jumps as well as upon taking damage. I think this effect adds a lot to the impact of these events while playing the game.

![ScreenShake](https://github.com/ian-chuang/RoboFroggy/blob/main/media/screenshake.gif)
