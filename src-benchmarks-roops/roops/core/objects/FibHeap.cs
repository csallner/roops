//$category roops.core.objects

/**
  * source: 
  * FibHeap.java
  *from : http://sciris.shu.edu/~borowski/Puzzle/Puzzle.html
  */
//Authors: Marcelo Frias
//$benchmarkclass
/**
 * @Invariant ( all node: FibHeapNode | node in this.min.*(child @+ right) => ( 
 *              ( node !in (node.*right @- node).child.*(child @+ right) ) && 
 *              ( ( node in this.min.*right ) => (node.parent==null && this.min.cost <= node.cost ) ) && 
 *              ( node.right != null ) && 
 *              ( node.right.left == node ) && 
 *              ( node.degree = #(node.child.*right @- null) ) && 
 *              ( all m: FibHeapNode | ( m in node.child.*(child @+ right) => node.cost <= m.cost ) ) && 
 *              ( node.child != null => node.(child.*right).parent==node ) ) 
 *            ) &&
 * 
 *            ( this.n = #(this.min.*(child @+ right) @- null) ) ;
 */
public class FibHeap {


	//$goals 5
	//$benchmark
	public void insertNodeTest(FibHeap fibHeap, FibHeapNode toInsert) {
		if (fibHeap!=null && toInsert!=null && toInsert.left==toInsert && toInsert.right==toInsert && toInsert.parent==null &&  toInsert.child==null && fibHeap.repOK()) {
		  FibHeapNode ret_val = fibHeap.insertNode(toInsert);
		}
	}

	//$goals 1
	//$benchmark
	public void minimumTest(FibHeap fibHeap) {
		if (fibHeap!=null && fibHeap.repOK()) {
		  FibHeapNode ret_val = fibHeap.minimum();
		}
	}

	//$goals 25
	//$benchmark
	public void removeMinTest(FibHeap fibHeap) {
		if (fibHeap!=null && fibHeap.repOK()) {
		  FibHeapNode ret_val = fibHeap.removeMin();
		}
	}

	public FibHeap() {}
	
	public /*@ nullable @*/FibHeapNode min;

	public int n;

	private void consolidate() {
		int D = n + 1;
		Object[] A = new Object[D];
		for (int i = 0; i < D; i++) {
			
			{/*$goal  6 reachable*/}
			A[i] = null;
		}
		

		int k = 0;
		FibHeapNode x = min;
		if (x != null) {
			
			{/*$goal  7 reachable*/}
			k++;
			x = x.right;
			while (x != min) {
				
				{/*$goal  8 reachable*/}
				k++;
				x = x.right;
			}
		} else {
			{/*$goal  9 unreachable*/}
		}
		
		
		while (k > 0) {
			
			{/*$goal  10 reachable*/}
			int d = x.degree;
			FibHeapNode rightNode = x.right;

			while (A[d] != null) {

				{/*$goal  11 reachable*/}
				if (!(A[d] instanceof FibHeapNode)) {
					
					{/*$goal  12 unreachable*/}
					throw new RuntimeException();
				}
				FibHeapNode y = (FibHeapNode) A[d];
				
				if (x.cost > y.cost) {

					{/*$goal  13 reachable*/}
					FibHeapNode temp = y;
					y = x;
					x = temp;
				} else {
				    {/*$goal  14 reachable*/}
					link(y, x);
				}
				A[d] = null;
				d++;
			}

			A[d] = x;
			x = rightNode;
			k--;
		}

		
		min = null;
		for (int i = 0; i < D; i++) {
			
			{/*$goal  15 reachable*/}
			if (A[i] != null) {
				
				{/*$goal  16 reachable*/}
				if (min != null) {

					{/*$goal  17 reachable*/}
					if (!(A[i] instanceof FibHeapNode)) {
						
						{/*$goal  18 unreachable*/}
						throw new RuntimeException();
					} 
					FibHeapNode node = (FibHeapNode) A[i];

					node.left.right = node.right;
					node.right.left = node.left;
					node.left = min;
					node.right = min.right;
					min.right = node;
					node.right.left = node;
					if (node.cost < min.cost) {
						

						if (!(A[i] instanceof FibHeapNode)) {
							{/*$goal  19 unreachable*/}
							throw new RuntimeException();
						}						
						min = (FibHeapNode) A[i];
						
					} else {
						{/*$goal  20 reachable*/}
					}
				} else {

					{/*$goal  21 reachable*/}
					if (!(A[i] instanceof FibHeapNode)) {
						
						{/*$goal  22 unreachable*/}
						throw new RuntimeException();
					}
					min = (FibHeapNode) A[i];
				}
			} else {
				{/*$goal  23 reachable*/}
			}
		}
		
		{/*$goal  24 reachable*/}
	}

	public FibHeapNode insertNode(FibHeapNode toInsert) {
		if (min != null) {
			
			{/*$goal  0 reachable*/}
			toInsert.left = min;
			toInsert.right = min.right;
			min.right = toInsert;
			toInsert.right.left = toInsert;
			if (toInsert.cost < min.cost) {
				
				{/*$goal  1 reachable*/}
				min = toInsert;
			} else {

				{/*$goal  2 reachable*/}
			}
		} else {
			
			{/*$goal  3 reachable*/}
			min = toInsert;

		}
		n++;
		
		{roops.util.Goals.reached(4);}
		return toInsert;
	}

	private void link(FibHeapNode node1, FibHeapNode node2) {
		node1.left.right = node1.right;
		node1.right.left = node1.left;
		node1.parent = node2;
		if (node2.child == null) {

			node2.child = node1;
			node1.right = node1;
			node1.left = node1;
		} else {

			node1.left = node2.child;
			node1.right = node2.child.right;
			node2.child.right = node1;
			node1.right.left = node1;
		}
		node2.degree++;
		node1.mark = false;
	}

	

	public FibHeapNode minimum() {
		{/*$goal  0 reachable*/}
		return min;
	}

	
	public FibHeapNode removeMin() {
		FibHeapNode z = min;
		if (z != null) {
			
			{/*$goal  0 reachable*/}
			int i = z.degree;
			FibHeapNode x = z.child;
			while (i > 0) {

				{/*$goal  1 reachable*/}
				FibHeapNode nextChild = x.right;
				x.left.right = x.right;
				x.right.left = x.left;
				x.left = min;
				x.right = min.right;
				min.right = x;
				x.right.left = x;
				x.parent = null;
				x = nextChild;
				i--;
			} 
			
			{/*$goal  2 reachable*/}
			z.left.right = z.right;
			z.right.left = z.left;
			if (z == z.right) {

				{/*$goal  3 reachable*/}
				min = null;
			} else {

				{/*$goal  4 reachable*/}
				min = z.right;
				consolidate();
			}

			n--;
		}
		
		{/*$goal  5 reachable*/}
		return z;
	}

        //*************************************************************************
        //************** From now on repOK()  *************************************
        //*************************************************************************
	
	public boolean repOK() {
		RoopsSet allNodes = new RoopsSet();
		RoopsList parent_headers_to_visit = new RoopsList();

		if (min != null) {

			// check first level 
			{
				int child_cound = 0;
				FibHeapNode curr = min;
				do  {
					if (curr.left.right != curr)
						return false;

					if (curr.parent != null)
						return false;
					
					if (curr.child != null)
						parent_headers_to_visit.add(curr);

					if (!allNodes.add(curr))
						return false;// repeated node
					
					curr = curr.left;
					child_cound++;
				
				} while (curr!=min);
				
			}

			while (!parent_headers_to_visit.isEmpty()) {

				// check other levels 

				FibHeapNode node = (FibHeapNode) parent_headers_to_visit.get(0);
				parent_headers_to_visit.remove(0);

				int node_count = 0;
				FibHeapNode curr_node = node.child;
				do {
					if (curr_node.left.right != curr_node)
						return false;

					if (curr_node.parent != null)
						return false;

					if (curr_node.child != null)
						parent_headers_to_visit.add(curr_node);

					if (curr_node.cost>node.cost)
						return false;
					
					if (!allNodes.add(curr_node))
						return false; // repeated node
					
					
					curr_node = curr_node.left;
					node_count++;
					
				} while (curr_node != node.child);

				if (node_count != node.degree)
					return false;

			}

		}

		if (allNodes.getSize() != this.n)
			return false;

		return true;
	}

}
//$endcategory roops.core.objects
