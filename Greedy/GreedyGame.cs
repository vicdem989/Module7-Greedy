

using Utils;
using System.Timers;
using LANGUAGE;


namespace Greedy
{


    public class GAME_SIZE
    {
        const int MIN_ROWS = 10;
        const int MIN_COLUMNS = 10;

        static int largeRows = Console.WindowWidth / 100 * 75;
        static int largeCol = Console.WindowHeight / 100 * 75; 

        public int rows = 0;
        public int columns = 0;

        public static GAME_SIZE SMALL { get { return new GAME_SIZE() { rows = MIN_ROWS, columns = MIN_COLUMNS }; } }
        public static GAME_SIZE LARGE { get { return new GAME_SIZE() { rows = largeRows, columns = largeCol }; } }
        public static GAME_SIZE XTRA_LARGE { get { return new GAME_SIZE() { rows = Console.WindowWidth, columns = Console.WindowHeight}; } }
    }


    public class GreedyGame : GameEngine.IScene
    {
        const int EMPTY = 0;
        const int MIN_NUMBER = 1;
        const int MAX_NUMBER = 9;
        const int PLAYER_ID = -1;
        const char PLAYER_TOKEN = '\u263A';

        const int HUD_HEIGHT = 3;

        GAME_SIZE gameSize;
        int[,] gameBoard;
        (int row, int column) player;
        bool playerMoved = false;
        bool dirty = true;
        int delta_y;
        int delta_x;
        int moveCount = 0;

        int score = 0;
        int maxScore = 0;
        double percentageMaxScoreGotten = 0;

        bool isPlaying = true;


        public GreedyGame(GAME_SIZE size)
        {
            gameSize = size;
        }

        #region Greedy Game Functions ----------------------------------------------------------------------------------

        int[,] CreateGameBoard(GAME_SIZE size)
        {
            int[,] gameBoard = new int[size.rows, size.columns];
            return gameBoard;
        }

        int[,] FillGameBoard(int[,] gameBoard, int minValue, int maxValue)
        {

            int rowCount = gameBoard.GetLength(0);
            int colCount = gameBoard.GetLength(1);
            Random rnd = new Random((int)DateTime.Now.Ticks);

            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount; col++)
                {
                    gameBoard[row, col] = rnd.Next(minValue, maxValue);
                    maxScore += gameBoard[row, col];
                }
            }

            return gameBoard;

        }

        (int row, int column) PickRandomStartPosition(int[,] gameBoard)
        {
            int rows = gameBoard.GetLength(0);
            int col = gameBoard.GetLength(1);
            Random rnd = new Random();
            return (row: rnd.Next(rows), column: rnd.Next(col));
        }

        private double PercentageCalculation(int score, int maxScore)
        {
            return ((double)score / maxScore) * 100.0;
        }

        private void GameOver(bool playStatus)
        {
            isPlaying = playStatus;
            Console.Clear();
            Output.Write(Output.Align(Language.appText.GameOver, Alignment.CENTER), true);
            Output.Write(Output.Align($"{Language.appText.YouGot} {ANSICodes.Colors.Green}{score} {Language.appText.OutOf} {maxScore}{ANSICodes.Reset}", Alignment.CENTER), true);
            Output.Write(Output.Align($"{Language.appText.MeaningYouGot} {ANSICodes.Colors.Cyan}{(PercentageCalculation(score, maxScore)).ToString("0.00")}% {Language.appText.Of} {maxScore}!{ANSICodes.Reset}", Alignment.CENTER), true);
            Output.Write(Output.Align(Language.appText.PlayAgain, Alignment.CENTER), true);
            string input = Console.ReadLine().ToLower();
            while (input == string.Empty && input != "y" && input != "n" && input != "j")
            {
                Output.Write(Output.Align(Language.appText.InvalidInput, Alignment.CENTER), true);
                input = Console.ReadLine().ToLower();
            }

            if (input == "n")
            {
                Output.Write(Output.Align(Language.appText.ThanksForPlaying));
                OnExitScreen(null, null);
            }
            else
            {
                OnExitScreen(typeof(GreedyGame), new object[] { GAME_SIZE.SMALL });
            }

        }

        private void DoMoves()
        {
            gameBoard[player.row, player.column] = EMPTY;
            player.row += delta_y;
            player.column += delta_x;
            if (gameBoard[player.row, player.column] < 1 || gameBoard[player.row, player.column] == EMPTY)
                GameOver(false);
            gameBoard[player.row, player.column] = PLAYER_ID;
            score++;
            moveCount--;
            dirty = true;
        }

        private void SetUpMoves()
        {
            playerMoved = false;
            gameBoard[player.row, player.column] = EMPTY;
            player.row += delta_y;
            player.column += delta_x;
            if (gameBoard[player.row += delta_y, player.column += delta_x] == EMPTY)
            {
                GameOver(false);
            }
            moveCount = gameBoard[player.row, player.column];
            if (moveCount < 1)
                GameOver(false);
            gameBoard[player.row, player.column] = PLAYER_ID;
            dirty = true;
        }


        private string drawBoard(int rowCount, int colCount, string output, int y, int x)
        {
            for (int row = 0; row < rowCount; row++)
            {
                output = $"{output}{ANSICodes.Positioning.SetCursorPos(y + row, x)}";
                for (int col = 0; col < colCount; col++)
                {
                    if (gameBoard[row, col] != PLAYER_ID && gameBoard[row, col] != EMPTY)
                    {
                        output = $"{output} \u001b[38;5;{gameBoard[row, col]}m{gameBoard[row, col]}{ANSICodes.Reset}";
                    }
                    else if (gameBoard[row, col] == PLAYER_ID)
                    {
                        output = $"{output} {ANSICodes.BgColors.Blue}{PLAYER_TOKEN}{ANSICodes.Reset}";
                    }
                    else
                    {
                        output = $"{output}  ";
                    }
                }

            }
            return output;
        }





        #endregion -----------------------------------------------------------------------------------------------------

        #region GameEngine.IScene --------------------------------------------------------------------------------------

        public Action<Type, object[]> OnExitScreen { get; set; }

        public void init()
        {
            gameBoard = CreateGameBoard(gameSize);
            gameBoard = FillGameBoard(gameBoard, MIN_NUMBER, MAX_NUMBER);
            player = PickRandomStartPosition(gameBoard);
            gameBoard[player.Item1, player.Item2] = PLAYER_ID;
            score = 0;
            dirty = true;
            isPlaying = true;
        }

        public void input()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey keyCode = Console.ReadKey(true).Key;
                if (keyCode == ConsoleKey.DownArrow || keyCode == ConsoleKey.W)
                {
                    delta_y = 1;
                    delta_x = 0;
                }
                else if (keyCode == ConsoleKey.UpArrow || keyCode == ConsoleKey.S)
                {
                    delta_y = -1;
                    delta_x = 0;
                }
                else if (keyCode == ConsoleKey.LeftArrow || keyCode == ConsoleKey.A)
                {
                    delta_x = -1;
                    delta_y = 0;
                }
                else if (keyCode == ConsoleKey.RightArrow || keyCode == ConsoleKey.D)
                {
                    delta_x = 1;
                    delta_y = 0;
                }
                else if (keyCode == ConsoleKey.Q)
                {
                    OnExitScreen(null, null);
                }
                else if (keyCode == ConsoleKey.R)
                {
                    OnExitScreen(typeof(GreedyGame), new object[] { GAME_SIZE.SMALL });
                }

                if (delta_y != 0 || delta_x != 0)
                    playerMoved = true;
            }
        }

        public void update()
        {
            if (playerMoved)
            {
                SetUpMoves();
            }
            else if (moveCount > 0)
            {
                DoMoves();
            }
            percentageMaxScoreGotten = PercentageCalculation(score, maxScore);
        }

        public void draw()
        {
            if (!dirty)
                return;

            Console.Clear();
            dirty = false;

            int rowCount = gameBoard.GetLength(0);
            int colCount = gameBoard.GetLength(1);

            string output = "";

            int x = (int)((Console.WindowWidth - gameSize.columns) * 0.5);
            int y = (int)((Console.WindowHeight - gameSize.rows) * 0.5) + HUD_HEIGHT;

            Console.Write($"{ANSICodes.Positioning.SetCursorPos((y - HUD_HEIGHT), x)}{Language.appText.Score}{score}");

            Output.Write(drawBoard(rowCount, colCount, output, y, x));

            Output.Write(Output.Align($"\n{(int)percentageMaxScoreGotten} {Language.appText.PercentageMaxScore} ", Alignment.CENTER), true);
        }



        #endregion ---------------------------------------------------------------------------------------------------------
    }
}