/**
 * List implementation based on Rohan Sharma's implementation 
 * 
 * @author Marcelo Frias
 * 
 */
//$category roops.core.objects
public class RoopsIntList {

    private /*@ nullable @*/ RoopsIntListNode header;
    private int len;

    public RoopsIntList() {
        clear();
    }

    public void clear() {
        header = null;
        len = 0;
    }

    public boolean add(int elem) {
        RoopsIntListNode new_node = new RoopsIntListNode(elem);
        if (len==0) {
           header = new_node;
        } else {
           RoopsIntListNode curr = this.header;
           while (curr.next!=null) {
             curr = curr.next;
           }
           curr.next = new_node;
        }
        len++;
        return true;
    }

    public boolean removeIndex(int index) {
      
        if (index==0) {
          header = header.next;
        } else {
          int i=0;
          RoopsIntListNode prev = null;
          RoopsIntListNode curr = this.header;
          while (i<index) {
            prev = curr;
            curr = curr.next;
            i++;
          }
          prev.next = curr.next;
        }
        len--;

        return true;
    }

    public boolean removeElem(int elem) {
        return removeIndex(indexOf(elem));
    }


    public int indexOf(int elem) {
        RoopsIntListNode curr = this.header;
        int index = 0;
        while (curr!=null) {
	  if (curr.int_value==elem) {
            return index;
          }
          curr = curr.next;
          index++;
        }
        return -1;
    }

    public boolean isEmpty() {
        return len == 0;
    }

    public boolean contains(int elem) {
        return indexOf(elem) != -1;
    }

    public int getSize() {
        return len;
    }

}
//$endcategory roops.core.objects

