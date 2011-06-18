//$category roops.extended.bv32.arr
//$benchmarkclass

public class BinSearchError {

       
	//$goals 1
	//$benchmark
       public void test_bin_search(int[] a, int key) {
         if (a!=null) {
           int ret_val = binarySearch(a,key);
         }
       }	

       public static int binarySearch(int[] a, int key) {
		int low = 0;
		int high = RoopsArray.getLength(a) - 1;
		int mid;
		int midVal ;
		
		while (low <= high)  {
			mid = (low + high) /2;

                        if (mid<0) {
                          /*$goal 0 reachable*/
                        }

			midVal = a[mid];

			if (midVal < key)
				low = mid + 1;
			else if (midVal > key)
				high = mid - 1;
			else {
				return mid; // key found
			}
		}
		
		return -(low+1);
	}
}
//$endcategory roops.extended.bv32.arr
