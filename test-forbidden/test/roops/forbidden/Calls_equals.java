package test.roops.forbidden;

/**
 * Example forbidden benchmark.
 * 
 * <p>This benchmark is illegal because it calls equals(Object).
 * 
 * @author csallner@uta.edu (Christoph Csallner)
 */
public class Calls_equals {
	

	public static boolean foo() 
	{
		boolean res = true;
		
		Object obj_obj = new Object();
		res &= obj_obj.equals(obj_obj); // INVOKEVIRTUAL java/lang/Object.equals(Ljava/lang/Object;)Z
						
		OverridesEquals own_own = new OverridesEquals();
		res &= own_own.equals(own_own);	// INVOKEVIRTUAL test/roops/forbidden/OverridesEquals.equals(Ljava/lang/Object;)Z
		
		Calls_equals self = new Calls_equals();
		res &= self.call_super(self);
		
		return res;
	}
	
	
	boolean call_super(Object object) {
		return super.equals(object);	// INVOKESPECIAL java/lang/Object.equals(Ljava/lang/Object;)Z
	}
}
