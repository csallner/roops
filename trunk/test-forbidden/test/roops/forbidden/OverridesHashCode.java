package test.roops.forbidden;

/**
 * Class that overrides the method java.lang.Object.hashCode().
 * 
 * @author csallner@uta.edu (Christoph Csallner)
 */
public class OverridesHashCode {

	@Override
	public int hashCode() {
		return -1;
	}
}
