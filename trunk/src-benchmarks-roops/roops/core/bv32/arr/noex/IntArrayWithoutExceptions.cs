//$category roops.core.bv32.arr.noex

// Authors: Nikolai Tillmann and Christoph Csallner
//$benchmarkclass
public class IntArrayWithoutExceptions
{


    #region Basic
    //$goals 2
    //$benchmark
    public void ArrayInitialization()
    {
        int[] a = new int[10];
        if (a[0] != 0)
        { /*$goal 0 unreachable*/}
        else
        { /*$goal 1*/}
    }

    //$goals 3
    //$benchmark
    public void ArrayInitializationVariableSize(int size)
    {
        if (size > 0)
        {
            int[] a = new int[size];
            if (a[0] != 0)
            { /*$goal 0 unreachable*/}
            else
            { /*$goal 1*/}
        }
        else
        { /*$goal 2*/}
    }

    //$goals 3
    //$benchmark
    public void ArrayInitializationVariableSizeVariableIndex(int size, int index)
    {
        if (size > 0 &&
            index >= 0 &&
            index < size)
        {
            int[] a = new int[size];
            if (a[index] != 0)
            { /*$goal 0 unreachable*/}
            else
            { /*$goal 1*/}
        }
        else
        { /*$goal 2*/}
    }

    //$goals 2
    //$benchmark
    public void DirectWriteFollowedByDirectRead(int v)
    {
        int[] a = new int[10];
        a[4] = v;
        if (a[4] == 42)
        { /*$goal 0*/}
        else
        { /*$goal 1*/}
    }

    //$goals 3
    //$benchmark
    public void DirectWriteFollowedByIndirectRead(int j, int v)
    {
        int[] a = new int[10];
        if (j >= 0 && j < 10)
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

    //$goals 3
    //$benchmark
    public void IndirectWriteFollowedByIndirectRead(int i, int j, int v)
    {
        int[] a = new int[10];
        if (i >= 0 && i < 10 &&
            j >= 0 && j < 10)
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
    #endregion

    #region Big
    //$goals 3
    //$benchmark
    public void BigArray(int i, int j)
    {
        if (i >= 0 && i < 100000 &&
            j >= 0 && j < 100000)
        {
            int[] a = new int[100000];
            a[i] = 42;
            if (a[j] == 42)
            { /*$goal 0*/}
            else
            { /*$goal 1*/}
        }
        else
        { /*$goal 2*/}
    }
    #endregion
}
//$endcategory roops.core.bv32.arr.noex