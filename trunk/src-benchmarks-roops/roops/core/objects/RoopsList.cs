/**
 * List implementation based on Rohan Sharma's implementation 
 * 
 * @author Marcelo Frias
 * 
 */
//$category roops.core.objects
public class RoopsList {

    private /*@ nullable @*/ RoopsListNode header;
    private int len;

    public RoopsList() {
        clear();
    }

    public void clear() {
        header = null;
        len = 0;
    }

    public boolean add(Object elem) {
        RoopsListNode new_node = new RoopsListNode(elem);
        len++;
        if (len==0) {
           header = new_node;
        } else {
           RoopsListNode curr = this.header;
           while (curr.next!=null) {
             curr = curr.next;
           }
           curr.next = new_node;
        }
        return true;
    }

    public boolean remove(int index) {
      
        if (index==0) {
          header = header.next;
        } else {
          int i=0;
          RoopsListNode prev = null;
          RoopsListNode curr = this.header;
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

    public Object get(int index) {
        int i=0;
        RoopsListNode curr = this.header;
        RoopsListNode prev = null;
        while (i<index) {
          prev = curr;
          curr = curr.next;
          i++;
        }
        return curr.object_value;
    }


    public boolean remove(Object elem) {
        return remove(indexOf(elem));
    }


    public int indexOf(Object elem) {
        RoopsListNode curr = this.header;
        int index = 0;
        while (curr!=null) {
	  if (curr.object_value==elem) {
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

    public boolean contains(Object elem) {
        return indexOf(elem) != -1;
    }

    public int getSize() {
        return len;
    }

}
//$endcategory roops.core.objects

