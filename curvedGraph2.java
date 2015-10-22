/* Paint a curved graph and adjust it's size when scrolling the mouse wheel 
	or when adjusting hte size of the screen its in
	@author Thomas Bishop 
*/

import java.awt.Graphics;
import java.awt.Graphics2D;
import java.awt.RenderingHints;

import java.awt.event.ComponentEvent;
import java.awt.event.ComponentListener;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;
import java.awt.event.MouseWheelEvent;
import java.awt.event.MouseWheelListener;

import javax.swing.JFrame;
import javax.swing.JPanel;

public class curvingGraph2 extends JFrame
{
	final static int RESET_LINES = 50;		  
		  static int width = 800;
		  static int height = 600;
		  static int numLines = RESET_LINES;

	public static void main( String args[] )
	{	
		// Need to get a better understanding of having a Runnable method call like this
		new curvingGraph2();
	}

	public curvingGraph2()
	{
		JPanel panel = new JPanel();
		setSize( width, height );
		add( panel );
		setResizable( true );
		setDefaultCloseOperation( JFrame.EXIT_ON_CLOSE );	

		// Lambda MouseWheelMoved event
		addMouseWheelListener((e) -> {
			int notches = e.getWheelRotation();

			if ( notches < 0 )
				numLines++;
			else
			{
				if ( numLines > 1 )
					numLines--;
			}
			
			repaint();
		});

		addMouseListener( new MouseListener ()
		{
			public void mouseExited( MouseEvent e ) {}
			public void mouseEntered( MouseEvent e ) {}
			public void mouseClicked( MouseEvent e ) {}
			public void mouseReleased( MouseEvent e ) {}

			@Override
			public void mousePressed( MouseEvent e )
			{
				if ( e.getButton() == MouseEvent.BUTTON2 )
				{
					numLines = RESET_LINES;
					repaint();
				}
			}
		});

		addComponentListener( new ComponentListener() 
		{
			@Override
			public void componentResized( ComponentEvent e )
			{
				height = getBounds().getSize().height;
				width = getBounds().getSize().width;
				setSize( width, height );
				repaint();
			}

			public void componentMoved( ComponentEvent e ) {}
			public void componentShown( ComponentEvent e ) {}
			public void componentHidden( ComponentEvent e ) {}
		});

		setVisible( true );
	}

	// TODO: Put in better screen clear method
	public void paint( Graphics g )
	{
		int x = 0, y = 0;
		Graphics2D g2d = (Graphics2D) g;
		
		g2d.setRenderingHint(RenderingHints.KEY_ANTIALIASING, 
							 RenderingHints.VALUE_ANTIALIAS_ON);

		g2d.clearRect( 0, 0, width, height );

		for( int i = 0; i < numLines; i++ )
		{
			x = ( width / numLines ) * i;
			y = height - ( height / numLines ) * i;
			g2d.drawLine( x, height, width, y );
		}
	}
}
