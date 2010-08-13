package test.roops.forbidden;

/**
 * Example forbidden benchmark.
 * 
 * <p>This benchmark is illegal because it operates on long values.
 * 
 * @author csallner@uta.edu (Christoph Csallner)
 */
public class Uses_long {
	

	public static long add(long a, long b) 
	{
		return a + b;
	}
	
	public static long or(long a, long b) 
	{
		return a | b;
	}	
	
	public static long shift(long a, long b) 
	{
		return a >> b << a;
	}		
	
	//TODO: more examples 
}
