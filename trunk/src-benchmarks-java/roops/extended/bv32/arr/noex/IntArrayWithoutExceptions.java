package roops.extended.bv32.arr.noex;

// Authors: Nikolai Tillmann and Christoph Csallner
import roops.util.RoopsArray; @roops.util.BenchmarkClass
public class IntArrayWithoutExceptions
{

    /* begin Basic */
    @roops.util.NrOfGoals(3)
    @roops.util.BenchmarkMethod static
    public void BigArrayManyUpdates1(int k)
    {
        int[] a = new int[100];
        for (int i = 0; i < RoopsArray.getLength(a); i++)
            a[i] = i;
        if (k >= 0 && k < RoopsArray.getLength(a) &&
            a[k] == 50)
        {roops.util.Goals.reached(0);}
        else
        {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(3)
    @roops.util.BenchmarkMethod static
    public void BigArrayManyUpdates2(int j, int k)
    {
        int[] a = new int[100000];
        for (int i = 0; i < RoopsArray.getLength(a); i++)
            a[i] = i == (j - 100) ? 1 : 2;
        if (k >= 0 && k < 100000 &&
            a[k] == 1)
        {roops.util.Goals.reached(0);}
        else
        {roops.util.Goals.reached(1);}
    }
    /* end */
}
/* end roops.extended.bv32.arr.noex */
