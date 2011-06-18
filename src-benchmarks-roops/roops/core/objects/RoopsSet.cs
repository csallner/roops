/**
 * Set implementation based on Rohan Sharma's implementation which uses RoopsMap with dummy value
 * 
 * @author Marcelo Frias
 * 
 */

//$category roops.core.objects

public class RoopsSet {
    private static Object UNUSED = new Object();    

    private /*@ nullable @*/ RoopsMap map;
 
    public RoopsSet() {
      map = new RoopsMap();
    }

    public boolean isEmpty() {
        return map.isEmpty();
    }
    
    public boolean add(Object key) {
        if (contains(key))
          return false;

        map.put(key, UNUSED);
        return true;
    }
    
    public boolean contains(Object key) {
        return map.containsKey(key);
    }
 
    public boolean remove(Object key) {
        return map.remove(key) != null;
    }
    
    public int getSize() {
        return map.getSize();
    }
}
//$endcategory roops.core.objects

