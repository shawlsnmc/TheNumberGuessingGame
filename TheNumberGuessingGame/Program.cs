using System;

namespace TheNumberGuessingGame
{
    class Program
    {
        ///
        /// Declare game variables
        ///
        private const int MAX_NUMBER_OF_PLAYER_GUESSES = 4;
        private const int MIN_NUMBER_TO_GUESS = 1;
        private const int MAX_NUMBER_TO_GUESS = 10;

        private static int playersGuess;
        private static int numberToGuess;
        private static int numberOfWins;
        private static int numberOfRounds;
        private static int numberOfCurrentPlayerGuess;
        private static int[] numbersPlayerHasGuessed = new int[MAX_NUMBER_OF_PLAYER_GUESSES];

        private static bool playingGame;
        private static bool playingRound;
        private static bool numberGuessedCorrectly;

        /// <summary>
        /// Application's Main method
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            //
            // Initialize new game
            //
            InitializeGame();

            //
            // Display the Welcome Screen with application Quit option
            //
            DisplayWelcomeScreen();

            //
            // Display the game rules
            //
            DisplayRulesScreen();

            //
            // Game loop
            // 
            while (playingGame)
            {
                //
                // Initialize new round
                //
                InitializeRound();

                //
                // Round loop
                // 
                while (playingRound)
                {
                    //
                    // Display the player guess screen and return the player's guess
                    //
                    playersGuess = DisplayGetPlayersGuessScreen();

                    //
                    // Evaluate the player's guess and provide the player feedback
                    //
                    DisplayPlayerGuessFeedback();

                    //
                    // Update round variables, process the results and provide player feedback
                    //
                    UpdateAndDisplayRoundStatus();
                }

                //
                // Round complete, display player stats and prompt to Continue/Quit
                //
                DisplayPlayerStats();
            }

            DisplayClosingScreen();
        }

        /// <summary>
        /// Initialize all game variables
        /// </summary>
        private static void InitializeGame()
        {
            playingGame = true;
            numberOfWins = 0;
            numberOfRounds = 0;
        }

        /// <summary>
        /// Initialize all round variables and get number to guess
        /// </summary>
        private static void InitializeRound()
        {
            numbersPlayerHasGuessed = new int[MAX_NUMBER_OF_PLAYER_GUESSES];
            playingRound = true;
            numberToGuess = GenRandom(MIN_NUMBER_TO_GUESS, MAX_NUMBER_TO_GUESS);
            numberOfCurrentPlayerGuess = 0;
            numberOfRounds++;
            numberGuessedCorrectly = false;

        }

        /// <summary>
        /// Display the opening screen and prompt to Continue/Quit
        /// </summary>
        private static void DisplayWelcomeScreen()
        {
            ConsoleKeyInfo playerKeyResponse;

            Console.Clear();

            Console.WriteLine("\n\n");
            Console.WriteLine("     Welcome to The Number Guessing Game");
            Console.WriteLine("          Lame Headache Productions");
            Console.WriteLine("\n\n");
            Console.WriteLine("        Press the (Esc) key to Quit.");
            Console.WriteLine("        Press any other key to Play.");

            playerKeyResponse = Console.ReadKey();

            //
            // Note: Console window closes immediately without displaying the closing screen.
            //
            if (playerKeyResponse.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Display the game rules
        /// </summary>
        private static void DisplayRulesScreen()
        {
            Console.Clear();

            Console.WriteLine("                The Number Guessing Game");
            Console.WriteLine("\n\n");
            Console.WriteLine("The computer will randomly select a number between 1 and 10.");
            Console.WriteLine("You will have four attempts to guess the number. After each");
            Console.WriteLine("guess the computer will indicate if you have guessed correctly");
            Console.WriteLine("or whether your guess is high or low.");
            Console.WriteLine("\n\n");
            Console.WriteLine("Press the any key to continue.");

            Console.ReadKey();
        }

        /// <summary>
        /// Prompt for and return the player's guess
        /// </summary>
        /// <returns></returns>
        private static int DisplayGetPlayersGuessScreen()
        {
            string playerResponse;
            int playerGuess = 0;
            bool validResponse = false;

            //
            // Validate player's guess
            //
            while (!validResponse)
            {
                //
                // Clear screen and set header
                //
                DisplayReset();

                // get user input
                Console.Write($"Please enter you guess between {MIN_NUMBER_TO_GUESS} and {MAX_NUMBER_TO_GUESS}: ");
                playerResponse = Console.ReadLine();

                // is it a valid number?
                validResponse = Int32.TryParse(playerResponse, out playerGuess);

                //It's a number but is it in the range?
                if (validResponse && playerGuess >= MIN_NUMBER_TO_GUESS && playerGuess <= MAX_NUMBER_TO_GUESS)
                {
                    //Guess truely is valid
                }
                //Input is not valid
                else
                {
                    validResponse = false;
                    Console.WriteLine($"I'm sorry but I need a number between {MIN_NUMBER_TO_GUESS} and {MAX_NUMBER_TO_GUESS}, please try again.");
                    DisplayContinueQuitPrompt();
                }


                
            }
            return playerGuess;
        }

        /// <summary>
        /// Evaluate the player's guess and provide the player feedback
        /// </summary>
        private static void DisplayPlayerGuessFeedback()
        {
            Console.WriteLine();
            if (playersGuess < numberToGuess)
            {
                //too low
                Console.WriteLine("Woah! You're too low!");
            }
            else if(playersGuess > numberToGuess)
            {
                //too high
                Console.WriteLine("Hitting the helium again I see! You're too high!");
            }
            else //if(playersGuess == numberToGuess)
            {
                //Guessed correctly
                Console.WriteLine("I'm not sure how but you guessed correctly!");
                numberGuessedCorrectly = true;
            }
        }

        /// <summary>
        /// Update round status, process the results and provide player feedback
        /// </summary>
        private static void UpdateAndDisplayRoundStatus()
        {
            Console.WriteLine();
            //Log player's guess
            numbersPlayerHasGuessed[numberOfCurrentPlayerGuess] = playersGuess;

            //increment guess counter
            numberOfCurrentPlayerGuess++;

            //
            // Player guessed correctly
            // 
            if (numberGuessedCorrectly)
            {
                numberOfWins++;
                //round over
                playingRound = false;
            }
            //
            // Player guessed incorrectly and has more guesses left
            // 
            else if (numberOfCurrentPlayerGuess < MAX_NUMBER_OF_PLAYER_GUESSES)
            {
                Console.WriteLine($"You have {MAX_NUMBER_OF_PLAYER_GUESSES - numberOfCurrentPlayerGuess} attempts remaining this round.");
                Console.WriteLine();
            }
            //
            // Player guessed incorrectly and has no more guesses left
            // 
            else
            {
                //round over
                playingRound = false;
                Console.WriteLine("I'm sorry to inform you, you have ran out of guesses and have lost this round.");
                Console.WriteLine();
            }

            Console.Write("This round you have made the following guesses: ");
            for (int t = 0; t < numberOfCurrentPlayerGuess; t++) { 
                Console.Write($"{numbersPlayerHasGuessed[t]} ");
            }
            Console.WriteLine();
 

            DisplayContinueQuitPrompt();
        }

        /// <summary>
        /// Display the player's current stats and prompt to Continue/Quit
        /// </summary>
        private static void DisplayPlayerStats()
        {
            DisplayReset();


            //proper english please... if 1 round use the word round otherwise use rounds
            if (numberOfRounds > 1)
            {
                Console.WriteLine($"You have played {numberOfRounds} rounds, winning {numberOfWins} of them.");
            }
            else
            {
                Console.WriteLine($"You have played {numberOfRounds} round, winning {numberOfWins} of them.");
            }

            Console.WriteLine("You may quit now or we can play another round, it's up to you.");
            DisplayContinueQuitPrompt();
        }

        /// <summary>
        /// Display the closing screen
        /// </summary>
        private static void DisplayClosingScreen()
        {
            //
            // Clear screen and set header
            //
            DisplayReset();

            Console.WriteLine("Thank you for playing our Number Guessing Game.\n");
            Console.WriteLine("          Lame Headache Productions.\n");
            Console.WriteLine("            Press any key to Exit.");

            Console.ReadKey();

            Environment.Exit(0);
        }



        /// <summary>
        /// reset display to default size and colors including the header
        /// </summary>
        public static void DisplayReset()
        {
            Console.Clear();
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;

            Console.WriteLine();
            Console.SetCursorPosition(15, 1);
            Console.WriteLine("   The Number Guessing Game   ");
            Console.WriteLine();

            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// display the Continue/Quit prompt
        /// </summary>
        public static void DisplayContinueQuitPrompt()
        {
            Console.CursorVisible = false;

            Console.WriteLine();

            Console.WriteLine("Press any key to continue, H for help, or press the ESC key to quit.");
            Console.WriteLine();
            ConsoleKeyInfo response = Console.ReadKey();

            //
            // Set flag if player chooses to quit
            //
            if (response.Key == ConsoleKey.Escape)
            {
                DisplayClosingScreen();
            }

            //
            // Show help screen if player presses h or H
            //
            else if (response.Key.ToString().ToUpper() == "H")
            {
                DisplayRulesScreen();
            }

            Console.CursorVisible = true;
}


        /// <summary>
        /// Generates a random number between (and including) min and max
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int GenRandom(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max+1);

        }
    }
}
