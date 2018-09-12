# Labyrinth of Polygons

A mobile tower defense game where you have the build towers to create a maze to stop invaders from leaving your labyrinth.


## Functional Requirements:
Every UI interaction between the player and the game is achieved by using MVC style (ie. Buttons, Inputs, Clicks). What’s below are functionalities which utilized special architectural styles.
 - Before starting the game, player can choose the difficult level (differed by number of enemies, gold income etc)
   - This is achieved by using Blackboard style in game. Since all data are stored in game manager, it is simple to alter those properties and apply to game. 

 - Some enemies can fire to damage the towers.
   - This functionality is achieved by combining the Publish Subscribe style and  the Pipe and Filter style. While the Publish Subscribe style realized the attack between towers and enemies, the Pipe and Filter style guaranteed an intellectual selection of target to attack for both tower and enemy.

 - The player can also upgrade/repair/destroy the existing towers.
   - This is achieved by using MVC style and the Object-Oriented style. While MVC styles handle the selections from the player, the Object-Oriented style makes each tower can be in different condition (ie. different types, level)

 - The game will not end till a certain number of enemies have escaped to the exit.
   - This is achieved by combining the Publish Subscribe style and the Layered style. The enemy controller subscribe each enemy so that it will know if there are any enemies alive on the board. If there are no enemies anymore, the enemy controller will notify the game manager through a number layers, then game manager will give orders to other game components.

## Non-functional Attributes:
 - Efficiency
   - The game should be fast enough on pathfinding.
     - To achieve efficiency on pathfinding. We used the Strategy pattern which encapsulated different algorithm for different situation. 
   - The game should keep being smooth even with a great number of enemies bullets on the screen.
     - By using the Publish Subscribe style, we successfully achieved this non-functional attribute. Since both towers and enemies subscribe each other, the bullets only exist between them. Therefore, handling bullets does not occupy a lot of system resources and redundant graphic computations are avoided.

 - Complexity/Challenging
   - For each wave or level the player goes deeper, the game should keep the difficulty. This is achieved by using the Blackboard style because we can easily modify the number and stats of enemies and the cost of towers, which will keep challenging the player despite the player might have possessed a number of high-level towers.


## Architecture Styles Used
 - Client Server 
   - Used internally to interact with different game components 
   - the gameboard acts as the server and the other modules are clients.
     - The enemies and towers both interacts with through the gameboard

 - Pipe and Filter 
   - the enemy objects targeting AI uses this for decision making.

 - Layered  
   - the Unity engine itself is layered
   - We added multiple layers of abstraction on top of the Unity game engine. 
     - Grid system adds a layer of abstraction on top of the game coordinate system

 - Event Based
   - Unity engine is mostly event based.
   - UI components communicates using events.
   - Components talk to each other using the Unity event system

 - Published Subscribe 
   - The towers are subscribers when they are created.
   - Whenever the enemy moves the gameboard notifies the tower of where they are and if they are in range or not. 

 - Blackboard
   - The game manager is where the references to core component objects are stored.
   - The gameboard is the data storage and modules need to access it to get data.

 - Object-Oriented
   - Tower is an abstract class, there are five inherited class (e.g. Tank Tower) represent various type of towers
   - Main benefits: Understandable, Reusable, Highly Cohesive.

 - MVC
   - Tower Controller plays as Model. it stores actual tower prefabs. Tile Event Handler is controller,  it sends request to Tower Controller (Model) to create a tower object. Tower UI Panel represents View, it generates output message for each operation, and receive input operation request from a user. 

## Design:
### Core System Components:

#### Game Manager:
Singleton pattern.
Purpose: A singleton object created at the start of the game. Acts as the core data accessor and manager for the entire game and handles transfer and persistence of data between different scenes. Hold references to all controller and core objects.

#### Level Manager:
Purpose: An object that is created the manage and hold data regarding the current scene/level. 


#### Grid System:
Purpose: An abstract level on top of the game coordinate system. Where the game objects can use to simplify spawning, movement, and distance checking. Allows the game objects to translate between grid position to game position. 
(Unity already have two coordinate systems, the 3D game world coordinate system and the 2D screen coordinate position) 


#### Gameboard:
Purpose: The game board that the game objects interact with. Hold data for the game that is running such as the enemies’ positions,towers’ positions. It needs to provide interface for the enemy and tower to interact with and implement the gameplay features.
Observer pattern:
It will notify the observers (Tower and Enemy) if they are in range of each other.
Adapter pattern:
Tower objects and enemy objects interracts through adapter functions provided by the game boarrd.


#### Pathfinder:
Strategy pattern.
Purpose: The pathfinder is a game object which encapsulates several algorithms to find the shortest path or check the path availability for different clients. When a client asks for a shortest path, the pathfinder will use corresponding algorithm. The choice of algorithm depends on the phase of gameboard and how efficient the client wants.
The basic algorithm for pathfinding is the Dijkstra’s algorithm. We also upgraded it with caching.

### Game Componets:

#### Tile:
Purpose: The game tile at each grid cell. A game tile can have multiple types it can be either an empty tile, entrance tile, exit tile, or obstacle tile. Each tile is also the input event controller that handles player input such as building of towers. It will also notify the view/UI to get player interaction.
Decorator:
The tile object can be attached with different functionalities dynamically depending on what the type of tile it is. For example if it is a empty tile, it needs to handle user input handle those functionalities are added. If it is a exit tile, then functionalities of taking damage is added.


#### Towers:
Purpose: Since the whole game is about tower defence, the tower is indeed the most significant part of it. The game support 5 various types of tower which are Tank Tower (taunt enemies hatred), Range Tower (low attack damage but long attack range), Slow Tower (slow nearby enemies), Heal Tower (heal nearby ally towers) and Gold Tower (generate golds). Each of these towers has its unique pros and cons, user needs to combine them properly to defend the base as long as possible.

Iterator:
Tower Controller uses Iterator design pattern to control tower game object (aka prefab). More specifically, there are total 5 different type of towers. These tower scripts and different initial variables are assigned into 5 different tower prefabs respectively. The Tower Controller does not need to know what these towers are capable of, it just provides a way to access this tower prefabs when the Tile Event Handler need them.

Singleton:
Tower Controller uses Singleton design pattern. When user clicks on an empty tile and selects a desired tower, the Tile Event Handler cannot build the tower itself because the job of the Tile Event Handler is handling the on mouse click event. Therefore, Tile Event Handler has to ask other class, which is Tower Controller to do this request. In order to make these building requests as simple as possible, the Tower Controller is designed by using Singleton design pattern, which means it provides a global point of access to it. And the Tile Event Handler just need to initialize the Tower Controller instance on first use, this make life more easier.

State:
Tile Event Handler uses State design pattern. It allows the Tile Event Handler to alter its behavior when its internal state (Tower Operation) changes. There are total 9 internal different states, 5 for building different type of towers, 3 for operation an existing tower, and 1 for NOP. For example, if a Tank Tower button is clicked by a user, then the selected Tile Event Handler changes its state from NOP to Build-Tank-Tower. Then if user confirm the building request, then Tile Event Handler perform the last Tower Operation, which is building a tank tower. Similarly, if a Upgrade Button is clicked, then the Tile Event Handler changes its state to Upgrade-Current-Tower, the corresponding existing tower would be upgrade.


#### Tower Building / Operational Buttons:
Purpose: Basic level 1 towers are good for the early rounds, however, as the time goes, enemies become stronger and stronger. In order to survive longer, user could either build new towers or operate (upgrade and repair ) existing ones during the battle phase. Besides, user is also able to remove existing towers in the building phase, the enemies path might change in this case.

Singleton:
Tower Operation Buttons (upgrade, repair, sell) use Singleton design pattern. The number of operational buttons are limited, using singleton not only avoid increase the number of tracked singleton instance dramatically, but also help every Tile Event Handler to access them more easily.

Observer:
Tower Building Buttons and Tower Operation Buttons use Observer design pattern. The purpose of these buttons are setting the correct state of the current selected Tile Event Handler, and then the Tile Event Handler could perform the correct state later. However, each enemy tile might has its Tile Event Handler, so the maximum number of Tile Event Handler is 192, how the building and operational buttons know which Tile Event Handler that they are going to modify? To solve this problem, observer are used. When a Tile Event Handler is selected, it notifies those buttons, then these buttons know exactly which Tile Event Handler is being used right now.


#### Tower Relative UI Panels:
Purpose: Tower Notification Panel shows some warning messages like block-path case and not-enough-gold case to let user know that the request is forbidden. Tower Info Panel displays tower parameter like hitpoint, attack damage etc.. Build Check Panel allows user to confirm building or operating a tower.

Singleton:
These three tower relative panels are designed as Singleton. These panels allows a user to know if building a tower on some position is ok. Also, a user could use the panel to check if the selected tower is the one that really need to build. Therefore, these panels may be use (appear or disappear) very frequently. That’s the reason why Singleton design pattern is used. It allow the system to operate these panel easily.





#### Enemy:
Observer pattern
Usage: enemy’s moving
Explanation: when enemy enters a new cell, notify the observers (cell and tower) to update the status: cells add the enemy to its current position, and towers add the enemy to its attacking list if it is in the attack range.

State pattern:
Usage: enemy controller
Explanation: for different stages (prepare and attack), the states are different for the enemy controller. Based on the state it will have different actions: to spawn enemy or not.

Factory pattern
Usage: spawn enemies
Explanation: Spawning different enemies (attacking, fast, flying, normal etc) by different types



Team Responsibality:

Ethan (Yifeng) Li - Core System Engineering (game manager, level manager, grid system, gameboard)

Richard (Xingyu) Xia - Pathfinder, User Interface Engineering(game level, score level), Android Build Engineering

Wayne (Yishi) Wang - Tower Systems Engineering (tower controller, tower UI, tower behaviour), iOS Build Engineering

Tianchen Bi - Enemy Systems Engineering (enemy controller, enemy behaviour)


