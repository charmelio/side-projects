/** Simple encryption and decryption program
*	@author Thomas Bishop
*/

import java.security.Security;
import javax.crypto.SecretKey;
import javax.crypto.Cipher;
import javax.crypto.KeyGenerator;
import javax.swing.JOptionPane;

public class simpleEncDec
{
	/* Hide the Key and Cipher from the rest of the JVM */
	private static SecretKey key = null;
	private static Cipher cipher = null;

	public static void main( String args[] ) throws Exception
	{
		/* Get password from user */
		String password = (String) JOptionPane.showInputDialog( null,
			"Please enter the password you'd like to encrypt:",
			"Encryption/Decryption",
			JOptionPane.DEFAULT_OPTION);

		/* Ask Sun for latest cryptograph */
		Security.addProvider( new com.sun.crypto.provider.SunJCE() );
		
		/* Generate variable key AES-128 */
		KeyGenerator keyGenerator = KeyGenerator.getInstance("AES");
		keyGenerator.init(128);
		SecretKey secretKey = keyGenerator.generateKey();
		cipher = Cipher.getInstance("AES/ECB/PKCS5Padding");

		/* Print to console the encrypt password and decrypted encryption */
		System.out.println( simpleEncryption(password, cipher, secretKey) );
		System.out.println( simpleDecryption(password, cipher, secretKey) );
	}

	private static String simpleEncryption( String password, Cipher cipher, SecretKey secretKey ) throws Exception
	{
		/* Convert text to raw data to encrypt */
		byte passwordBytes[] = password.getBytes("UTF8");
		cipher.init(Cipher.ENCRYPT_MODE, secretKey);
		byte cipherBytes[] = cipher.doFinal(passwordBytes);
		String cipherText = new String(cipherBytes, "UTF8");

		return cipherText;
	}

	private static String simpleDecryption( String cipherText, Cipher cipher, SecretKey secretKey ) throws Exception
	{
		/* Convert text to raw data to decrypt */
		byte cipherBytes[] = cipher.doFinal(cipherText.getBytes("UTF8"));
		cipher.init(Cipher.DECRYPT_MODE, secretKey);
		byte decryptedBytes[] = cipher.doFinal(cipherBytes);
		String decryptedText = new String(decryptedBytes, "UTF8");

		return decryptedText;
	}
}
