/** Guessing Game v3
*	Users try to guess the random number to win
* 	Easiest way to win is to use the divide-and-conquer/binary search strategy

* 	Casino rules: Change int variables to float
*	@author Thomas Bishop -- October 2014
*/

import java.util.Random;
import javax.swing.JOptionPane;

public class GuessingGame3
{
	/**	Difficulty Parameters. 
	*	The equation for TOTAL_GUESSES calculates a fair number of guesses 
	*	based on the range; without the maintainer having to guess or do much work. 
	*	Example: MIN:1, MAX:10 => TOTAL_GUESSES: 4
	*/
	final static int MIN = 1,
					 MAX = 100,
		   			 TOTAL_GUESSES = (int)(Math.ceil( Math.log(MAX - MIN + 1) 
		   			 								/ Math.log(2) ));

	/** Boolean to determine if the player wants to keep playing or not.
	*/
	private static boolean keepPlaying = true;

	/** Main control loop on game to start another round or not.
	*	
	*	<pre>
	*	@param args <code>String[]</code> of the command-line arguments.
	*/
	public static void main( String args[] ) 
	{
		Random rnJesus = new Random();

		// Game control loop
		while( keepPlaying )
		{
			int answer = rnJesus.nextInt( (MAX - MIN) + 1) + MIN;
			String winLoseDialog = "", winLoseTitle = "";

			// Prompt user with gameplay interface and find out if they lost
			if ( !playingGame(answer) )
			{
					winLoseDialog = "You lost! The number was " + answer + "!\nContinue?";
					winLoseTitle = "Game Over!";
			}
			else
			{
					winLoseDialog = "You won!\nDo you want to play again?";
					winLoseTitle = "Congratulations!";

			}

			if ( JOptionPane.showConfirmDialog( null,
				 winLoseDialog, winLoseTitle,
				 JOptionPane.YES_NO_OPTION ) == JOptionPane.NO_OPTION )
					keepPlaying = false;
		}

		// User has decided to quit playing -- display exit message
		System.out.printf( "\n Thanks for playing! \n" );	
	}

	/** Game Control Function
	*	<pre>
	*	@param 	answer	The random number the user must guess correctly as type <code>int</code>.
	*	@return	boolean	If true, the user won. If false, the user lost or quit.
	*	@throws ParseException when a user inputs a non-numeric into the JOptionPane.
	*	</pre>
	*/
	static boolean playingGame( int answer )
	{
		int input;
		String msg = String.format("What is your guess? (%d - %d)", MIN, MAX);
		String hint = "", paneGuess = "";

		// A single round of the game's control loop
		for( int i = TOTAL_GUESSES; i >= 0; i-- )
		{
			// Did the user run out of guesses?
			if ( i <= 0 )
				return false;

			// Display input textbox, current hint and remaining guesses
			paneGuess = (String)JOptionPane.showInputDialog( null,
				hint + msg,
				i + " Guesses Remaining",
				JOptionPane.DEFAULT_OPTION);

			// Exit if user decides to stop playing
			if ( paneGuess == null )
			{
				System.out.printf( "\n Thanks for playing! \n" );
				System.exit(0);
			}

			// Test to make sure paneGuess is successfully converted
			try
			{
				input = Integer.parseInt( paneGuess );
			}
			catch( Exception e )
			{
				// Dont penalize user for invalid input
				hint = "Invalid Input! ";
				i++;
				continue;
			}

			// Exit loop if the user guessed correctly
			if ( compareTo(input, answer) == 0 )
				break;

			if ( compareTo(input, answer) > 0 )
				hint = "Too high! ";
			else
				hint = "Too low! ";
		}

		// Fall through fail safe
		return true;
	}

	/** Compare the guess to the user input. If positive, the guess was too high. If negative, the guess was too low. If 0, the guess was correct.
	*	
	*	<pre>
	*	@param 	a, b 	a, the user's input; b, the system's value to guess. Both are of type <code>int</code>.
	*	@return int 	The difference between the two parameters. 
	*	@throws			No excpetions expected.
	*	</pre>
	*/
	static int compareTo( int a, int b )
	{
		return (a - b);
	}
}
