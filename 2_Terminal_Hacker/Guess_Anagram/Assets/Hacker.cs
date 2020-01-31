using UnityEngine;

public class Hacker : MonoBehaviour {

	// Game configuration data
	string[] level1Passwords = { "books", "aisle", "shelf", "password", "font", "borrow"};
	string[] level2Passwords = { "prisoner", "handcuffs", "holster", "uniform", "arrest"};
	string[] level3Passwords = { "starfield", "telescope", "environment", "exploration", "astronauts"};
	const string menuHint = "You may type menu at any time.";

	int level;
	string password;

	enum Screen {MainMenu, Password, Win };
	Screen currentScreen;

	// Use this for initialization
	void Start () {
		ShowMainMenu();
	}


	// this should only decide how to handle input, not actually do it
	void OnUserInput(string input)
    {
		if (input == "menu") {
			ShowMainMenu();
        }
		else if (input == "quit" || input == "close" || input == "exit")
		{
			Terminal.WriteLine("If on the web close the browser tab");
			Application.Quit();
		}
		else if (currentScreen == Screen.MainMenu) 
		{
			RunMainMenu(input);
        }
		else if (currentScreen == Screen.Password)
		{
			CheckPassword(input);
		}

    }


	void ShowMainMenu()
	{
		currentScreen = Screen.MainMenu;
		Terminal.ClearScreen();

		Terminal.WriteLine("What would you like to hack into?");
		Terminal.WriteLine("Press 1 for the local library");
		Terminal.WriteLine("Press 2 for the police station");
		Terminal.WriteLine("Press 3 for NASA");
		Terminal.WriteLine("Enter your selection: ");
	}


	void RunMainMenu(string input)
    {
		bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");
		if (isValidLevelNumber)
		{
			level = int.Parse(input); // convert string to int
			AskForPassword();
		}
		else if (input == "007")
		{
			Terminal.WriteLine("Please select a level Mr. Bond!");
		}
		else
		{
			Terminal.WriteLine("Please choose a valid level");
			Terminal.WriteLine(menuHint);
		}
	}
	

	void AskForPassword()
	{
		currentScreen = Screen.Password;
		Terminal.ClearScreen();
		SetRandomPassword();
		Terminal.WriteLine("Enter your password, hint: " + password.Anagram());
		Terminal.WriteLine(menuHint);
	}

	private void SetRandomPassword()
	{
		switch (level)
		{
			case 1:
				password = level1Passwords[Random.Range(0, level1Passwords.Length)];
				break;
			case 2:
				password = level2Passwords[Random.Range(0, level2Passwords.Length)];
				break;
			case 3:
				password = level3Passwords[Random.Range(0, level3Passwords.Length)];
				break;
			default:
				Debug.LogError("Invalid level number");
				break;
		}
	}

	void CheckPassword(string input)
	{
		if (input == password)
		{
			DisplayWinScreen();
		}
		else
		{
			AskForPassword();
		}
	}

	void DisplayWinScreen()
	{
		currentScreen = Screen.Win;
		Terminal.ClearScreen();
		ShowLevelReward();
		Terminal.WriteLine(menuHint);
	}

	void ShowLevelReward()
	{
		switch (level)
		{
			case 1:
				Terminal.WriteLine("Have a book...");
				Terminal.WriteLine(@"
     _________
    /      / /
   /      / /
  /      / /    ~~~~~*~*~*~*
 /______/_/
(______(_/
"
				);
				break;
			case 2:
				Terminal.WriteLine("Have a donut...");
				Terminal.WriteLine(@"
       ___
    .-'   `-.
  .'   . ;   `.
 /   ; ... ' :  \
|  :  (   ) ; ` |    mmmmmmmm
 \   . ```  '  /
  `.   . '   .'
    `-.___.-'
"
				);
				break;
			case 3:
				Terminal.WriteLine("Welcome to NASA's database system!");
				Terminal.WriteLine(@"
 ,-,-.  +   .  *  
/.( +.\          *
\ {. */    +   `
 `-`-'-'
"
				);
				break;
			default:
				Debug.LogError("Invalid level reached");
				break;
		}		
	}
}
