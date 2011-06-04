//$category roops.core.objects

//Authors: Marcelo Frias

public class FibHeapNode {


	public boolean mark = false;
	public int cost = 0;
	public int degree = 0;

	public /*@ nullable @*/ FibHeapNode parent;
	public /*@ nullable @*/ FibHeapNode left;
	public /*@ nullable @*/ FibHeapNode right;
	public /*@ nullable @*/ FibHeapNode child;

	public FibHeapNode() {}
}
//$endcategory roops.core.objects
