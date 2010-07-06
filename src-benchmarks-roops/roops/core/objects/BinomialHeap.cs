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

[2] Visser W., P\u{a}s\u{a}reanu C.~S., Pel\'anek R., 
\emph{Test Input Generation for Java Containers using State Matching}, 
in ISSTA 2006, pp.~37--48, 2006.

[3] Galeotti J.P., Rosner N., Lopez Pombo C.G. and Frias M.F.
\emph{Analysis of Invariants for Efficient Bounded Verification}, in 
ISSTA 2010, to appear.

***********************************************************************/


//$category roops.core.objects

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
//$benchmarkclass
public class BinomialHeap {

	//$goals 10
	//$benchmark
	public void extractMinTest(BinomialHeap binomialHeap) {
		binomialHeap.extractMin();
	}


	public /*@ nullable @*/ BinomialHeapNode Nodes;

	public int size;

	// helper procedure
	private void merge(/*@ nullable @*/BinomialHeapNode binHeap) {

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
	private void unionNodes(/*@ nullable @*/BinomialHeapNode binHeap) {
		merge(binHeap);

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

	public /*@ nullable @*/BinomialHeapNode extractMin() {

		if (Nodes == null)
			return null;

                int old_size = size;

		BinomialHeapNode temp = Nodes, prevTemp = null;
		BinomialHeapNode minNode = Nodes.findMinNode();
		while (temp.key != minNode.key) {
			{/*$goal 0*/}
			prevTemp = temp;
			temp = temp.sibling;
		}

		if (prevTemp == null) {
			{/*$goal 1*/}
			Nodes = temp.sibling;
		} else {
			{/*$goal 2*/}
			prevTemp.sibling = temp.sibling;
		}
		temp = temp.child;
		BinomialHeapNode fakeNode = temp;
		while (temp != null) {
			{/*$goal 3*/}
			temp.parent = null;
			temp = temp.sibling;
		}

		if ((Nodes == null) && (fakeNode == null)) {
			{/*$goal 4*/}
			size = 0;
		} else {
			if ((Nodes == null) && (fakeNode != null)) {
				{/*$goal 5*/}
				Nodes = fakeNode.reverse(null);
				size--;
			} else {
				{/*$goal 6*/}
				if ((Nodes != null) && (fakeNode == null)) {
					{/*$goal 7*/}
					size--;
				} else {
					{/*$goal 8*/}
					unionNodes(fakeNode.reverse(null));
					size--;
				}
			}
		}
 
		if (this.size==12)
			{/*$goal 9*/}

		return minNode;
	}

}
//$endcategory roops.core.objects
