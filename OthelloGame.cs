#nullable enable


using System;
using static System.Console;

namespace Bme121
{
    //record Player( string Colour, string Symbol, string Name );
    
    class Player
    {
		
        public readonly string Colour;
        public readonly string Symbol;
        public readonly string Name;
        
        public Player( string Colour, string Symbol, string Name )
        {
          this.Colour = Colour;
          this.Symbol = Symbol;
          this.Name = Name;
        }
    }
    
    static partial class Program
    {
        // Display common text for the top of the screen.
        public static int moveX;
        public static int moveY;
		
        static void Welcome( )
        {
			WriteLine( "Welcome to Othelo!" );
			
        }
        
        // Collect a player name or default to form the player record.

        
        static Player NewPlayer( string colour, string symbol, string defaultName )
        {
			Write( $"{colour} - what is your name?: ");
			string player = ReadLine( );
			
			WriteLine( );
            return new Player( colour , symbol, player );
        }
        
        // Determine which player goes first or default.
        
        static int GetFirstTurn( Player[ ] players, int defaultFirst )
        {
			Write( "Who goes first? ( 1 for white, 0 for black ): " );
			int firstTurn = int.Parse( ReadLine( ) );
			
            return firstTurn;
            WriteLine( );
        }
        
        // Get a board size (between 4 and 26 and even) or default, for one direction.
        
        static int GetBoardSize( string direction, int defaultSize )
        {
			WriteLine( );
			Write( "What is your desired boardsize?: ");
			int boardSize = int.Parse( ReadLine() );
			if( boardSize < 4 || boardSize > 26 || boardSize % 2 != 0)
			{
				return defaultSize;
				WriteLine( );
			}
			else
			{
				return boardSize;
				WriteLine( );
			}
			WriteLine( "Type 'Over' to end the game if no more possible moves. " );
        }
        
        // Get a move from a player.
        
        static string GetMove( Player player )
        {
			
			Write( $"{player.Name} ({player.Colour}) enter your move (coordinates on board): " );
			string move = ReadLine( );
			
			return move;
			
        }
        
        // Try to make a move. Return true if it worked.
        
        static bool TryMove( string[ , ] board, Player player, string move )
        {
			moveX = IndexAtLetter( move[0].ToString( ) );
			moveY = IndexAtLetter( move[1].ToString( ) );
			
			if( moveX < board.GetLength( 0 )  && moveY < board.GetLength( 1 ) )
			{
				if( move.Length == 2 && board[ moveX, moveY ] == " " )
				{
					if( TryDirection( board, player, move ) )
					{
						board[ moveX, moveY ] = player.Symbol;
						return true;
					}
					else
					{
						return false;
					}
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
			if( move == "skip" || move == "quit" )
			{
				return true;
			}
			
        }
        
        
        // Do the flips along a direction specified by the row and column delta for one step.
        
        static bool TryDirection( string[ , ] board, Player player, string move )
        
        //int moveRow, int deltaRow, int moveCol, int deltaCol, 
        
        {
			moveX = IndexAtLetter( move[0].ToString( ) );
			moveY = IndexAtLetter( move[1].ToString( ) );
		
			bool validMove = false;
						
			for( int i = moveY + 1; i < board.GetLength( 0 ); i ++ )
			{
				
				if( board[ moveX, moveY + 1] == player.Symbol )
				{
					break;
				}
				else if( board[ moveX, i] == " " )
				{
					break;
				}
				else if( board[ moveX, i ] == player.Symbol )
				{
					
					for( int j = moveY + 1; j < i; j ++ )
					{
						board[ moveX, j ] = player.Symbol;
					}
					validMove = true;
				}
			}
			
			for( int i = moveY - 1; i >= 0; i -- )
			{
				if( board[ moveX, moveY - 1] == player.Symbol )
				{
					break;
				}
				else if( board[ moveX, i] == " " )
				{
					break;
				}
				else if( board[ moveX, i ] == player.Symbol )
				{
					for( int j = moveY - 1; j >= i; j -- )
					{
						board[ moveX, j ] = player.Symbol;
					}
					validMove = true;
				}
			}
		
			for( int i = moveX + 1; i < board.GetLength( 1 ); i ++ )
			{
				if( board[ moveX + 1, moveY] == player.Symbol )
				{
					break;
				}
				else if( board[ i, moveY] == " " )
				{
					break;
				}
				else if( board[ i, moveY ] == player.Symbol )
				{
					for( int j = moveX + 1; j < i; j ++ )
					{
						board[ j, moveY ] = player.Symbol;
					}
					validMove = true;
					
				}
			}
			
			for( int i = moveX - 1; i >= 0; i -- )
			{
				if( board[ moveX - 1, moveY] == player.Symbol )
				{
					break;
				}
				else if( board[ i, moveY] == " " )
				{
					break;
				}
				else if( board[ i, moveY ] == player.Symbol )
				{
					for( int j = moveX - 1; j >= i; j -- )
					{
						board[ j, moveY ] = player.Symbol;
					}
					validMove = true;
					
				}
			}
			
			//move to top right
			
			int a = 1;
			int moves = 0;
			
			int steps = Math.Min( board.GetLength( 0 ) - moveY, moveX );
			//for (i = moveY + 1; i < board.GetLength( 0 ); i++)
			while( steps > 0 )
			{
				if( moveY + a > board.GetLength( 0 ) - 1|| moveX - a < 0 )
				{
					break;
				}
				if( board[ moveX - 1, moveY + 1 ] == player.Symbol )
				{
					break;
				}
				else if( board[ moveX - a, moveY + a] == " " )
				{
					break;
				}
				else if( board[ moveX - a, moveY + a ] == player.Symbol )
				{
					int b = 1;
					while( moves > 0 )
					{
						board[ moveX - b, moveY + b ] = player.Symbol;
						moves --;
						b ++;
					}
					validMove = true;
				}
				else
				{
					moves++;
				}
				steps--;
				a ++;
			}
			
			//move to bottom left
			a = 1;
			moves = 0;
			
			steps = Math.Min( moveY, board.GetLength( 1 ) - moveX );
			while( steps > 0 )
			{
				if( moveY - a < 0 || moveX + a > board.GetLength( 1 ) - 1)
				{
					break;
				}
				if( board[ moveX + 1, moveY - 1 ] == player.Symbol )
				{
					break;
				}
				else if( board[ moveX + a, moveY - a] == " " )
				{
					break;
				}
				else if( board[ moveX + a, moveY - a ] == player.Symbol )
				{
					int b = 1;
					while( moves > 0 )
					{
						board[ moveX + b, moveY - b ] = player.Symbol;
						moves --;
						b ++;
					}
					validMove = true;
				}
				else
				{
					moves++;
				}
				steps--;
				a ++;
			}
			
			//move to top left
			a = 1;
			moves = 0;
			
			steps = Math.Min( moveY, moveX );
			while( steps > 0 )
			{
				if( moveY - a < 0 || moveX - a < 0 )
				{
					break;
				}
				if( board[ moveX - 1, moveY - 1 ] == player.Symbol )
				{
					break;
				}
				else if( board[ moveX - a, moveY - a] == " " )
				{
					break;
				}
				else if( board[ moveX - a, moveY - a ] == player.Symbol )
				{
					int b = 1;
					while( moves > 0 )
					{
						board[ moveX - b, moveY - b ] = player.Symbol;
						moves --;
						b ++;
					}
					validMove = true;
				}
				else
				{
					moves++;
				}
				steps--;
				a ++;
			}
			
			//move to bottom right 
			a = 1;
			moves = 0;
			
			steps = Math.Min( board.GetLength( 0 ) - moveY, board.GetLength( 1 ) - moveX );
			while( steps > 0 )
			{
				if( moveY + a > board.GetLength( 0 ) - 1 || moveX + a > board.GetLength( 1 ) - 1)
				{
					break;
				}
				if( board[ moveX + 1, moveY + 1 ] == player.Symbol )
				{
					break;
				}
				else if( board[ moveX + a, moveY + a] == " " )
				{
					break;
				}
				else if( board[ moveX + a, moveY + a ] == player.Symbol )
				{
					int b = 1;
					while( moves > 0 )
					{
						board[ moveX + b, moveY + b ] = player.Symbol;
						moves --;
						b ++;
					}
					validMove = true;
				}
				else
				{
					moves++;
				}
				steps--;
				a ++;
			}
			
			return validMove;
			
			
			
			
        }
        
        // Count the discs to find the score for a player.
        
        static int GetScore( string[ , ] board, Player player )
        {
			int count = 0;
			
			for( int i = 0; i < board.GetLength( 0 ); i ++ )
			{
				for (int j = 0; j < board.GetLength( 1 ); j++)
				{
					if( board[ i, j ] == player.Symbol )
					{
						count++;
					}
				}
			}
			WriteLine( count );
			return count;
			}
        
        // Display a line of scores for all players.
        
        static void DisplayScores( string[ , ] board, Player[ ] players )
        {
			WriteLine( "White Score: " + GetScore( board, players[ 1 ] ) );
			WriteLine( "Black Score: " + GetScore( board, players[ 0 ] ) );
        }
        
        // Display winner(s) and categorize their win over the defeated player(s).
        
        static void DisplayWinners( string[ , ] board, Player[ ] players )
        {
			if( GetScore( board, players [ 1 ] ) > GetScore( board, players[ 0 ] ) )
			{
				WriteLine( "WHITE WINS!!!" );
			}
			else if( GetScore( board, players [ 0 ] ) > GetScore( board, players[ 1 ] ) )
			{
				WriteLine( "BLACK WINS!!!" );
			}
			else
			{
				WriteLine( "TIE!!!" );
			}
        }
        
        
        static void Main( )
        {
            // Set up the players and game.
            // Note: I used an array of 'Player' objects to hold information about the players.
            // This allowed me to just pass one 'Player' object to methods needing to use
            // the player name, colour, or symbol in 'WriteLine' messages or board operation.
            // The array aspect allowed me to use the index to keep track or whose turn it is.
            
            Welcome( );
            
            Player[ ] players = new Player[ ] 
            {
                NewPlayer( colour: "black", symbol: "X", defaultName: "Black" ),
                NewPlayer( colour: "white", symbol: "O", defaultName: "White" ),
            };
            
            int turn = GetFirstTurn( players, defaultFirst: 0 );
           
            int rows = GetBoardSize( direction: "rows",    defaultSize: 8 );
			
			int cols = rows;

            string[ , ] game = NewBoard( rows, cols );
            
            // Play the game.
            
            bool gameOver = false;
            while( ! gameOver )
            {
                DisplayBoard( game ); 
                DisplayScores( game, players );
                
                string move = GetMove( players[ turn ] );
                
                int countGame = 0;
               
				
				if( move == "quit" ) gameOver = true;
				
				if( move == "Over" ) gameOver = true;
				
                else
                {
                    bool madeMove = TryMove( game, players[ turn ], move );
                    
                    for( int i = 0; i < game.GetLength( 0 ); i ++ )
					{
						for (int j = 0; j < game.GetLength( 1 ); j++)
						{
							if( game[ i, j ] != " " )
							//( !TryMove( game, players[ 0 ], LetterAtIndex(i) + LetterAtIndex(j) ) && !TryMove( game, players[1], LetterAtIndex(i) + LetterAtIndex(j) ) )
							{
								countGame ++;
							}
							
						}
					}
					
					if( countGame == game.Length )
					{
						gameOver = true;
					}
					else
					{
						countGame = 0;
					}
					
                    if( madeMove ) turn = ( turn + 1 ) % players.Length;
                    else 
                    {
                        Write( " Your choice didn't work!" );
                        Write( " Press <Enter> to try again." );
                        ReadLine( ); 
                    }
                }
            }
            
            // Show fhe final results.
            
            DisplayWinners( game, players );
            WriteLine( );
        }
    }
}


