//$category roops.core.objects

public class RoopsListNode {
  
  /*@ nullable @*/ RoopsListNode next;  

  /*@ nullable @*/ Object object_value;

  public RoopsListNode(Object new_object_value) {
    this.object_value = new_object_value;
    this.next = null;
  }

}

//$endcategory roops.core.objects
