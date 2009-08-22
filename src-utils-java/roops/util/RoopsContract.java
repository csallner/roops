package roops.util;

/**
 * Helper class to check boolean-valued expressions.
 * 
 * @author csallner@uta.edu (Christoph Csallner)
 */
public class RoopsContract
{
	public static void assume(boolean b)
	{
		if (!b)
			throw new IllegalStateException("Assume violated");
	}
}
