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
    	
    	if (tree!=null && tree.repOK()) {
            AvlNode ret_val = tree.findNode(x);
    	}
  }

   //$goals 3
   //$benchmark
   public void fmaxTest(AvlTree tree) {
	   
	   if (tree!=null && tree.repOK()) {
	     AvlNode ret_val = tree.fmax();
	   }
   }

   //$goals 3
   //$benchmark
   public void fminTest(AvlTree tree) {
	   
	   if (tree!=null && tree.repOK()) {
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

	//*************************************************************************
	//************** From now on repOk()  *************************************
	//*************************************************************************

        public boolean repOK() { 
		RoopsSet allNodes = new RoopsSet();
		RoopsIntList allData = new RoopsIntList();
                RoopsStack stack = new RoopsStack();
		if (root != null) {
                  stack.push(root);
                }
		
		while (stack.getSize()>0) {

			AvlNode node = (AvlNode) stack.pop();

			if (!allNodes.add(node))
				return false; // Not acyclic.

			if (!allData.add(node.element))
				return false; // Repeated data.

			// check balance           
			int l_Height;
                        if (node.left == null)
                          l_Height = 0 ;
                        else
                          l_Height = node.left.height;

			int r_Height;
                        if (node.right == null)
                          r_Height = 0 ;
                        else
                          r_Height = node.right.height;

			int difference = l_Height - r_Height;
			if (difference < -1 || difference > 1)
				return false; // Not balanced.

			int max;
                        if (l_Height > r_Height)
                          max = l_Height ;
                        else
                          max = r_Height;

			if (node.height != 1 + max)
				return false; // Wrong height.

                        // visit descendants
			if (node.left != null)
				stack.push(node.left);

			if (node.right != null)
				stack.push(node.right);

		}
		
		if (!repOK_isOrdered(root))
			return false;
		
		
		return true;
	}

        private boolean repOK_isOrdered(AvlNode n) {
            if (n==null)
              return true;

            int min = repOK_isOrderedMin(n);
            int max = repOK_isOrderedMax(n);

            return repOK_isOrdered(n, min, max);
        }

        private boolean repOK_isOrdered(AvlNode n, int min, int max) {

            if ((n.element<min ) || (n.element>max))
                return false;

            if (n.left != null) {
                if (!repOK_isOrdered(n.left, min, n.element))
                    return false;
            }
            if (n.right != null) {
                if (!repOK_isOrdered(n.right, n.element, max))
                    return false;
            }
            return true;
        }

        private int repOK_isOrderedMin(AvlNode n) {
          AvlNode curr = n;
          while (curr.left!=null) {
            curr = curr.left;
          }
          return curr.element;
        }

        private int repOK_isOrderedMax(AvlNode n) {
          AvlNode curr = n;
          while (curr.right!=null) {
            curr = curr.right;
          }
          return curr.element;
        }

}
//$endcategory roops.core.objects

