//$category roops.core.objects

//Authors: Marcelo Frias
/**
 * source Apache Harmony software http://harmony.apache.org/
 *         http://www.docjar.com/html/api/java/util/ArrayList.java.html
 */
//$benchmarkclass
public class ArrayList {

	//$goals 6
	//$benchmark
	public void containsTest(ArrayList arrayList, int o) {
		if (arrayList != null) {
			{ /*$goal 0 reachable*/}
			boolean ret_val = arrayList.contains(o);
		} else {
			{ /*$goal 1 reachable*/}
		}
	}
	
	//$goals 12
	//$benchmark
	public void removeTest(ArrayList arrayList, int index) {
		if (arrayList != null) {
			{ /*$goal 0 reachable*/}
			Object ret_val = arrayList.remove(index);
		} else {
			{ /*$goal 1 reachable*/}
		}
	}

	//$goals 18
	//$benchmark
	public void addTest(ArrayList arrayList, int o) {
		if (arrayList != null) {
			{ /*$goal 0 reachable*/}
			boolean ret_val = arrayList.add(o);
		} else {
			{ /*$goal 1 reachable*/}
		}
	}

	private int firstIndex;

	private int lastIndex;

	private Object[] array;

        private int arrayLength;

	private int modCount;

	public boolean contains(Object elem) {

		if (elem != null) {
			/*$ goal 2 reachable*/
			for (int i = firstIndex; i < lastIndex; i++) {
				/*$ goal 4 reachable*/
				if (elem_equals(elem, array[i])) {
					/*$ goal 6 reachable*/
					return true;
				} else {
					/*$ goal 7 reachable*/
				}
			}
			/*$ goal 5 reachable*/
		} else {
			/*$ goal 3 reachable*/
			for (int i = firstIndex; i < lastIndex; i++) {
				/*$ goal 8 reachable*/
				if (array[i] == null) {
					/*$ goal 10 reachable*/
					return true;
				} else {
					/*$ goal 11 reachable*/
				}
			}
			/*$ goal 9 reachable*/
		}
		return false;
	}

	private boolean elem_equals(Object elem1, Object elem2) {
		return elem1 == elem2;
	}

	public boolean add(Object elem) {
		if (lastIndex == arrayLength) {
			/*$ goal 2 reachable*/
			growAtEnd(1);
		} else {
			/*$ goal 3 reachable*/
		}
		array[lastIndex] = elem;
		lastIndex++;
		modCount++;
		return true;
	}

	private void growAtEnd(int required) {
		int size = lastIndex - firstIndex;
		int sufix_length = arrayLength - lastIndex;
		if (firstIndex >= required - sufix_length) {
			/*$ goal 4 reachable*/
			
			int newLast = lastIndex - firstIndex;
			if (size > 0) {
				/*$ goal 6 reachable*/
				add_system_arraycopy(array, firstIndex, array, 0, size);

				int start;
				if (newLast < firstIndex) {
					/*$ goal 8 reachable*/
					start = firstIndex;
				} else {
					/*$ goal 9 reachable*/
					start = newLast;
				}

				arrays_fill(start);
			} else {
				/*$ goal 7 reachable*/
			}
			firstIndex = 0;
			lastIndex = newLast;
		} else {

			/*$ goal 5 reachable*/

			int increment = size >> 1;

			if (required > increment) {
				/*$ goal 10 reachable*/
				increment = required;
			} else {
				/*$ goal 11 reachable*/
			}
			if (increment < 12) {
				/*$ goal 12 reachable*/
				increment = 12;
			} else {
				/*$ goal 13 reachable*/
			}
			Object[] newArray = newElementArray(size + increment);
			if (size > 0) {
				/*$ goal 14 reachable*/				
				add_system_arraycopy(array, firstIndex, newArray, 0, size);
				firstIndex = 0;
				lastIndex = size;
			} else {
				/*$ goal 15 reachable*/
			}
			array = newArray;
                        arrayLength = size + increment;
			
		}
		
	}

	private static Object[] newElementArray(int size) {
		Object[] result = new Object[size];
		return result;
	}

	public Object remove(int location) {
		Object result;
		int size = lastIndex - firstIndex;
		if (0 <= location && location < size) {
			/*$ goal 2 reachable*/
			if (location == size - 1) {
				/*$ goal 4 reachable*/
				lastIndex--;
				result = array[lastIndex];
				array[lastIndex] = null;
			} else {
				/*$ goal 5 reachable*/

			  if (location == 0) {
				/*$ goal 6 reachable*/
				result = array[firstIndex];
				array[firstIndex] = null;
				firstIndex++;
			  } else {
				/*$ goal 7 reachable*/
     			int elementIndex = firstIndex + location;
				result = array[elementIndex];
				int size_div_2 = size >> 1;
				if (location < size_div_2) {
					/*$ goal 8 reachable*/
					system_arraycopy(array, firstIndex, array, firstIndex + 1, location);
					array[firstIndex] = null;
					firstIndex++;
				} else {
					/*$ goal 9 reachable*/
					system_arraycopy(array, elementIndex + 1, array, elementIndex, size - location - 1);
					lastIndex--;
					array[lastIndex] = null;
				}
			  }
		    }
			
			if (firstIndex == lastIndex) {
				firstIndex = lastIndex = 0;
			}
		} else {
			/*$ goal 3 reachable*/
			throw new IndexOutOfBoundsException();
		}

		modCount++;
		return result;
	}

	private void system_arraycopy(Object[] src, int srcPos, Object[] dest, int destPos, int length) {
		for (int i = 0; i < length; i++) {
			/*$ goal 10 reachable*/
			int srcIndex = srcPos + i;
			int destIndex = destPos + i;
			dest[destIndex] = src[srcIndex];
		}
		/*$ goal 11 reachable*/
	}

	private void add_system_arraycopy(Object[] src, int srcPos, Object[] dest, int destPos, int length) {
		for (int i = 0; i < length; i++) {
			/*$ goal 18 reachable*/
			int srcIndex = srcPos + i;
			int destIndex = destPos + i;
			dest[destIndex] = src[srcIndex];
		}
		/*$ goal 19 reachable*/
	}

	private void arrays_fill(int start) {
		for (int i = start; i < this.arrayLength; i++) {
			/*$ goal 16 reachable*/
			this.array[i] = null;
		}
		/*$ goal 17 reachable*/
	}

}
//$endcategory roops.core.objects

