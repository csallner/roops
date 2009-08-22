package roops.util;

import java.lang.annotation.ElementType;
import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;
import java.lang.annotation.Target;

/**
 * Benchmark entry point. 
 * 
 * This annotation can only be applied to a method. Roops
 * does not allow constructors as entry points. 
 * 
 * @author csallner@uta.edu (Christoph Csallner)
 */
@Target(ElementType.METHOD)
@Retention(RetentionPolicy.RUNTIME)
public @interface BenchmarkMethod {

}
