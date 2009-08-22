//$category roops.extended.bv32.arr.noex

// Authors: Nikolai Tillmann and Christoph Csallner
//$benchmarkclass
public class IntArrayWithoutExceptions
{

    #region Basic
    //$goals 3
    //$benchmark
    public void BigArrayManyUpdates1(int k)
    {
        int[] a = new int[100];
        for (int i = 0; i < RoopsArray.getLength(a); i++)
            a[i] = i;
        if (k >= 0 && k < RoopsArray.getLength(a) &&
            a[k] == 50)
        { /*$goal 0*/}
        else
        { /*$goal 1*/}
    }

    //$goals 3
    //$benchmark
    public void BigArrayManyUpdates2(int j, int k)
    {
        int[] a = new int[100000];
        for (int i = 0; i < RoopsArray.getLength(a); i++)
            a[i] = i == (j - 100) ? 1 : 2;
        if (k >= 0 && k < 100000 &&
            a[k] == 1)
        { /*$goal 0*/}
        else
        { /*$goal 1*/}
    }
    #endregion
}
//$endcategory roops.extended.bv32.arr.noex