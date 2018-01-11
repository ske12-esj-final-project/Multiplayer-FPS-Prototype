# Multiplayer FPS Prototype
Multiplayer FPS prototype purposed to study basic FPS mechanics and Networking using Unity3D.

## Scripts
- RequireComponent states that class requires specific type so we don't have to perform error handling.
- Marking variables as [SerializeField] will make it show in the Inspector view even though they are private.
- To avoid the conflict of Script usage between Players, we need to disable desired components form non-local players.
- In this case, we disabled PlayerMotor, PlayerController, SceneCamera, and AudioListeners.

## Movement
- GetAxisRaw is as same as GetAxis except smoothness.
- Normalization guarantees our vectors length to be 1. Multiplicated by speed, we will get the velocity added to the Player.
- We can move a Player using MovePosition where distance = velocity * time.
- We can turn a Player around using MoveRotation and Quaternion.

## Networking
- uNet is splited up to 2 levels, HLAPI and Transport Layer.
- Transport Layer takes care of low-level and stuffs behind the scenes.
- High-level API (HLAPI) connects to Transport Layer and takes care of distributed object management, state synchronization, and other classes.
- Unity prepared the components such as Network Manager, Network Identity, Network Transform.
- uNet is based on the principle such that client is considered to be a host and used server as the same machine as client.
- When client wants to interact with another one, client connects to the host via server, and server will send response to another one.
- A lot of modern games don't use this since players don't want to type in IP address every time. They want matchmaking but it's restricted by firewall and NAT configuration.
- Unity solved it by Unity Relay Server as a middle between client and server.

## Spawning
- Create empty GameObject and set NetworkManager Player Spawn Met to be Round Robin.
