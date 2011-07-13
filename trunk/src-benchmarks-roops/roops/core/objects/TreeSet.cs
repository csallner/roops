//$category roops.core.objects

//Authors: Marcelo Frias
//$benchmarkclass
/**
 * @Invariant ( this.RED==false ) && 
 *		( this.BLACK==true ) &&
 *		( this.root.parent in null ) &&
 *		( this.root!=null => this.root.color = this.BLACK ) && 
 *		( all n: TreeSetEntry | n in this.root.*(left @+ right @+ parent) @- null => ( 
 *				(n.key != null ) &&
 *				( n.left != null => n.left.parent = n ) &&
 *				( n.right != null => n.right.parent = n ) &&
 *				( n.parent != null => n in n.parent.(left @+ right) ) &&
 *				( n !in n.^parent ) &&
 *				( all x : TreeSetEntry | (( x in n.left.^(left @+ right) @+ n.left @- null ) => ( n.key > x.key )) ) &&
 *				( all x : TreeSetEntry | (( x in n.right.^(left @+ right) @+ n.right @- null ) => ( x.key > n.key ))) &&
 *				( n.color = this.RED && n.parent != null => n.parent.color = this.BLACK ) && 
 *				( ( n.left=null && n.right=null ) => ( n.blackHeight=1 ) ) &&
 *				( n.left!=null && n.right=null => ( 
 *				      ( n.left.color = this.RED ) && 
 *				      ( n.left.blackHeight = 1 ) && 
 *				      ( n.blackHeight = 1 )  
 *				)) &&
 *				( n.left=null && n.right!=null =>  ( 
 *				      ( n.right.color = this.RED ) && 
 *				      ( n.right.blackHeight = 1 ) && 
 *				      ( n.blackHeight = 1 ) 
 *				 )) && 
 *				( n.left!=null && n.right!=null && n.left.color=this.RED && n.right.color=this.RED => ( 
 *				        ( n.left.blackHeight = n.right.blackHeight ) && 
 *				        ( n.blackHeight = n.left.blackHeight ) 
 *				)) && 
 *				( n.left!=null && n.right!=null && n.left.color=this.BLACK && n.right.color=this.BLACK => ( 
 *				        ( n.left.blackHeight=n.right.blackHeight ) && 
 *				        ( n.blackHeight=n.left.blackHeight + 1 )  
 *				)) && 
 *				( n.left!=null && n.right!=null && n.left.color=this.RED && n.right.color=this.BLACK => ( 
 *				        ( n.left.blackHeight=n.right.blackHeight + 1 ) && 
 *				        ( n.blackHeight = n.left.blackHeight  )  
 *				)) && 
 *				( n.left!=null && n.right!=null && n.left.color=this.BLACK && n.right.color=this.RED => ( 
 *				        ( n.right.blackHeight=n.left.blackHeight + 1 ) && 
 *				        ( n.blackHeight = n.right.blackHeight  )  )) 
 *				)) ; 
 */
public class TreeSet {

	//$goals 27
	//$benchmark	
	public void addTest(TreeSet treeSet, int aKey) {
		if (treeSet!=null && treeSet.repOK()) {
		  boolean ret_val = treeSet.add(aKey);
		}
	}

	//$goals 5
	//$benchmark
	public void containsTest(TreeSet treeSet, int aKey) {
		if (treeSet!=null && treeSet.repOK()) {
		  boolean ret_val = treeSet.contains(aKey);
		}
	}

	//$goals 33
	//$benchmark
	public void removeTest(TreeSet treeSet, int aKey) {
		if (treeSet!=null && treeSet.repOK()) {
		  boolean ret_val = treeSet.remove(aKey);
		}
	}

	public /*@ nullable @*/ TreeSetEntry root = null;

	/**
	 * The number of entries in the tree
	 */
	public  int size = 0;

	/**
	 * The number of structural modifications to the tree.
	 */
	public  int modCount = 0;

	public /*static final */ boolean RED = false;
	public /*static final */ boolean BLACK = true;




	public boolean contains(int aKey) {
		return getEntry(aKey) != null;
	}

	private TreeSetEntry getEntry_remove(int key) {
		TreeSetEntry p = root;
		while (p != null) {

			{/*$goal 0 reachable*/}
			
			if (key == p.key) {
				
				{/*$goal 1 reachable*/}
				return p;
			} else if (key < p.key) {
				
				{/*$goal 2 reachable*/}
				p = p.left;
			} else {
				
				{/*$goal 3 reachable*/}
				p = p.right;
			}
		}
		{/*$goal 4 reachable*/}
		return null;
	}



	private TreeSetEntry getEntry(int key) {
		TreeSetEntry p = root;
		while (p != null) {

			{/*$goal 0 reachable*/}
			
			if (key == p.key) {
				
				{/*$goal 1 reachable*/}
				return p;
			} else if (key < p.key) {
				
				{/*$goal 2 reachable*/}
				p = p.left;
			} else {
				
				{/*$goal 3 reachable*/}
				p = p.right;
			}
		}
		{/*$goal 4 reachable*/}
		return null;
	}


	private void init_TreeSetEntry(TreeSetEntry entry, int new_key, TreeSetEntry new_parent) {
		entry.color = false;
		entry.left = null;
		entry.right = null;
		entry.key = new_key;
		entry.parent = new_parent;
	}
	

	public boolean add(int aKey) {
		TreeSetEntry t = root;

		if (t == null) {
			incrementSize();
			root = new TreeSetEntry();
			init_TreeSetEntry(root, aKey, null);
			
			{/*$goal 1 reachable*/}
			return false;
		}

		boolean boolean_true= true;
		while (boolean_true) {
			
			if (aKey == t.key) {
				
				{/*$goal 2 reachable*/}
				return true;
			} else if (aKey < t.key) {
				
				if (t.left != null) {
					
					{/*$goal 3 reachable*/}
					t = t.left;
				} else {
					
					{/*$goal 4 reachable*/}
					incrementSize();
					t.left = new TreeSetEntry();
					init_TreeSetEntry(t.left, aKey, t);
					
					fixAfterInsertion(t.left);
					
					{/*$goal 5 reachable*/}
					return false;
				}
			} else { // cmp > 0
				
				{/*$goal 6 reachable*/}
				
				if (t.right != null) {
					
					{/*$goal 7 reachable*/}
					t = t.right;
				} else {
					
					{/*$goal 8 reachable*/}
					incrementSize();
					t.right = new TreeSetEntry();
					init_TreeSetEntry(t.right, aKey, t);
					fixAfterInsertion(t.right);
					
					{/*$goal 9 reachable*/}
					return false;
				}
			}
		}
		{/*$goal 26 unreachable*/}
		return false;
	}

	private void incrementSize() {
		{/*$goal 0 reachable*/}
		modCount++;
		size++;
	}

	/**
	 * Balancing operations.
	 *
	 * Implementations of rebalancings during insertion and deletion are
	 * slightly different than the CLR version.  Rather than using dummy
	 * nilnodes, we use a set of accessors that deal properly with null.  They
	 * are used to avoid messiness surrounding nullness checks in the main
	 * algorithms.
	 */

	private static boolean colorOf(TreeSetEntry p) {
		boolean black = true;
		boolean result ;
		if (p==null)
			result =black;
		else
			result =p.color;
		return result;
	}

	private static TreeSetEntry parentOf(TreeSetEntry p) {
		TreeSetEntry result;
		if (p == null)
			result = null;
		else
			result = p.parent;
		
		return result;
	}

	private static void setColor(TreeSetEntry p, boolean c) {
		if (p != null)
			p.color = c;
	}

	private static TreeSetEntry leftOf(TreeSetEntry p) {
		TreeSetEntry result ;
		if (p == null)
			result = null;
		else
			result = p.left;
		return result;
	}

	private static TreeSetEntry rightOf(TreeSetEntry p) {
		TreeSetEntry result;
		if (p == null) 
			result = null;
		else
			result = p.right;
		return result;
	}

	/** From CLR **/
	private void rotateLeft(TreeSetEntry p) {
		TreeSetEntry r = p.right;
		p.right = r.left;
		if (r.left != null)
			r.left.parent = p;
		r.parent = p.parent;
		if (p.parent == null)
			root = r;
		else if (p.parent.left == p)
			p.parent.left = r;
		else
			p.parent.right = r;
		r.left = p;
		p.parent = r;
	}

	/** From CLR **/
	private void rotateRight(TreeSetEntry p) {
		TreeSetEntry l = p.left;
		p.left = l.right;
		if (l.right != null)
			l.right.parent = p;
		l.parent = p.parent;
		if (p.parent == null)
			root = l;
		else if (p.parent.right == p)
			p.parent.right = l;
		else
			p.parent.left = l;
		l.right = p;
		p.parent = l;
	}

	/** From CLR **/
	private void fixAfterInsertion(final TreeSetEntry entry) {
		TreeSetEntry x = entry;
		x.color = RED;

		while (x != null && x != root && x.parent.color == RED) {
			
			{/*$goal 10 reachable*/}
			
			if (parentOf(x) == leftOf(parentOf(parentOf(x)))) {
				
				{/*$goal 11 reachable*/}
				TreeSetEntry y = rightOf(parentOf(parentOf(x)));
				if (colorOf(y) == RED) {
					
					{/*$goal 12 reachable*/}
					setColor(parentOf(x), BLACK);
					setColor(y, BLACK);
					setColor(parentOf(parentOf(x)), RED);
					x = parentOf(parentOf(x));
				} else {
					
					{/*$goal 13 reachable*/}
					if (x == rightOf(parentOf(x))) {
						
						{/*$goal 14 reachable*/}
						x = parentOf(x);
						rotateLeft(x);
					} else {
						{/*$goal 15 reachable*/}
					}
					setColor(parentOf(x), BLACK);
					setColor(parentOf(parentOf(x)), RED);
					if (parentOf(parentOf(x)) != null) {
						{/*$goal 16 reachable*/}
						rotateRight(parentOf(parentOf(x)));
					} else {
						{/*$goal 17 unreachable*/} //source: CLR
					}
				}
			} else {
				
				{/*$goal 18 reachable*/}
				TreeSetEntry y = leftOf(parentOf(parentOf(x)));
				if (colorOf(y) == RED) {
					
					{/*$goal 19 reachable*/}
					setColor(parentOf(x), BLACK);
					setColor(y, BLACK);
					setColor(parentOf(parentOf(x)), RED);
					x = parentOf(parentOf(x));
				} else {
					
					{/*$goal 20 reachable*/}
					if (x == leftOf(parentOf(x))) {
						
						{/*$goal 21 reachable*/}
						x = parentOf(x);
						rotateRight(x);
					} else {
						{/*$goal 22 reachable*/}
					}
					setColor(parentOf(x), BLACK);
					setColor(parentOf(parentOf(x)), RED);
					if (parentOf(parentOf(x)) != null) {
						{/*$goal 23 reachable*/}
						rotateLeft(parentOf(parentOf(x)));
					} else {
						{/*$goal 24 unreachable*/} //source: CLR
					}
					
				}
			}
		}
		{/*$goal 25 reachable*/}
		root.color = BLACK;
	}


	
	
	public boolean remove(int aKey) {
		TreeSetEntry p = getEntry_remove(aKey);
		if (p == null) {
			{/*$goal 5 reachable*/}
			return false;
		}
		
		
		deleteEntry(p);
		
		{/*$goal 32 reachable*/}
		return true;
	}

	/**
	 * Delete node p, and then rebalance the tree.
	 */
	private void deleteEntry(TreeSetEntry p) {
		decrementSize();

		// If strictly internal, copy successor's element to p and then make p
		// point to successor.
		if (p.left != null && p.right != null) {
			
			{/*$goal 6 reachable*/}
			TreeSetEntry s = successor(p);
			p.key = s.key;

			p = s;
		} // p has 2 children

		// Start fixup at replacement node, if it exists.
		TreeSetEntry replacement;
		if (p.left != null )
		  replacement = p.left ;
		else
		  replacement = p.right;

		if (replacement != null) {
			
			{/*$goal 12 reachable*/}
			
			// Link replacement to parent
			replacement.parent = p.parent;
			if (p.parent == null) {
				
				{/*$goal 13 reachable*/}
				root = replacement;
			} else if (p == p.parent.left){
				
				{/*$goal 14 reachable*/}
				p.parent.left = replacement;
		   } else {
			   
			    {/*$goal 15 reachable*/}
				p.parent.right = replacement;
		   }

			// Null out links so they are OK to use by fixAfterDeletion.
			p.left = p.right = p.parent = null;

			// Fix replacement
			if (p.color == BLACK) {
				
				{/*$goal 16 reachable*/}
				fixAfterDeletion(replacement);
			}
		} else if (p.parent == null) { // return if we are the only node.
			
			{/*$goal 26 reachable*/}
			root = null;
		} else { //  No children. Use self as phantom replacement and unlink.
			if (p.color == BLACK) {
				
				{/*$goal 27 reachable*/}
				fixAfterDeletion(p);
			}

			if (p.parent != null) {
				
				{/*$goal 28 reachable*/}
				if (p == p.parent.left) {
					
					{/*$goal 29 reachable*/}
					p.parent.left = null;
				} else if (p == p.parent.right) {
					
					{/*$goal 30 reachable*/}
					p.parent.right = null;
				}
				
				{/*$goal 31 reachable*/}
				p.parent = null;
			}
		}
	}

	/** From CLR **/
	private void fixAfterDeletion(final TreeSetEntry entry) {
		TreeSetEntry x = entry;

		while (x != root && colorOf(x) == BLACK) {
			
			{/*$goal 17 reachable*/}
			
			if (x == leftOf(parentOf(x))) {
				
				{/*$goal 18 reachable*/}
				TreeSetEntry sib = rightOf(parentOf(x));

				if (colorOf(sib) == RED) {
					
					{/*$goal 19 reachable*/}
					setColor(sib, BLACK);
					setColor(parentOf(x), RED);
					rotateLeft(parentOf(x));
					sib = rightOf(parentOf(x));
				}

				if (colorOf(leftOf(sib)) == BLACK
						&& colorOf(rightOf(sib)) == BLACK) {
					
					{/*$goal 20 reachable*/}
					
					setColor(sib, RED);
					x = parentOf(x);
				} else {
					if (colorOf(rightOf(sib)) == BLACK) {
						
						{/*$goal 21 reachable*/}
						setColor(leftOf(sib), BLACK);
						setColor(sib, RED);
						rotateRight(sib);
						sib = rightOf(parentOf(x));
					}
					setColor(sib, colorOf(parentOf(x)));
					setColor(parentOf(x), BLACK);
					setColor(rightOf(sib), BLACK);
					rotateLeft(parentOf(x));
					x = root;
				}
			} else { // symmetric
				TreeSetEntry sib = leftOf(parentOf(x));

				if (colorOf(sib) == RED) {
					
					{/*$goal 22 reachable*/}
					setColor(sib, BLACK);
					setColor(parentOf(x), RED);
					rotateRight(parentOf(x));
					sib = leftOf(parentOf(x));
				}

				if (colorOf(rightOf(sib)) == BLACK
						&& colorOf(leftOf(sib)) == BLACK) {
					
					{/*$goal 23 reachable*/}
					setColor(sib, RED);
					x = parentOf(x);
				} else {
					if (colorOf(leftOf(sib)) == BLACK) {
						
						{/*$goal 24 reachable*/}
						setColor(rightOf(sib), BLACK);
						setColor(sib, RED);
						rotateLeft(sib);
						sib = leftOf(parentOf(x));
					}
					setColor(sib, colorOf(parentOf(x)));
					setColor(parentOf(x), BLACK);
					setColor(leftOf(sib), BLACK);
					rotateRight(parentOf(x));
					x = root;
				}
			}
		}

		{/*$goal 25 reachable*/}
		setColor(x, BLACK);
	}

	private void decrementSize() {
		modCount++;
		size--;
	}

	/*
	 * Returns the successor of the specified Entry, or null if no such.
	 */
	private TreeSetEntry successor(TreeSetEntry t) {
		if (t == null) {
			
			{/*$goal 7 unreachable*/} //always t!=null
			return null;
		} else if (t.right != null) {
			TreeSetEntry p = t.right;
			while (p.left != null) {
				{/*$goal 8 reachable*/}
				p = p.left;
			}
			
			{/*$goal 9 reachable*/}
			return p;
		} else {
			TreeSetEntry p = t.parent;
			TreeSetEntry ch = t;
			while (p != null && ch == p.right) {
				{/*$goal 10 unreachable*/} //always t.right != null
				ch = p;
				p = p.parent;
			}
			{/*$goal 11 unreachable*/} //always t.right != null
			return p;
		}
	}

	//*************************************************************************
	//************************* From now on repOk  ****************************
	//*************************************************************************.
	public boolean repOK() {
		if (root == null)
			return size == 0;

		if (root.parent != null)
			return false;

		RoopsSet visited = new RoopsSet();
		visited.add(root);
		RoopsList workList = new RoopsList();
		workList.add(root);
	
		while (workList.getSize() > 0) {

			TreeSetEntry current = (TreeSetEntry) workList.get(0);
			workList.remove(0);

			TreeSetEntry cl = current.left;
			if (cl != null) {
				if (!visited.add(cl))
					return false;
				if (cl.parent != current)
					return false;
				workList.add(cl);
			}
			TreeSetEntry cr = current.right;
			if (cr != null) {
				if (!visited.add(cr))
					return false;
				if (cr.parent != current)
					return false;
				workList.add(cr);
			}
		}

		if (visited.getSize() != size)
			return false;

		if (!repOK_Colors())
			return false;

		return repOK_KeysAndValues();
	}

	public boolean repOK_Colors() {

		RoopsList workList = new RoopsList();
		workList.add(root);
		while (workList.getSize() > 0) {
			TreeSetEntry current = (TreeSetEntry) workList.get(0);
			workList.remove(0);
			TreeSetEntry cl = current.left;
			TreeSetEntry cr = current.right;
			if (current.color == RED) {
				if (cl != null && cl.color == RED)
					return false;
				if (cr != null && cr.color == RED)
					return false;
			}
			if (cl != null)
				workList.add(cl);
			if (cr != null)
				workList.add(cr);
		}

		int numberOfBlack = -1;
		RoopsList workList2 = new RoopsList();
		
		workList2.add(new Pair(root, 0));
		
		while (workList2.getSize() > 0) {
			Pair p = (Pair) workList2.get(0);
			workList2.remove(0);
			TreeSetEntry e = p.e;
			int n = p.n;
			if (e != null && e.color == BLACK)
				n++;
			if (e == null) {
				if (numberOfBlack == -1)
					numberOfBlack = n;
				else if (numberOfBlack != n)
					return false;
			} else {
				workList2.add(new Pair(e.left, n));
				workList2.add(new Pair(e.right, n));
			}
		}
		return true;
	}

	public boolean repOK_KeysAndValues() {
		int min = repOK_findMin(root);
		int max = repOK_findMax(root);
		if (!repOK_orderedKeys(root, min-1, max+1))
			return false;

		// touch values
		RoopsList workList = new RoopsList();
		workList.add(root);
		while (workList.getSize() > 0) {
			TreeSetEntry current = (TreeSetEntry) workList.get(0);
			workList.remove(0);

			if (current.left != null)
				workList.add(current.left);
			if (current.right != null)
				workList.add(current.right);
		}
		return true;
	}

	private int repOK_findMin(TreeSetEntry root) {
		TreeSetEntry curr = root;
		while (curr.left!=null) {
			curr = curr.left;
		}
		return curr.key;
	}

	private int repOK_findMax(TreeSetEntry root) {
		TreeSetEntry curr = root;
		while (curr.right!=null) {
			curr = curr.right;
		}
		return curr.key;
	}

	public boolean repOK_orderedKeys(TreeSetEntry e, int min, int max) {

		if ((e.key <= min) || (e.key >= max))
			return false;
		if (e.left != null)
			if (!repOK_orderedKeys(e.left, min, e.key))
				return false;

		if (e.right != null)
			if (!repOK_orderedKeys(e.right, e.key, max))
				return false;
		return true;
	}

	
}
//$endcategory roops.core.objects

