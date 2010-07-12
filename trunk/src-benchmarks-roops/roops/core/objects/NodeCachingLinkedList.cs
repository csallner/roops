/****************************************************************************
Author: Juan Pablo Galeotti and Marcelo Frias, Relational Formal Methods 
Group, University of Buenos Aires and Buenos Aires Institute of Technology,
Buenos Aires, Argentina.

ROOPS class implementing the apache.commons.collections class NodeCachingLinkedList.
It uses an auxiliary class LinkedListNode. Method removeIndex has been modified by
adding a goal that requires the cache list to be full to be covered. This means that
22 nodes are required in the cache part of the structure. 
A bug has been seeded in method isCacheFull. The bug allows to remove a node from the
NodeCachingLinkedList using method removeIndex and end up with an overflown cache. 
This state is captured by goal 10. The input NodeCachingLinkedList must have 23 nodes
in its cache linked list.

The class has annotations in JFSL [1] given as ROOPS comments. In particular, a class
invariant is provided.

[1] http://sdg.csail.mit.edu/forge/plugin.html
****************************************************************************/


//$category roops.core.objects

/**
 * @Invariant 
 *		( this.header!=null ) &&
 *		( this.header.next!=null ) &&
 *		( this.header.previous!=null ) &&
 *		( this.size==#(this.header.*next @- null)-1 ) &&
 *		( this.size>=0 ) &&
 *		( this.cacheSize <= this.maximumCacheSize ) &&
 *		( this.DEFAULT_MAXIMUM_CACHE_SIZE == 20 ) &&
 *		( this.cacheSize == #(this.firstCachedNode.*next @- null) ) &&
 *		(all m: LinkedListNode | ( m in this.firstCachedNode.*next @- null ) => (
 *				  m !in m.next.*next @- null &&
 *				  m.previous==null &&
 *				  m.value==null )) &&
 *		(all n: LinkedListNode | ( n in this.header.*next @- null ) => (
 *				  n!=null &&
 *				  n.previous!=null &&
 *				  n.previous.next==n &&
 *				  n.next!=null &&
 *				  n.next.previous==n )) ; 
 *
 */
//$benchmarkclass
public class NodeCachingLinkedList {

	//$goals 11
	//$benchmark
	public void removeIndexTest(NodeCachingLinkedList list, int index) {
		list.removeIndex(index);
	}

	public/*@ nullable @*/ LinkedListNode header;
	public int size;
	public int modCount;

	public final int DEFAULT_MAXIMUM_CACHE_SIZE = 20;

	/**
	 * The first cached node, or <code>null</code> if no nodes are cached.
	 * Cached nodes are stored in a singly-linked list with
	 * <code>next</code> pointing to the next element.
	 */
	public /*@ nullable @*/ LinkedListNode firstCachedNode;

	/**
	 * The size of the cache.
	 */
	public int cacheSize;

	/**
	 * The maximum size of the cache.
	 */
	public int maximumCacheSize;

	//-----------------------------------------------------------------------

	/**
	 * Checks whether the cache is full.
	 * 
	 * @return true if the cache is full
	 */
	protected boolean isCacheFull() {
		//return cacheSize >= maximumCacheSize; 
		return cacheSize > maximumCacheSize; //<- BUG SEEDED
	}

	/**
	 * Adds a node to the cache, if the cache isn't full.
	 * The node's contents are cleared to so they can be garbage collected.
	 * 
	 * @param node  the node to add to the cache
	 */
	protected void addNodeToCache(LinkedListNode node) {
		if (isCacheFull()) {
			{/*$goal 7 unreachable*/}
			// don't cache the node.
			return;
		}
                
		// clear the node's contents and add it to the cache.
		LinkedListNode nextCachedNode = firstCachedNode;
		node.previous = null;
		node.next = nextCachedNode;
		node.setValue(null);
		firstCachedNode = node;
		cacheSize++;
		{/*$goal 8*/}
	}

	/**
	 * Removes the specified node from the list.
	 *
	 * @param node  the node to remove
	 * @throws NullPointerException if <code>node</code> is null
	 */
	private void super_removeNode(LinkedListNode node) {
		node.previous.next = node.next;
		node.next.previous = node.previous;
		size--;
		modCount++;
	}

	/**
	 * Removes the node from the list, storing it in the cache for reuse
	 * if the cache is not yet full.
	 * 
	 * @param node  the node to remove
	 */
	protected void removeNode(LinkedListNode node) {
		super_removeNode(node);
		addNodeToCache(node);
	}

	//-----------------------------------------------------------------------

	/**
	 * @Modifies_Everything
	 */
	public/*@ nullable @*/Object removeIndex(int index) {

		LinkedListNode node = getNode(index, false);
		Object oldValue = node.getValue();
		removeNode(node);

		if (maximumCacheSize==DEFAULT_MAXIMUM_CACHE_SIZE) {
			{/*$goal 9*/}

			if (cacheSize>maximumCacheSize) {
				{/*$goal 10*/}
			}
		} 
		return oldValue;
	}

	//-----------------------------------------------------------------------
	protected LinkedListNode getNode(int index, boolean endMarkerAllowed)
			throws RuntimeException {
		// Check the index is within the bounds
		if (index < 0) {
			{/*$goal 0*/}

			throw new RuntimeException("Couldn't get the node: "
					+ "index (" + index + ") less than zero.");
		}
		if (!endMarkerAllowed && index == size) {
			{/*$goal 1*/}

			throw new RuntimeException("Couldn't get the node: "
					+ "index (" + index + ") is the size of the list.");
		}
		if (index > size) {
			{/*$goal 2*/}

			throw new IndexOutOfBoundsException("Couldn't get the node: "
					+ "index (" + index + ") greater than the size of the "
					+ "list (" + size + ").");
		}
		// Search the list and get the node
		LinkedListNode node;
		if (index < (size / 2)) {
			{/*$goal 3*/}

			// Search forwards
			node = header.next;
			for (int currentIndex = 0; currentIndex < index; currentIndex++) {
				{/*$goal 4*/}
				node = node.next;
			}
		} else {
			{/*$goal 5*/}

			// Search backwards
			node = header;
			for (int currentIndex = size; currentIndex > index; currentIndex--) {
				{/*$goal 6*/}
				node = node.previous;
			}
		}
		return node;
	}
	//-----------------------------------------------------------------------

}
//$endcategory roops.core.objects
