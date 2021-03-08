# Tanks
A short demo of how a player might control rigidbody movement. Contains a simple terrain, a player capsule and a simulated player to interact with.

Written in Unity 2020.2.3f1
 
This is a basic demonstrator to answer a question from the Unity Physics forum, but feel free to do whatever you like with it. I recommend using the physics solver "Temporal Gauss Seidel" due to the partial rigidbody rotational freeze constraints. This should already be set in the project.

It uses the old input system to make the code easier to read for people without experience of the new Input System.

It uses the Ignore Raycast layer to save adding a layer mask to the ground check.

It uses the Player tag to identify colliders associated with the player object.

All of the code is in one class - just to make it as readable as possible, obviously you should try to keep your classes as simple as possible.

Have fun.
