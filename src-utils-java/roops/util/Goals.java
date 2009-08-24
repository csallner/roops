package roops.util;

import java.lang.reflect.Member;
import java.util.BitSet;
import java.util.HashMap;
import java.util.Map;

/**
 * Counts the number of goals reached by a state-space
 * exploration system. Very basic.
 * 
 * @author csallner@uta.edu (Christoph Csallner)
 * @author mainul.islam@mavs.uta.edu (Mainul Islam)
 */
public class Goals {

	protected final static Map<Member, BitSet> memberGoalsReached =
		new HashMap<Member, BitSet>();
	protected final static BitSet reachedGoals = new BitSet();
	
	/**
	 * Reset all goals to "not reached"
	 */
	public static void resetGoals() {
		reachedGoals.clear();
	}
	
	/**
	 * Reset all goals to "not reached"
	 */
	public static void storeAndResetGoals(Member member) {
		if (member!=null)
			memberGoalsReached.put(member, (BitSet) reachedGoals.clone());

		reachedGoals.clear();
	}
	
	
	/**
	 * @return number of reached goals stored for member,
	 * else -1
	 */
	public static int getNrGoalsReached(Member member) {
		BitSet bitSet = memberGoalsReached.get(member);
		return (bitSet==null)? -1 : bitSet.cardinality(); 
	}
	
	/**
	 * Record that we reached goal number goalIndex.
	 * 
	 * @param goalIndex non-negative number
	 */
	public static void reached(int goalIndex) {
		reached(goalIndex, Verdict.UNKNOWN);
	}
	
	public static void reached(int goalIndex, Verdict reach) {
		reachedGoals.set(goalIndex);
	}
	
	/**
	 * @return the number of goals recorded as reached, since the last
	 * time resetGoals() was called.
	 */
	public static int getNrGoalsReached() {
		return reachedGoals.cardinality();
	}
}
