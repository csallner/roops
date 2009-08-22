//$category roops.extended.bv32.arr.noex

// Authors: Nikolai Tillmann and Christoph Csallner
//$benchmarkclass
public class IntArrayWithoutExceptionsWithArrayParameters
{
    #region Basic
    //$goals 2
    //$benchmark
    public void DirectWriteFollowedByDirectRead(int[] a, int v)
    {
        if (a != null && RoopsArray.getLength(a) >= 10)
        {
            a[4] = v;
            if (a[4] == 42)
            { /*$goal 0*/}
            else
            { /*$goal 1*/}
        }
    }

    //$goals 3
    //$benchmark
    public void DirectWriteFollowedByIndirectRead(int[] a, int j, int v)
    {
        if (a != null && RoopsArray.getLength(a) >= 10)
        {
            if (j >= 0 && j < RoopsArray.getLength(a))
            {
                a[4] = v;
                if (a[j] == 42)
                { /*$goal 0*/}
                else
                { /*$goal 1*/}
            }
            else
            { /*$goal 2*/}
        }
    }

    //$goals 3
    //$benchmark
    public void IndirectWriteFollowedByIndirectRead(int[] a, int i, int j, int v)
    {
        if (a != null)
        {
            if (i >= 0 && i < RoopsArray.getLength(a) &&
                j >= 0 && j < RoopsArray.getLength(a))
            {
                a[i] = v;
                if (a[j] == 42)
                { /*$goal 0*/}
                else
                { /*$goal 1*/}
            }
            else
            { /*$goal 2*/}
        }
    }
    #endregion
}
//$endcategory roops.extended.bv32.arr.noex