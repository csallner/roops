/**
 * Map implementation based on Rohan Sharma's implementation which uses RoopsLists
 * 
 * @author Marcelo Frias
 * 
 */

//$category roops.core.objects

public class RoopsMap {

    private /*@ nullable @*/ RoopsList keys;
    private /*@ nullable @*/ RoopsList vals;

    private int size;

    public RoopsMap() {
        keys = new RoopsList();
        vals = new RoopsList();
    }

    public boolean isEmpty() {
        return keys.isEmpty();
    }

    public boolean containsKey(Object key) {
        return keys.contains(key);
    }

    public Object get(Object key) {
        if (containsKey(key)) {
          return vals.get(keys.indexOf(key));
        } else {
          return null;
        }
    }

    public void put(Object key, Object value) {
        if (containsKey(key)) {
          remove(key);
        }

        keys.add(key);
        vals.add(value);

        size++;
    }

    public Object remove(Object key) {
        int index = keys.indexOf(key);
        if (index!=-1) {
          Object value = vals.get(index);

          keys.remove(index);
          vals.remove(index);
          size--;

          return value;
        } else {
          return null;
        }
    }

    public  RoopsList values() {
        return vals;
    }

    public int getSize() {
        return size;
    }

    public  RoopsList keys() {
        return keys;
    }

}
//$endcategory roops.core.objects

