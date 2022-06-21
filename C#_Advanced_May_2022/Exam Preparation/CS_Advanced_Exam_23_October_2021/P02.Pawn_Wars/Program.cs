using System;

namespace P02.Pawn_Wars
{
    internal class Program
    {
        static void Main()
        {
            char[,] chessboard = new char[8, 8];

            Pawn whitePawn = null;
            Pawn blackPawn = null;
            BoardFill(chessboard, ref whitePawn, ref blackPawn);

            Pawn winingPawn = null;

            while (true)
            {
                if(whitePawn.Row == 0)
                {
                    winingPawn = whitePawn;
                    break;
                }

                if(blackPawn.Row == chessboard.GetLength(0) - 1)
                {
                    winingPawn = blackPawn;
                    break;
                }

                //White Pawn Move
                if (whitePawn.Row + -1 == blackPawn.Row)
                {
                    if (whitePawn.Col + -1 == blackPawn.Col)
                    {
                        whitePawn.Row += -1;
                        whitePawn.Col += -1;
                        winingPawn = whitePawn;
                        break;
                    }

                    if (whitePawn.Col + 1 == blackPawn.Col)
                    {
                        whitePawn.Row += -1;
                        whitePawn.Col += 1;
                        winingPawn = whitePawn;
                        break;
                    }

                    whitePawn.Row += -1;
                }
                else
                {
                    whitePawn.Row += -1;
                }

                //Black Pawn Move

                if (blackPawn.Row + 1 == whitePawn.Row)
                {
                    if (blackPawn.Col + -1 == whitePawn.Col)
                    {
                        blackPawn.Row += 1;
                        blackPawn.Col += -1;
                        winingPawn = blackPawn;
                        break;
                    }

                    if (blackPawn.Col + 1 == whitePawn.Col)
                    {
                        blackPawn.Row += 1;
                        blackPawn.Col += 1;
                        winingPawn = blackPawn;
                        break;
                    }

                    blackPawn.Row += 1;
                }
                else
                {
                    blackPawn.Row += 1;
                }
            }

            if(winingPawn.Row == 0 || winingPawn.Row == chessboard.GetLength(0) - 1)
                Console.WriteLine($"Game over! {winingPawn.Color} pawn is promoted to a queen at {(char)(winingPawn.Col + 97)}{(char)(56 - winingPawn.Row)}.");
            else
                Console.WriteLine($"Game over! {winingPawn.Color} capture on {(char)(winingPawn.Col + 97)}{(char)(56 - winingPawn.Row)}.");
        }


        private static void BoardFill(char[,] chessboard, ref Pawn whitePawn, ref Pawn blackPawn)
        {
            for (int row = 0; row < chessboard.GetLength(0); row++)
            {
                var line = Console.ReadLine().ToCharArray();
                for (int col = 0; col < chessboard.GetLength(1); col++)
                {
                    chessboard[row, col] = line[col];
                    if (line[col] == 'w')
                    {
                        whitePawn = new Pawn(row, col, "White");
                        chessboard[row, col] = '-';
                    }

                    if (line[col] == 'b')
                    {
                        blackPawn = new Pawn(row, col, "Black");
                        chessboard[row, col] = '-';
                    }
                }
            }
        }
    }

    class Pawn
    {

        public Pawn(int row, int col, string color)
        {
            Row = row;
            Col = col;
            Color = color;
        }

        public int Row { get; set; }
        public int Col { get; set; }
        public string Color { get; set; }
    }
}
