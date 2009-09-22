package roops.util;

/**
 * A goal reached during execution.
 * 
 * @author csallner@uta.edu (Christoph Csallner)
 */
public class ReachedGoal {
  protected final String className;
  protected final String methodName;
  protected final int goalId;
  protected final int lineNr; // hack to distinguish overloaded methods
  
  /**
   * Constructor
   */
  public ReachedGoal(String className, String methodName, int lineNr, int goalId) {
    if (className==null || methodName==null)
      throw new NullPointerException();
    this.className = className;
    this.methodName = methodName;
    this.lineNr = lineNr;
    this.goalId = goalId;
  }
  
  @Override
  public boolean equals(Object obj) {
    if (!(obj instanceof ReachedGoal)) 
      return false;
      
    return equals((ReachedGoal) obj);
  }
  
  public boolean equals(ReachedGoal other) {
    return 
      (className.equals(other.className)) && 
      (methodName.equals(other.methodName)) &&
      lineNr==other.lineNr &&
      goalId==other.goalId;
  }
  
  @Override
  public int hashCode() {
    return methodName.hashCode();
  }
  
  @Override
  public String toString() {
    return className+" "+methodName+" "+lineNr+" "+goalId;
  }
}