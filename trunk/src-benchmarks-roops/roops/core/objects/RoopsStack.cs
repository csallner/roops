/**
 * Stack implementation based on RoopsList 
 * 
 * @author Marcelo Frias
 * 
 */
//$category roops.core.objects
public class RoopsStack {

  private /*@ nullable @*/ RoopsList list;

  public RoopsStack() {
    list = new RoopsList();
  }

  public void push(Object new_object) {
    list.add(new_object);
  }
  
  public Object pop() {
    int last_index = list.getSize() - 1;
    Object ret_val = list.get(last_index);
    list.remove(last_index);
    return ret_val; 
  }

  public int getSize() {
    return list.getSize();
  }
}
//$endcategory roops.core.objects

