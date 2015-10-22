/*
	Create a guessing program that starts its guess at half way through the range.
	Ask the user if the computer guessed too high or too low, or if correct.
	Make the guess use a binary search to zero in on the correct answer.

    @author Thomas Bishop
    @TODO: Add counter for guesses and cheat checker
*/
#include <stdio.h>
#include <string.h>

int main(int argc, char const *argv[])
{
    /* Range of guesses
     *   MAXIMUM needs to be one greater than what you want */
	const int MINIMUM = 0,
			  MAXIMUM = 101;

	int guess = ((MAXIMUM - MINIMUM) / 2) + MINIMUM;
	int tmp_min = MINIMUM;
	int tmp_max = MAXIMUM;
	char userInput[10];

    /* Display Instructions to Play */
	printf("********************** Guessing Game **********************\n");
	printf("*  Instructions:                                          *\n");
	printf("*      The computer will \"guess\" a number!                *\n");
	printf("*      You, as the user, will tell the computer whether   *\n");
	printf("*      it's guess was correct, too high or too low.       *\n");
    printf("*                                                         *\n");
	printf("*      If its guess is too high, type in: HIGHER          *\n");
	printf("*      If its guess is too low, type in: LOWER 		  *\n");
	printf("*      If its guess was correct, type in: CORRECT         *\n");
	printf("***********************************************************\n\n");

	printf("Computer: My guess is %d\nUser: ", guess);
	
	/* Iterative method of binary search is faster than recursive method */
	while( 1 )
	{
		fgets( userInput, sizeof(userInput), stdin );

        /* Ignore case of users input */
		if ( strcasecmp( userInput, "HIGHER\n" ) == 0 )
		{
			tmp_max = tmp_max;
			tmp_min = guess;
			guess = ((tmp_max - tmp_min) / 2) + tmp_min;
			printf("Computer: My guess is %d\nUser: ", guess);
			continue;
		}

		if ( strcasecmp( userInput, "LOWER\n" ) == 0 )
		{
			tmp_max = guess;
			tmp_min = tmp_min;
			guess = ((tmp_max - tmp_min) / 2) + tmp_min;
			printf("Computer: My guess is %d\nUser: ", guess);
			continue;
		}

		if ( strcasecmp( userInput, "CORRECT\n" ) == 0 )
		{
			printf("Computer: Ha! I'm so good at guessing! Good game, user.\n");
			break;
		}

        /* Catch user putting in something not defined */
		printf("Computer: I don't understand what you entered!\nUser: ");
	}

	return 0;
}
