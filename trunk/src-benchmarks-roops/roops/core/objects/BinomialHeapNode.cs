/***********************************************************************
Author: Juan Pablo Galeotti and Marcelo Frias, Relational Formal Methods 
Group, University of Buenos Aires and Buenos Aires Institute of Technology,
Buenos Aires, Argentina.
Auxiliary ROOPS class BinomialHeapNode, for ROOPS class BinomialHeap. 
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

// Authors: Marcelo Frias and Juan Pablo Galeotti

public class BinomialHeapNode {

	public int key; 

	public int degree; // depth of the binomial tree having the current node as its root

	public /*@ nullable @*/BinomialHeapNode parent; // pointer to the parent of the current node

	public /*@ nullable @*/BinomialHeapNode sibling; // pointer to the next binomial tree in the list

	public /*@ nullable @*/BinomialHeapNode child; // pointer to the first child of the current node

	public int getKey() {
		return key;
	}

	private void setKey(int value) { 
		key = value;
	}

	public int getDegree() { 
		return degree;
	}

	private void setDegree(int deg) { 
		degree = deg;
	}

	public BinomialHeapNode getParent() { 
		return parent;
	}

	private void setParent(BinomialHeapNode par) { 
		parent = par;
	}

	public BinomialHeapNode getSibling() { 
		return sibling;
	}

	private void setSibling(BinomialHeapNode nextBr) { 
		sibling = nextBr;
	}

	public BinomialHeapNode getChild() { 
		return child;
	}

	private void setChild(BinomialHeapNode firstCh) { 
		child = firstCh;
	}

	public BinomialHeapNode reverse(BinomialHeapNode sibl) {
		BinomialHeapNode ret;
		if (sibling != null)
			ret = sibling.reverse(this);
		else
			ret = this;
		sibling = sibl;
		return ret;
	}

	public BinomialHeapNode findMinNode() {
		BinomialHeapNode x = this, y = this;
		int min = x.key;

		while (x != null) {
			if (x.key < min) {
				y = x;
				min = x.key;
			}
			x = x.sibling;
		}

		return y;
	}



}
