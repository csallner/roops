package test.roops.forbidden;

/**
 * Example forbidden benchmark.
 * 
 * <p>This benchmark is illegal because it calls hashCode().
 * 
 * @author csallner@uta.edu (Christoph Csallner)
 */
public class Calls_hashCode {
	
	/**
	 * From the Jvm spec, it seems that the method reference that appears
	 * as an argument of invokevirtual may still need to be resolved to
	 * a super-class that actually declares the method:
	 * http://java.sun.com/docs/books/jvms/second_edition/html/Instructions2.doc6.html#invokevirtual
	 * 
	 * <p>This seems to be a feature, to allow modifications in a class hierarchy
	 * without requiring re-compilation. See:
	 * http://java.sun.com/docs/books/jls/second_edition/html/binaryComp.doc.html#44872
	 */
	public static int foo() 
	{
		int i = 0;
		
		Object obj_obj = new Object();
		i += obj_obj.hashCode();	// INVOKEVIRTUAL java/lang/Object.hashCode()I
		
		Object obj_non = new DoesNotOverride();
		i += obj_non.hashCode();	// INVOKEVIRTUAL java/lang/Object.hashCode()I
		
		DoesNotOverride non_non = new DoesNotOverride();
		i += non_non.hashCode();	// INVOKEVIRTUAL java/lang/Object.hashCode()I
		
		Object obj_own = new OverridesHashCode();
		i += obj_own.hashCode();	// INVOKEVIRTUAL java/lang/Object.hashCode()I
		
		/* Depending on the class actually loaded for OverridesHashCode, this call
		 * may also be dispatched to Object.hashCode(). */
		OverridesHashCode own_own = new OverridesHashCode();
		i += own_own.hashCode();	// INVOKEVIRTUAL test/roops/forbidden/OverridesHashCode.hashCode()I
		
		Calls_hashCode self = new Calls_hashCode();
		i += self.call_super();
		
		return i;
	}
	
	
	int call_super() {
		return super.hashCode();	// INVOKESPECIAL java/lang/Object.hashCode()I
	}
}
