# Multiplayer FPS Prototype
Multiplayer FPS prototype purposed to study basic FPS mechanics and Networking using Unity3D.

## Scripts
- RequireComponent states that class requires specific type so we don't have to perform error handling.
- Marking variables as [SerializeField] will make it show in the Inspector view even though they are private.
- To avoid the conflict of Script usage between Players, we need to disable desired components form non-local players.
- In this case, we disabled PlayerMotor, PlayerController, SceneCamera, and AudioListeners.
- OnDisable will be called when GameObject is being destroyed.
- Header is used for grouping variables displayed on Inspector.
- Since we don't want to apply Spring when Player is jumping, so we need to setup ConfigurableJoint in the Script.
- We use Physics.Raycast to perform a shooting (more details [here](https://docs.unity3d.com/ScriptReference/Physics.Raycast.html)).
- Marking method as [Client] will make it to be called on the Client only.
- Marking method as [Command] will make it to be called on the Server only.
- base is used to access the base class from derived class (as same as super in Java).
- Marking variable as [SyncVar] will make it to be broadcasted its value to all clients in the server (e.g. current health).
- Awake is used to initialize any variables or game state before the game starts.
- Marking method as [ClientRpc] will make it sure that it will be called in all clients.
- Use #region to fold the code when it's too long and head to read.

## Movement
- GetAxisRaw is as same as GetAxis except smoothness.
- Normalization guarantees our vectors length to be 1. Multiplicated by speed, we will get the velocity added to the Player.
- We can move a Player using MovePosition where distance = velocity * time.
- We can turn a Player around using MoveRotation and Quaternion.
- For Jumping, use AddForce since we need actual force, not a rigid movement as Moving and Rotating.
- Use Configurable Joint and customize Spring and Damper value in Y Drive.
- In Rigidbody, there is an option called Drag which can be considered as an air resistance.
- In Jumping, we use Acceleration = Player's Mass.

## Networking
- uNet is splited up to 2 levels, HLAPI and Transport Layer.
- Transport Layer takes care of low-level and stuffs behind the scenes.
- High-level API (HLAPI) connects to Transport Layer and takes care of distributed object management, state synchronization, and other classes.
- Unity prepared the components such as Network Manager, Network Identity, Network Transform.
- uNet is based on the principle such that client is considered to be a host and used server as the same machine as client.
- When client wants to interact with another one, client connects to the host via server, and server will send response to another one.
- A lot of modern games don't use this since players don't want to type in IP address every time. They want matchmaking but it's restricted by firewall and NAT configuration.
- Unity solved it by Unity Relay Server as a middle between client and server.

## Syncing Movement
- NetworkTransform helps game smoother and can predict where Players are going using Rigidbody synchronization.
- Movement Threshold is how much we can move in units before data is sending out.
- Snap Threshold is how many units we can move before we don't do any interpolation and snap to that point (e.g. teleporting, lag spikes).
- Configurable Network Send rate per seconds, setting too much rate is impossible.
- We want to sync the rotation also, so we need to set Network Transform child as Camera with zero Interpolate Movement.

## Spawning
- Create empty GameObject and set NetworkManager Player Spawn Met to be Round Robin.

## LayerMask
- We use LayerMask to distinguish shooting layer between LocalPlayer and RemotePlayer.
- Since Unity uses Layer as an index but they represented as string, so we need to use LayerMask.NameToLayer to convert string to int.

## Shooting
- Player locally check that if bullet hits another one, then it calls CmdPlayerShot from the server. The server tells another Player has been shot and should be taken damage and now client will perform TakeDamage. Since currentHealth is SyncVar so it will broadcast value to all clients in the server.
