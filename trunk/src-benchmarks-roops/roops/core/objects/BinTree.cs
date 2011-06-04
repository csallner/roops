//$category roops.core.objects

//Authors: Marcelo Frias
//$benchmarkclass
/**
 * 
 * @Invariant all n : BinTreeNode | n in this.root.*(left @+ right ) => ( 
 *            ( n !in n.^(left @+ right) ) && 
 *            ( all m: BinTreeNode | m in n.left.*(left @+right) => m.key < n.key ) && 
 *            ( all m: BinTreeNode | m in n.right.*(left @+right) => n.key < m.key ) && 
 *            ( n.left!=null => n.left.parent=n ) &&
 *            ( n.right!=null=> n.right.parent=n ) && 
 *            ( n=this.root => n.parent=null ) ) ;
 * 
 */
public class BinTree {

	//$goals 9
	//$benchmark
	public void addTest(BinTree tree, int x) {
		if (tree!=null) {
		  tree.add(x);
		}
	}

	//$goals 5
	//$benchmark
	public void findTest(BinTree tree, int x) {
		boolean ret_val;
		if (tree!=null) {
		  ret_val = tree.find(x);
		}
	}

	//$goals 17
	//$benchmark
	public void removeTest(BinTree tree, BinTreeNode z) {
		BinTreeNode ret_val;
		if (tree!=null && z!=null) {
		  ret_val = tree.remove(z);
		}
	}	

	
	public /*@ nullable @*/ BinTreeNode root;


	public void add(int x) {
		BinTreeNode current = root;

		if (root == null) {
			{/*$ goal 0 reachable */ }
			root = new BinTreeNode();
			initNode(root,x);
			return;
		}

		while (current.key != x) {
			
			{/*$ goal 1 reachable */ }
			
			if (x < current.key) {
				
				{/*$ goal 2 reachable */ }
				
				if (current.left == null) {
					{/*$ goal 3 reachable */ }
					current.left = new BinTreeNode();
					initNode(current.left,x);
				} else {
					{/*$ goal 4 reachable */ }
					current = current.left;
				}
			} else {
				{/*$ goal 5 reachable */ }
				
				if (current.right == null) {
					{/*$ goal 6 reachable */ }
					current.right = new BinTreeNode();
					initNode(current.right,x);
				} else {
					{/*$ goal 7 reachable */ }
					current = current.right;
				}
			}
		}
		
		{/*$ goal 8 reachable */ }
	}


	private void initNode(BinTreeNode node, int x) {
		node.key = x;
		node.left = null;
		node.right = null;
	}

	public boolean find(int x) {
		BinTreeNode current = root;

		while (current != null) {
			{/*$ goal 0 reachable */ }
			
			if (current.key == x) {
				{/*$ goal 1 reachable */ }
				return true;
			}

			if (x < current.key) {
				{/*$ goal 2 reachable */ }
				current = current.left;
			} else {
				{/*$ goal 3 reachable */ }
				current = current.right;
			}
		}

		{/*$ goal 4 reachable */ }
		return false;
	}

	private BinTreeNode treeMinimum(final BinTreeNode x_param) {
		BinTreeNode x = x_param;
		while (x.left != null) {
			{/*$ goal 15 reachable */ }
			x = x.left;
		}
		{/*$ goal 16 reachable */ }
		return x;
	}

	private BinTreeNode treeSuccessor(final BinTreeNode x_param) {
		BinTreeNode x = x_param;
		BinTreeNode result;
		if (x.right != null) {
			{/*$ goal 11 reachable */ }
			result = treeMinimum(x.right);
		} else {
			{/*$ goal 12 unreachable */ }
			BinTreeNode y = x.parent;
			while (y != null && x == y.right) {
				{/*$ goal 13 unreachable */ }
				x = y;
				y = y.parent;
			}

			result = y;
		}
		{/*$ goal 14 reachable */ }
		return result;
	}

	
	public BinTreeNode remove(final BinTreeNode z) {
		BinTreeNode y = null;
		if (z.left == null || z.right == null) {
			{/*$ goal 0 reachable */ }
			y = z;
		} else {
			{/*$ goal 1 reachable */ }
			y = treeSuccessor(z);
		}

		BinTreeNode x = null;
		if (y.left != null) {
			{/*$ goal 2 reachable */ }
			x = y.left;
		} else {
			{/*$ goal 3 reachable */ }
			x = y.right;
		}

		if (x != null) {
			{/*$ goal 4 reachable */ }
			x.parent = y.parent;
		}

		if (y.parent == null) {
			{/*$ goal 5 reachable */ }
			this.root = x;
		} else {
			{/*$ goal 6 reachable */ }
			if (y == y.parent.left){
				{/*$ goal 7 reachable */ }
				y.parent.left = x;
			}else{
				{/*$ goal 8 reachable */ }
				y.parent.right = x;
			}
		}

		if (y != z) {
			{/*$ goal 9 reachable */ }
			z.key = y.key;
		}

		{/*$ goal 10 reachable */ }
		return y;
	}


	public BinTree() {}

}
//$endcategory roops.core.objects
