package roops.core.bv32.arr.noex;

// Authors: Nikolai Tillmann and Christoph Csallner
import roops.util.RoopsArray; @roops.util.BenchmarkClass
public class IntArrayWithoutExceptions
{


    /* begin Basic */
    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void ArrayInitialization()
    {
        int[] a = new int[10];
        if (a[0] != 0)
        {roops.util.Goals.reached(0, roops.util.Verdict.UNREACHABLE);}
        else
        {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(3)
    @roops.util.BenchmarkMethod static
    public void ArrayInitializationVariableSize(int size)
    {
        if (size > 0)
        {
            int[] a = new int[size];
            if (a[0] != 0)
            {roops.util.Goals.reached(0, roops.util.Verdict.UNREACHABLE);}
            else
            {roops.util.Goals.reached(1);}
        }
        else
        {roops.util.Goals.reached(2);}
    }

    @roops.util.NrOfGoals(3)
    @roops.util.BenchmarkMethod static
    public void ArrayInitializationVariableSizeVariableIndex(int size, int index)
    {
        if (size > 0 &&
            index >= 0 &&
            index < size)
        {
            int[] a = new int[size];
            if (a[index] != 0)
            {roops.util.Goals.reached(0, roops.util.Verdict.UNREACHABLE);}
            else
            {roops.util.Goals.reached(1);}
        }
        else
        {roops.util.Goals.reached(2);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void DirectWriteFollowedByDirectRead(int v)
    {
        int[] a = new int[10];
        a[4] = v;
        if (a[4] == 42)
        {roops.util.Goals.reached(0);}
        else
        {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(3)
    @roops.util.BenchmarkMethod static
    public void DirectWriteFollowedByIndirectRead(int j, int v)
    {
        int[] a = new int[10];
        if (j >= 0 && j < 10)
        {
            a[4] = v;
            if (a[j] == 42)
            {roops.util.Goals.reached(0);}
            else
            {roops.util.Goals.reached(1);}
        }
        else
        {roops.util.Goals.reached(2);}
    }

    @roops.util.NrOfGoals(3)
    @roops.util.BenchmarkMethod static
    public void IndirectWriteFollowedByIndirectRead(int i, int j, int v)
    {
        int[] a = new int[10];
        if (i >= 0 && i < 10 &&
            j >= 0 && j < 10)
        {
            a[i] = v;
            if (a[j] == 42)
            {roops.util.Goals.reached(0);}
            else
            {roops.util.Goals.reached(1);}
        }
        else
        {roops.util.Goals.reached(2);}
    }
    /* end */

    /* begin Big */
    @roops.util.NrOfGoals(3)
    @roops.util.BenchmarkMethod static
    public void BigArray(int i, int j)
    {
        if (i >= 0 && i < 100000 &&
            j >= 0 && j < 100000)
        {
            int[] a = new int[100000];
            a[i] = 42;
            if (a[j] == 42)
            {roops.util.Goals.reached(0);}
            else
            {roops.util.Goals.reached(1);}
        }
        else
        {roops.util.Goals.reached(2);}
    }
    /* end */
}
/* end roops.core.bv32.arr.noex */
