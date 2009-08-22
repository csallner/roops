package roops.core.bv32.linear.noex.gods;

// Authors: Nikolai Tillmann and Christoph Csallner
import roops.util.RoopsArray; @roops.util.BenchmarkClass
public class LinearWithoutOverflow
{
    /* begin Equals */
    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void XPlusYEqualsZ(int x, int y, int z)
    {
        if (x + y == z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void XPlusCEqualsZ(int x, int z)
    {
        if (x + 93287 == z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void XTimesCEqualsZ(int x, int z)
    {
        if (x * 74 == z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void XMinusYEqualsZ(int x, int y, int z)
    {
        if (x - y == z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void XMinusCEqualsZ(int x, int z)
    {
        if (x - 93287 == z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void MinusYEqualsZ(int y, int z)
    {
        if (-y == z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void MinusYEqualsC(int y)
    {
        if (-y == 234567)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }
    /* end */

    /* begin Unequals */
    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void XPlusYUnequalsZ(int x, int y, int z)
    {
        if (x + y != z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void XPlusCUnequalsZ(int x, int z)
    {
        if (x + 93287 != z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void XTimesCUnequalsZ(int x, int z)
    {
        if (x * 74 != z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void XMinusYUnequalsZ(int x, int y, int z)
    {
        if (x - y != z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void XMinusCUnequalsZ(int x, int z)
    {
        if (x - 93287 != z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void MinusYUnequalsZ(int y, int z)
    {
        if (-y != z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void MinusYUnequalsC(int y)
    {
        if (-y != 234567)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }
    /* end */

    /* begin GreaterThan */
    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void XPlusYGreaterThanZ(int x, int y, int z)
    {
        if (x + y > z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void XPlusCGreaterThanZ(int x, int z)
    {
        if (x + 93287 > z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void XTimesCGreaterThanZ(int x, int z)
    {
        if (x * 74 > z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void XMinusYGreaterThanZ(int x, int y, int z)
    {
        if (x - y > z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void XMinusCGreaterThanZ(int x, int z)
    {
        if (x - 93287 > z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void MinusYGreaterThanZ(int y, int z)
    {
        if (-y > z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void MinusYGreaterThanC(int y)
    {
        if (-y > 234567)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }
    /* end */

    /* begin GreaterEqualThan */
    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void XPlusYGreaterEqualThanZ(int x, int y, int z)
    {
        if (x + y >= z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void XPlusCGreaterEqualThanZ(int x, int z)
    {
        if (x + 93287 >= z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void XTimesCGreaterEqualThanZ(int x, int z)
    {
        if (x * 74 >= z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void XMinusYGreaterEqualThanZ(int x, int y, int z)
    {
        if (x - y >= z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void XMinusCGreaterEqualThanZ(int x, int z)
    {
        if (x - 93287 >= z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void MinusYGreaterEqualThanZ(int y, int z)
    {
        if (-y >= z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void MinusYGreaterEqualThanC(int y)
    {
        if (-y >= 234567)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }
    /* end */

    /* begin LessThan */
    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void XPlusYLessThanZ(int x, int y, int z)
    {
        if (x + y < z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void XPlusCLessThanZ(int x, int z)
    {
        if (x + 93287 < z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void XTimesCLessThanZ(int x, int z)
    {
        if (x * 74 < z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void XMinusYLessThanZ(int x, int y, int z)
    {
        if (x - y < z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void XMinusCLessThanZ(int x, int z)
    {
        if (x - 93287 < z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void MinusYLessThanZ(int y, int z)
    {
        if (-y < z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void MinusYLessThanC(int y)
    {
        if (-y < 234567)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }
    /* end */

    /* begin LessEqualThan */
    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void XPlusYLessEqualThanZ(int x, int y, int z)
    {
        if (x + y <= z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void XPlusCLessEqualThanZ(int x, int z)
    {
        if (x + 93287 <= z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void XTimesCLessEqualThanZ(int x, int z)
    {
        if (x * 74 <= z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void XMinusYLessEqualThanZ(int x, int y, int z)
    {
        if (x - y <= z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void XMinusCLessEqualThanZ(int x, int z)
    {
        if (x - 93287 <= z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void MinusYLessEqualThanZ(int y, int z)
    {
        if (-y <= z)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void MinusYLessEqualThanC(int y)
    {
        if (-y <= 234567)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }
    /* end */

    /* begin Combinations */
    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void TestLinearEquations1(int i, int j)
    {
        if (i + j == 31 &&
            i - j == 5)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void TestLinearEquations2(int i, int j, int k)
    {
        if (2 * i + j == 31 &&
            i - 3 * j == k)
            {roops.util.Goals.reached(0);}
        else
            {roops.util.Goals.reached(1);}
    }
    /* end */
}
    
/* end roops.core.bv32.linear.noex.gods */
