package test.roops.forbidden;

import java.util.LinkedList;
import java.util.List;

/**
 * Example forbidden benchmark.
 * 
 * <p>This benchmark is illegal because it calls code from
 * java.util.
 * 
 * @author csallner@uta.edu (Christoph Csallner)
 */
public class Uses_java_util {
	
	public static boolean add(String s) 
	{
		List<String> list = new LinkedList<String>();
		list.add(s);
		return list.contains(s);
	}
	
	//TODO: more examples 
}
