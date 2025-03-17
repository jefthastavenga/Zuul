using System;

class Game
{
	// Private fields
	private Parser parser;
	private Player Player;
	// private Room currentRoom;

	// Constructor
	public Game()
	{
		parser = new Parser();
		Player = new Player();
		CreateRooms();
	}

	// Initialise the Rooms (and the Items)
	private void CreateRooms()
	{
		// Create the rooms
		Room inside = new Room("inside the main hal of the university");
		Room outside = new Room("outside the main entrance of the university");
		Room theatre = new Room("in a lecture theatre");
		Room pub = new Room("in the campus pub");
		Room lab = new Room("in a computing lab");
		Room basement = new Room("in the basement");
		Room secondFloor = new Room("on the second floor");
		Room storageroom = new Room("in the storage room");
		Room bedroom = new Room("in the bedroom");

		// Initialise room exits
		outside.AddExit("inside", inside);

		inside.AddExit("east", theatre);
		inside.AddExit("south", lab);
		inside.AddExit("west", pub);
		inside.AddExit("down", basement);
		inside.AddExit("up", secondFloor);

		theatre.AddExit("west", inside);

		pub.AddExit("east", inside);

		lab.AddExit("north", inside);

		basement.AddExit("up", inside);
		basement.AddExit("north", storageroom);

		secondFloor.AddExit("down", inside);
		secondFloor.AddExit("north", bedroom);

		storageroom.AddExit("south", basement);

		bedroom.AddExit("south", secondFloor);
		// Create your Items here
		// ...
		// And add them to the Rooms
		// ...

		// Start game outside
		Player.CurrentRoom = outside;
	}

	//  Main play routine. Loops until end of play.
	public void Play()
	{
		PrintWelcome();

		// Enter the main command loop. Here we repeatedly read commands and
		// execute them until the player wants to quit.
		bool finished = false;
		while (!finished)
		{
			Command command = parser.GetCommand();
			finished = ProcessCommand(command);
		}
		Console.WriteLine("Thank you for playing.");
		Console.WriteLine("Press [Enter] to continue.");
		Console.ReadLine();
	}

	// Print out the opening message for the player.
	private void PrintWelcome()
	{
		Console.WriteLine();
		Console.WriteLine("Welcome to Zuul!");
		Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
		Console.WriteLine("Type 'help' if you need help.");
		Console.WriteLine();
		Console.WriteLine(Player.CurrentRoom.GetLongDescription());
	}

	// Given a command, process (that is: execute) the command.
	// If this command ends the game, it returns true.
	// Otherwise false is returned.
	private bool ProcessCommand(Command command)
	{
		bool wantToQuit = false;

		if (command.IsUnknown())
		{
			Console.WriteLine("I don't know what you mean...");
			return wantToQuit; // false
		}

		switch (command.CommandWord)
		{
			case "help":
				PrintHelp();
				break;
			case "go":
				GoRoom(command);
				break;
			case "quit":
				wantToQuit = true;
				break;
			case "look":
				Look(command);
				break;
			case "status":
				Status();
				break;
		}

		return wantToQuit;
	}

	// ######################################
	// implementations of user commands:
	// ######################################

	// Print out some help information.
	// Here we print the mission and a list of the command words.
	private void PrintHelp()
	{
		Console.WriteLine("You are lost. You are alone.");
		Console.WriteLine("You wander around at the university.");
		Console.WriteLine();
		// let the parser print the commands
		parser.PrintValidCommands();
	}
	private void Look(Command command)
	{
		Console.WriteLine(Player.CurrentRoom.GetLongDescription());
	}
	// Try to go to one direction. If there is an exit, enter the new
	// room, otherwise print an error message.
	private void GoRoom(Command command)
	{
		if (!command.HasSecondWord())
		{
			// if there is no second word, we don't know where to go...
			Console.WriteLine("Go where?");
			return;
		}

		string direction = command.SecondWord;

		// Try to go to the next room.
		Room nextRoom = Player.CurrentRoom.GetExit(direction);
		Player.Damage(50);
		if (nextRoom == null)
		
		if (Player.Health >= 10)
		{
			Player.CurrentRoom = nextRoom;
			Console.WriteLine(Player.CurrentRoom.GetLongDescription());
		}

		if (Player.Health <= 1)
		{
			// Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("you have died.");
			Environment.Exit(0);
		}

		Player.CurrentRoom = nextRoom;
		Console.WriteLine(Player.CurrentRoom.GetLongDescription());
	}
	private void Status()
	{
		Console.WriteLine("Player's health: " + Player.Health);
	}
}

