package roops.util;

import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;

/**
 * Count of goals in the method body, regardless
 * of their reachability or reachability annotation.
 * 
 * @author csallner@uta.edu (Christoph Csallner)
 */
@Retention(RetentionPolicy.RUNTIME)
public @interface NrOfGoals {

	public int value();

}
