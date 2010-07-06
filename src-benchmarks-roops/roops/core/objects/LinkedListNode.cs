/****************************************************************************
Author: Juan Pablo Galeotti and Marcelo Frias, Relational Formal Methods 
Group, University of Buenos Aires and Buenos Aires Institute of Technology,
Buenos Aires, Argentina.
Companion LinkedListNode implementation for a ROOPS class implementing the 
apache.commons.collections class NodeCachingLinkedList.

The class has annotations in JFSL [1] given as ROOPS comments.

[1] http://sdg.csail.mit.edu/forge/plugin.html
****************************************************************************/



//$category roops.core.objects

public class LinkedListNode {

    /** A pointer to the node before this node */
    public /*@ nullable @*/LinkedListNode previous;
    /** A pointer to the node after this node */
    public /*@ nullable @*/LinkedListNode next;
    /** The object contained within this node */
    public /*@ nullable @*/Object value;


    public /*@ pure @*/Object getValue() {
        return value;
    }
    
    public void setValue(Object value) {
        this.value = value;
    }
    
    public LinkedListNode getPreviousNode() {
        return previous;
    }
    
    public void setPreviousNode(LinkedListNode previous) {
        this.previous = previous;
    }
    
    public LinkedListNode getNextNode() {
        return next;
    }
    
    public void setNextNode(LinkedListNode next) {
        this.next = next;
    }
}
//$endcategory roops.core.objects

