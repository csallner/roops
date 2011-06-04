//$category roops.core.objects

//Authors: Marcelo Frias
//$benchmarkclass
/**
 * @Invariant all x: AvlNode | x in this.root.*(left @+ right) @- null => 
 * (
 *		(x !in x.^(left @+ right)) && 
 *		(all y: AvlNode | (y in x.left.*(left @+ right) @-null) => y.element < x.element ) && 
 *		(all y: AvlNode | (y in x.right.*(left @+right) @- null) => x.element < y.element ) && 
 *		(x.left=null && x.right=null => x.height=0) && 
 *		(x.left=null && x.right!=null => x.height=1 && x.right.height=0) && 
 *		(x.left!=null && x.right=null => x.height=1 && x.left.height=0) && 
 *		(x.left!=null && x.right!=null => x.height= (x.left.height>x.right.height ? x.left.height : x.right.height )+1 && ( (x.left.height > x.right.height ? x.left.height - x.right.height : x.right.height - x.left.height ))<=1)
 * );
 * 
 */
public class AvlTree {

  //$goals 5
  //$benchmark
  public void findNodeTest(AvlTree tree, int x) {
    	
    	if (tree!=null) {
		  AvlNode ret_val = tree.findNode(x);
    	}
  }

   //$goals 3
   //$benchmark
   public void fmaxTest(AvlTree tree) {
	   
	   if (tree!=null) {
	     AvlNode ret_val = tree.fmax();
	   }
   }

   //$goals 3
   //$benchmark
   public void fminTest(AvlTree tree) {
	   
	   if (tree!=null) {
	     AvlNode ret_val = tree.fmin();
	   }
   }

	public /*@ nullable @*/AvlNode root;

	public/*@ nullable @*/AvlNode findNode(final int x) {
		return find(x, this.root);
	}


	private AvlNode find(final int x, final AvlNode arg) {
		AvlNode t = arg;
		while (t != null) {
			
			{ /*$goal 0 reachable*/}
			if (x < t.element) {
				
				{ /*$goal 1 reachable*/}
				t = t.left;
			} else if (x > t.element) {
				
				{ /*$goal 2 reachable*/}
				t = t.right;
			} else {
				
				{ /*$goal 3 reachable*/}
				return t; // Match
			}
		}

		{ /*$goal 4 reachable*/}
		return null; // No match
	}

	
	public AvlNode fmax() {
		return findMax(this.root);
	}


	private AvlNode findMax(final AvlNode arg) {
		AvlNode t = arg;
		if (t == null) {
			{ /*$goal 0 reachable*/}
			return t;
		}

		while (t.right != null) {
			{ /*$goal 1 reachable*/}
			t = t.right;
		}
		
		{ /*$goal 2 reachable*/}
		return t;
	}



	public AvlNode fmin() {
		return findMin(this.root);
	}

	

	private AvlNode findMin(final AvlNode arg) {
		AvlNode t = arg;
		if (t == null) {
			{ /*$goal 0 reachable*/}
			return t;
		}

		while (t.left != null) {
			{ /*$goal 1 reachable*/}
			t = t.left;
		}
		
		{ /*$goal 2 reachable*/}
		return t;
	}

	public AvlTree() {}

}
//$endcategory roops.core.objects

