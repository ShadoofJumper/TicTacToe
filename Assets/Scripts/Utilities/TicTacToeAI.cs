using System;

namespace Utilities
{
    public static class TicTacToeAI
    {
        private static int _currentPlayerValue;
        private static int _currentOpponentValue;
        private static int _currentFreeValue;

        public static int GetBestMove(int[] board, int playerValue, int opponentValue, int freeValue)
        {
            _currentPlayerValue = playerValue;
            _currentOpponentValue = opponentValue;
            _currentFreeValue = freeValue;
            
            int bestMove = -1;
            int bestScore = int.MinValue;
        
            //iterate all cells, make move on them and after check score for this move, return best move
            for (int move = 0; move < 9; move++)
            {
                //check is move valid (free cell)
                if (IsMoveValid(board, move))
                {
                    int[] newBoard = CloneBoard(board);
                    MakeMove(newBoard, move, _currentPlayerValue);
                    int score = Minimax(newBoard, 0, false);
                
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = move;
                    }
                }
            }
        
            return bestMove;
        }

        private static int Minimax(int[] board, int depth, bool isMaximizing)
        {
            //check game over
            if (IsGameOver(board))
            {
                int score = Evaluate(board);
                return score;
            }
            
            //set start best score
            int bestScore = isMaximizing ? int.MinValue : int.MaxValue;
            //iterate all moves
            for (int move = 0; move < 9; move++)
            {
                if (IsMoveValid(board, move))
                {
                    int[] newBoard = CloneBoard(board);
                    int currentPlayer = isMaximizing ? _currentPlayerValue : _currentOpponentValue;
                    MakeMove(newBoard, move, currentPlayer);
                    
                    int score = Minimax(newBoard, depth + 1, !isMaximizing);
                    
                    if (isMaximizing)
                    {
                        bestScore = Math.Max(score, bestScore);
                    }
                    else
                    {
                        bestScore = Math.Min(score, bestScore);
                    }
                }
            }
            
            return bestScore;
        }

        private static bool IsMoveValid(int[] board, int move)
        {
            return (board[move] == _currentFreeValue);
        }

        private static int[] CloneBoard(int[] board)
        {
            int[] newBoard = new int[9];
            Array.Copy(board, newBoard, board.Length);
            return newBoard;
        }

        private static void MakeMove(int[] board, int move, int player)
        {
            board[move] = player;
        }

        private static bool IsGameOver(int[] board)
        {
            //game over when board full or win one side of the sides
            return IsBoardFull(board) || CheckWin(board, _currentPlayerValue) || CheckWin(board, _currentOpponentValue);
        }
        
        private static bool IsBoardFull(int[] board)
        {
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == ' ')
                {
                    return false;
                }
            }
            return true;
        }

        private static bool CheckWin(int[] board, int player)
        {
            //check horizontal and vertical lines
            for (int i = 0; i < 3; i++)
            {
                if ((board[(i*3) + 0] == player && board[(i*3)+ 1] == player && board[(i*3) + 2] == player) ||
                    (board[0 + i] == player && board[3 + i] == player && board[6 + i] == player))
                {
                    return true;
                }
            }

            //check diagonal
            if ((board[0] == player && board[4] == player && board[8] == player) ||
                (board[2] == player && board[4] == player && board[6] == player))
            {
                return true;
            }

            return false;
        }

        private static int Evaluate(int[] board)
        {
            if (CheckWin(board, _currentPlayerValue))
            {
                return 10;
            }
            else if (CheckWin(board, _currentOpponentValue))
            {
                return -10;
            }
            else
            {
                return 0;
            }
        }
    }
}
