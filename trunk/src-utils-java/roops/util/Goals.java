package roops.util;

import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;

/**
 * Counts the number of goals reached by a state-space
 * exploration system. Very basic.
 * 
 * @author csallner@uta.edu (Christoph Csallner)
 * @author mainul.islam@mavs.uta.edu (Mainul Islam)
 */
public class Goals {

	protected final static String NL = System.getProperty("line.separator");
	protected static void newline(StringBuilder sb) {
		sb.append(NL);
	}
	
  /**
   * className --> (methodName --> goal*))
   */
  private final static Map<String, Map<String, Set<ReachedGoal>>> reachedGoals =
    new HashMap<String, Map<String, Set<ReachedGoal>>>();
  	
	/**
	 * Reset all goals to "not reached"
	 */
	public static void resetGoals() {
		reachedGoals.clear();
	}		
	
	
	/**
	 * Record that we reached goal number goalIndex.
	 * 
	 * @param goalIndex non-negative number
	 */
	public static void reached(int goalIndex) {
		reachedInternal(goalIndex, Verdict.UNKNOWN);
	}
		
	public static void reached(int goalIndex, Verdict reach) {
	  reachedInternal(goalIndex, reach);
	}
	
	protected static void reachedInternal(int goalIndex, Verdict reach) {
    StackTraceElement[] stack = Thread.currentThread().getStackTrace();
    if (stack.length<4) {
      System.out.println("Warning: could not record reached goal.");
      return;
    }
    
    String className = stack[3].getClassName(); 
    String methodName = stack[3].getMethodName();
    int lineNr = stack[3].getLineNumber();
    if (className==null || methodName==null) {
      System.out.println("Warning: could not record reached goal.");
      return;
    }
    if (lineNr < 0)
      System.out.println("Warning: cannot distinguish overloading.");
    
    ReachedGoal reachedGoal = new ReachedGoal(
        className, methodName, lineNr, goalIndex);
    
    Map<String,Set<ReachedGoal>> classMembers = reachedGoals.get(className);
    if (classMembers==null) {
      classMembers = new HashMap<String,Set<ReachedGoal>>();
      reachedGoals.put(className, classMembers);
    }
    
    Set<ReachedGoal> methodGoals = classMembers.get(methodName);
    if (methodGoals==null) {
      methodGoals = new HashSet<ReachedGoal>();
      classMembers.put(methodName, methodGoals);
    }
    
    methodGoals.add(reachedGoal);
	}
	
	/**
	 * @param className fully qualified class name as returned by
	 * {@link StackTraceElement#getClassName()}.
	 * 
	 * @return number of distinct goals reached in className, 
	 * since last call to {@link #resetGoals()}.
	 */
	public static int getNrReachedGoals(String className) {
	  int classGoalCount = 0;
	  Map<String,Set<ReachedGoal>> classGoals = reachedGoals.get(className);
	  if (classGoals != null)
	  	for (Set<ReachedGoal> methodGoals: classGoals.values())
	  		classGoalCount += methodGoals.size();
	  
	  return classGoalCount;
	}


	public static String printReachedGoals() {
	  final StringBuilder sb = new StringBuilder();
	  for (Map<String,Set<ReachedGoal>> classGoals: reachedGoals.values())
	    for (Set<ReachedGoal> methodGoals: classGoals.values())
	      for (ReachedGoal goal: methodGoals) {
	        sb.append(goal);
	        newline(sb);
	      }
	  
	  return sb.toString();
	}
	
	public static String printReachedGoalsSummary() {
	  final StringBuilder sb = new StringBuilder();
	  int totalGoalCount = 0;
	  for (Map<String,Set<ReachedGoal>> classGoals: reachedGoals.values()) {
	  	String className = "";
	  	int classGoalCount = 0;
	  
	    for (Set<ReachedGoal> methodGoals: classGoals.values()) {
	    	ReachedGoal firstGoal = methodGoals.iterator().next();
	    	className = firstGoal.className;
	      sb.append(className+" "+firstGoal.methodName+" "+methodGoals.size());
	      newline(sb);
	      classGoalCount += methodGoals.size();
	    }
	    
	    sb.append(className+" "+classGoalCount);
	    newline(sb);
	    totalGoalCount += classGoalCount;
	  }
	  sb.append("total "+totalGoalCount);
	  newline(sb);
	  return sb.toString();		
	}
}
