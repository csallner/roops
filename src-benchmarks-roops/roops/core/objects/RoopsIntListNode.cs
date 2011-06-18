//$category roops.core.objects

public class RoopsIntListNode {
  
  /*@ nullable @*/ RoopsIntListNode next;  

  /*@ nullable @*/ int int_value;

  public RoopsIntListNode(int new_int_value) {
    this.int_value = new_int_value;
    this.next = null;
  }

}

//$endcategory roops.core.objects
