/***********************************************************************
Author: Juan Pablo Galeotti and Marcelo Frias, Relational Formal Methods 
Group, University of Buenos Aires and Buenos Aires Institute of Technology,
Buenos Aires, Argentina.
ROOPS class BinomialHeap with an auxiliary ROOPS class BinomialHeapNode. 
These classes are an implementation of Binomial Heaps. They come with a 
JFSL [1] contract that is annotated as a ROOPS comment.
This file is an adaptation from the BinomialHeap implementation used in
[2]. The implementation contains a subtle bug reported in [3] that
requires 13 BinomialHeapNode objects to be exhibited. The adaptation
includes a goal that requires an input structure with 13 BinomialHeapNode
objects to be covered, as a means to show wether the tool under use
can generate such a complex input.

[1] http://sdg.csail.mit.edu/forge/plugin.html

[2] Visser W., Pasareanu C.S., Pelanek R., Test Input Generation for Java
Containers using State Matching, in ISSTA 2006, pp.37-48, 2006.

[3] Galeotti J.P., Rosner N., Lopez Pombo C.G. and Frias M.F.
Analysis of Invariants for Efficient Bounded Verification, in 
ISSTA 2010, to appear.

***********************************************************************/

//$category roops.core.objects
//$benchmarkclass
/**
 * @Invariant ( all n: BinomialHeapNode | ( n in this.Nodes.*(sibling @+ child) @- null => (
 *		            ( n.parent!=null  => n.key >=  n.parent.key )  &&   
 *		            ( n.child!=null   => n !in n.child.*(sibling @+ child) @- null ) && 
 *		            ( n.sibling!=null => n !in n.sibling.*(sibling @+ child) @- null ) && 
 *		            ( ( n.child !=null && n.sibling!=null ) => (no m: BinomialHeapNode | ( m in n.child.*(child @+ sibling) @- null && m in n.sibling.*(child @+ sibling) @- null )) ) && 
 *		            ( n.degree >= 0 ) && 
 *		            ( n.child=null => n.degree = 0 ) && 
 *		            ( n.child!=null =>n.degree=#(n.child.*sibling @- null) )  && 
 *		            ( #( ( n.child @+ n.child.child.*(child @+ sibling) ) @- null ) = #( ( n.child @+ n.child.sibling.*(child @+ sibling)) @- null )  ) && 
 *		            ( n.child!=null => ( all m: BinomialHeapNode | ( m in n.child.*sibling@-null =>  m.parent = n  ) ) ) && 
 *		            ( ( n.sibling!=null && n.parent!=null ) => ( n.degree > n.sibling.degree ) )
 * ))) && 
 * ( this.size = #(this.Nodes.*(sibling @+ child) @- null) ) &&
 * ( all n: BinomialHeapNode | n in this.Nodes.*sibling @- null => ( 
 *  ( n.sibling!=null => n.degree < n.sibling.degree ) && 
 *  ( n.parent=null ) 
 *  )) ;
 */
public class BinomialHeap {

	//$goals 10
	//$benchmark
	public void extractMinTest(BinomialHeap binomialHeap) {
		
		if (binomialHeap!=null && binomialHeap.repOK()) {
		  BinomialHeapNode ret_val = binomialHeap.extractMin();
		}
	}


	//$goals 3
	//$benchmark
	public void findMinTest(BinomialHeap binomialHeap) {
		
		if (binomialHeap!=null && binomialHeap.repOK()) {
		  int ret_val = binomialHeap.findMinimum();
		}
	}

	//$goals 3
	//$benchmark 	
	public void decreaseKeyNodeTest(BinomialHeap binomialHeap, BinomialHeapNode node, int new_value) {
		
		if (binomialHeap!=null && binomialHeap.repOK()) {
		  binomialHeap.decreaseKeyNode(node, new_value);
		}
	}

	//$goals 22
	//$benchmark
	public void insertTest(BinomialHeap binomialHeap, int value) {
		
		if (binomialHeap!=null && binomialHeap.repOK()) {
		  binomialHeap.insert(value);
		}
	}

	public /*@ nullable @*/ BinomialHeapNode Nodes;

	public int size;

	// helper procedure
	private void merge_extractMin(BinomialHeapNode binHeap) {

		BinomialHeapNode temp1 = Nodes, temp2 = binHeap;
		while ((temp1 != null) && (temp2 != null)) {
			if (temp1.degree == temp2.degree) {
				BinomialHeapNode tmp = temp2;
				temp2 = temp2.sibling;
				tmp.sibling = temp1.sibling;
				temp1.sibling = tmp;
				temp1 = tmp.sibling;
			} else {
				if (temp1.degree < temp2.degree) {
					if ((temp1.sibling == null)
							|| (temp1.sibling.degree > temp2.degree)) {
						BinomialHeapNode tmp = temp2;
						temp2 = temp2.sibling;
						tmp.sibling = temp1.sibling;
						temp1.sibling = tmp;
						temp1 = tmp.sibling;
					} else {
						temp1 = temp1.sibling;
					}
				} else {
					BinomialHeapNode tmp = temp1;
					temp1 = temp2;
					temp2 = temp2.sibling;
					temp1.sibling = tmp;
					if (tmp == Nodes) {
						Nodes = temp1;
					} 
				}
			}
		}

		if (temp1 == null) {
			temp1 = Nodes;
			while (temp1.sibling != null) {
				temp1 = temp1.sibling;
			}
			temp1.sibling = temp2;
		} 

	}

	// another helper procedure
	private void unionNodes_extractMin(BinomialHeapNode binHeap) {
		merge_extractMin(binHeap);

		BinomialHeapNode prevTemp = null, temp = Nodes , nextTemp = Nodes.sibling;
		
		while (nextTemp != null) {
			if ((temp.degree != nextTemp.degree)
					|| ((nextTemp.sibling != null) && (nextTemp.sibling.degree == temp.degree))) {
				prevTemp = temp;
				temp = nextTemp;
			} else {
				if (temp.key <= nextTemp.key) {
					temp.sibling = nextTemp.sibling;
					nextTemp.parent = temp;
					nextTemp.sibling = temp.child;
					temp.child = nextTemp;
					temp.degree++;
				} else {
					if (prevTemp == null) {
						Nodes = nextTemp;
					} else {
						prevTemp.sibling = nextTemp;
					}
					temp.parent = nextTemp;
					temp.sibling = nextTemp.child;
					nextTemp.child = temp;
					nextTemp.degree++;
					temp = nextTemp;
				}
			}

			nextTemp = temp.sibling;
		}
	}

	public BinomialHeapNode extractMin() {

		if (Nodes == null) 
			return null;

		int old_size = size;

		BinomialHeapNode temp = Nodes, prevTemp = null;
		BinomialHeapNode minNode = findMinNode_extractMin(Nodes);
		while (temp.key != minNode.key) {
			{/*$goal 0 reachable*/}
			prevTemp = temp;
			temp = temp.sibling;
		}

		if (prevTemp == null) {
			{/*$goal 1 reachable*/}
			Nodes = temp.sibling;
		} else {
			{/*$goal 2 reachable*/}
			prevTemp.sibling = temp.sibling;
		}
		temp = temp.child;
		BinomialHeapNode fakeNode = temp;
		while (temp != null) {
			{/*$goal 3 reachable*/}
			temp.parent = null;
			temp = temp.sibling;
		}

		if ((Nodes == null) && (fakeNode == null)) {
			{/*$goal 4 reachable*/}
			size = 0;
		} else {
			if ((Nodes == null) && (fakeNode != null)) {
				{/*$goal 5 reachable*/}
				Nodes = fakeNode.reverse(null);
				size--;
			} else {
				{/*$goal 6 reachable*/}
				if ((Nodes != null) && (fakeNode == null)) {
					{/*$goal 7 reachable*/}
					size--;
				} else {
					{/*$goal 8 reachable*/}
					unionNodes_extractMin(fakeNode.reverse(null));
					size--;
				}
			}
		}
 
		if (this.size==12) {
			{/*$goal 9 reachable*/}
		}
		return minNode;
	}

	
	public int findMinimum() {
		return findMinNode(Nodes).key;
	}
	
	private static BinomialHeapNode findMinNode_extractMin(BinomialHeapNode arg) {
		BinomialHeapNode x = arg, y = arg;
		int min = x.key;

		while (x != null) {
			{/*$goal 0 reachable*/}
			
			if (x.key < min) {
				{/*$goal 1 reachable*/}
				y = x;
				min = x.key;
			}
			x = x.sibling;
		}

		{/*$goal 2 reachable*/}
		return y;
	}

	private static BinomialHeapNode findMinNode(BinomialHeapNode arg) {
		BinomialHeapNode x = arg, y = arg;
		int min = x.key;

		while (x != null) {
			{/*$goal 0 reachable*/}
			
			if (x.key < min) {
				{/*$goal 1 reachable*/}
				y = x;
				min = x.key;
			}
			x = x.sibling;
		}

		{/*$goal 2 reachable*/}
		return y;
	}


	public void decreaseKeyNode(BinomialHeapNode node, int new_value) {
		if (node == null) {
			{/*$goal 0 reachable*/}
			return;
		}
		
		node.key = new_value;
		BinomialHeapNode tempParent = node.parent;

		while ((tempParent != null) && (node.key < tempParent.key)) {
			{/*$goal 1 reachable*/}
			
			int z = node.key;
			node.key = tempParent.key;
			tempParent.key = z;

			node = tempParent;
			tempParent = tempParent.parent;
		}
		{/*$goal 2 reachable*/}
	}

	

	public void insert(int value) {
		if (value > 0) {
			{/*$goal 0 reachable*/}
			
			BinomialHeapNode temp = new BinomialHeapNode();
			temp.key = value;
			
			if (Nodes == null) {
				{/*$goal 1 reachable*/}
				
				Nodes = temp;
				size = 1;
			} else {
				{/*$goal 2 reachable*/}
				
				unionNodes_insert(temp);
				size++;
			}
		} else {
			{/*$goal 21 reachable*/}
		}
	}

	// another helper procedure
	private void unionNodes_insert(BinomialHeapNode binHeap) {
		merge_insert(binHeap);
	
		BinomialHeapNode prevTemp = null, temp = Nodes , nextTemp = Nodes.sibling;
		
		while (nextTemp != null) {
			if ((temp.degree != nextTemp.degree)
					|| ((nextTemp.sibling != null) && (nextTemp.sibling.degree == temp.degree))) {
				
				{/*$goal 14 reachable*/}
				
				prevTemp = temp;
				temp = nextTemp;
			} else {
				
				{/*$goal 15 reachable*/}
				
				if (temp.key <= nextTemp.key) {
					
					{/*$goal 16 reachable*/}
					
					temp.sibling = nextTemp.sibling;
					nextTemp.parent = temp;
					nextTemp.sibling = temp.child;
					temp.child = nextTemp;
					temp.degree++;
				} else {
					
					{/*$goal 17 reachable*/}
					
					if (prevTemp == null) {
						
						{/*$goal 18 reachable*/}
						
						Nodes = nextTemp;
					} else {
						
						{/*$goal 19 reachable*/}
						prevTemp.sibling = nextTemp;
					}
					temp.parent = nextTemp;
					temp.sibling = nextTemp.child;
					nextTemp.child = temp;
					nextTemp.degree++;
					temp = nextTemp;
				}
			}
	
			{/*$goal 20 reachable*/}
			nextTemp = temp.sibling;
		}
	}

	// helper procedure
	private void merge_insert(BinomialHeapNode binHeap) {
	
		BinomialHeapNode temp1 = Nodes, temp2 = binHeap;
		while ((temp1 != null) && (temp2 != null)) {
			
			{/*$goal 3 reachable*/}
			
			if (temp1.degree == temp2.degree) {
				
				{/*$goal 4 reachable*/}
				
				BinomialHeapNode tmp = temp2;
				temp2 = temp2.sibling;
				tmp.sibling = temp1.sibling;
				temp1.sibling = tmp;
				temp1 = tmp.sibling;
			} else {
				
				{/*$goal 5 reachable*/}
				
				if (temp1.degree < temp2.degree) {
					
					{/*$goal 6 reachable*/}
					
					if ((temp1.sibling == null)
							|| (temp1.sibling.degree > temp2.degree)) {
						
						{/*$goal 7 reachable*/}
						
						BinomialHeapNode tmp = temp2;
						temp2 = temp2.sibling;
						tmp.sibling = temp1.sibling;
						temp1.sibling = tmp;
						temp1 = tmp.sibling;
					} else {
						
						{/*$goal 8 reachable*/}
						
						temp1 = temp1.sibling;
					}
				} else {
					{/*$goal 9 reachable*/}
					
					BinomialHeapNode tmp = temp1;
					temp1 = temp2;
					temp2 = temp2.sibling;
					temp1.sibling = tmp;
					if (tmp == Nodes) {
						{/*$goal 10 reachable*/}
						Nodes = temp1;
					} 
				}
			}
		}
	
		if (temp1 == null) {

			{/*$goal 11 reachable*/}
			temp1 = Nodes;
			while (temp1.sibling != null) {

				{/*$goal 12 reachable*/}
				temp1 = temp1.sibling;
			}
			
			{/*$goal 13 reachable*/}
			temp1.sibling = temp2;
		} 
	
	}

	//*************************************************************************
	//************************* From now on repOK  ****************************
	//*************************************************************************.
	
	public boolean repOK() {
		RoopsList seen = new RoopsList();

		if (this.Nodes!=null) {

			if (!repOK_isAcyclic(this.Nodes, seen))
				return false;
			
			if (!repOK_ordered(this.Nodes))
				return false;

			if (this.Nodes.parent!=null)
				return false;
			
			if (this.Nodes.sibling!=null) {
				if (this.Nodes.degree >= this.Nodes.sibling.degree)
					return false;
			}
			
			BinomialHeapNode ns = this.Nodes.sibling; 
			while (ns != null) {

				if (!repOK_isAcyclic(ns, seen))
					return false;
		    	  
				if (ns.parent!=null)
					return false;
		    	  
				if (ns.sibling!=null) {
		    		  if (ns.degree>=ns.sibling.degree)
		    			  return false;
		    	  }
		    	  

		    	  if (!repOK_ordered(ns))
		    		  return false;

		    	  
		    	  ns = ns.sibling;
		      }
			
		}
		
		int node_count = seen.getSize();
		
		if (this.size!=node_count)
			return false;
			
		return true;
	}
	
	private static boolean repOK_nodeRepOk(BinomialHeapNode Node) {
		RoopsList seen = new RoopsList();

		if (Node!=null) {

			if (!repOK_isAcyclic(Node, seen))
				return false;
			
			if (!repOK_ordered(Node))
				return false;

			if (Node.parent!=null)
				return false;
			
			if (Node.sibling!=null) {
				if (Node.degree >= Node.sibling.degree)
					return false;
			}
			
			BinomialHeapNode ns = Node.sibling; ;
		    while (ns != null) {

				  if (!repOK_isAcyclic(ns, seen))
				     return false;
		    	  
		    	  if (ns.parent!=null)
		    		  return false;
		    	  
		    	  if (ns.sibling!=null) {
		    		  if (ns.degree>=ns.sibling.degree)
		    			  return false;
		    	  }
		    	  
		    	  if (!repOK_ordered(ns))
		    		  return false;

		    	  ns = ns.sibling;
		      }
			
		}
		
		return true;
	}

     private static boolean repOK_isAcyclic(BinomialHeapNode start, RoopsList seen) {

    	 if (seen.contains(start))
    		 return false;
    	 
    	 if (start.degree<0)
    		 return false;
    	 
    	 seen.add(start);
    	 
    	 BinomialHeapNode child = start.child;
    	 
    	 int child_count = 0;
    	 
    	 while (child!=null) {
    		 
    		 child_count++;
    		 
        	 if (child.parent != start)
        		 return false;
    		 
    		 if (!repOK_isAcyclic(child, seen))
    			 return false;
    		 
    		 if (child.sibling!=null) {
    			 if (child.degree<=child.sibling.degree)
    				 return false;
    		 }
    		 child = child.sibling;
    	 }
    	
    	 if (start.degree!=child_count)
    		 return false;
    	 
    	 if (start.child!=null) {
    		 int tam_child=1;
    		 if (start.child.child!=null) {
    			 BinomialHeapNode curr = start.child.child;
    			 while (curr!=null) {
    			   tam_child+= repOK_count_nodes(start.child.child);
    			   curr = curr.sibling;
    			 }
    		 }
    		 
    		 int tam_sibling=1;
    		 if (start.child.sibling!=null) {
    			 BinomialHeapNode curr = start.child.sibling;
    			 while (curr!=null) {
    			   tam_sibling+= repOK_count_nodes(start.child.sibling);
  			       curr = curr.sibling;
    			 }
    		 }
    		 
    		 if (tam_child!=tam_sibling)
    			 return false;
    		 
    	 }
    	 
    	 return true;
	}
	
	private static int repOK_count_nodes(BinomialHeapNode start) {
		
		int node_count = 1;
		
		BinomialHeapNode child = start.child;
		while (child!=null) {
			
			node_count += repOK_count_nodes(child);
			
			child=child.sibling;
		}
		
		return node_count;
	}

	private static boolean repOK_ordered(final BinomialHeapNode node) {
		    if (node.child != null) {
		      if (node.child.key < node.key) {
		        return false;
		      }
		      if (!repOK_ordered(node.child)) {
		        return false;
		      }
		      for (BinomialHeapNode ns = node.child.sibling; ns != null; ns = ns.sibling) {
		        if (ns.key < node.key) {
		          return false;
		        }
		        if (!repOK_ordered(ns)) {
		          return false;
		        }
		      }
		      return true;
		    } 
		     
		    return true;
		    
		  }


	
}
/* end roops.core.objects */

