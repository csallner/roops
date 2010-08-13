package test.roops.forbidden;

/**
 * Class that overrides the method java.lang.Object.equals(Object).
 * 
 * @author csallner@uta.edu (Christoph Csallner)
 */
public class OverridesEquals {

	@Override
	public boolean equals(Object obj) {
		return this == obj;
	}
}
