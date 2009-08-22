//$category roops.core.bv32.linear.noex.gods

// Authors: Nikolai Tillmann and Christoph Csallner
//$benchmarkclass
public class LinearWithoutOverflow
{
    #region Equals
    //$goals 2
    //$benchmark
    public void XPlusYEqualsZ(int x, int y, int z)
    {
        if (x + y == z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void XPlusCEqualsZ(int x, int z)
    {
        if (x + 93287 == z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void XTimesCEqualsZ(int x, int z)
    {
        if (x * 74 == z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void XMinusYEqualsZ(int x, int y, int z)
    {
        if (x - y == z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void XMinusCEqualsZ(int x, int z)
    {
        if (x - 93287 == z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void MinusYEqualsZ(int y, int z)
    {
        if (-y == z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void MinusYEqualsC(int y)
    {
        if (-y == 234567)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }
    #endregion

    #region Unequals
    //$goals 2
    //$benchmark
    public void XPlusYUnequalsZ(int x, int y, int z)
    {
        if (x + y != z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void XPlusCUnequalsZ(int x, int z)
    {
        if (x + 93287 != z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void XTimesCUnequalsZ(int x, int z)
    {
        if (x * 74 != z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void XMinusYUnequalsZ(int x, int y, int z)
    {
        if (x - y != z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void XMinusCUnequalsZ(int x, int z)
    {
        if (x - 93287 != z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void MinusYUnequalsZ(int y, int z)
    {
        if (-y != z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void MinusYUnequalsC(int y)
    {
        if (-y != 234567)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }
    #endregion

    #region GreaterThan
    //$goals 2
    //$benchmark
    public void XPlusYGreaterThanZ(int x, int y, int z)
    {
        if (x + y > z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void XPlusCGreaterThanZ(int x, int z)
    {
        if (x + 93287 > z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void XTimesCGreaterThanZ(int x, int z)
    {
        if (x * 74 > z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void XMinusYGreaterThanZ(int x, int y, int z)
    {
        if (x - y > z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void XMinusCGreaterThanZ(int x, int z)
    {
        if (x - 93287 > z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void MinusYGreaterThanZ(int y, int z)
    {
        if (-y > z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void MinusYGreaterThanC(int y)
    {
        if (-y > 234567)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }
    #endregion

    #region GreaterEqualThan
    //$goals 2
    //$benchmark
    public void XPlusYGreaterEqualThanZ(int x, int y, int z)
    {
        if (x + y >= z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void XPlusCGreaterEqualThanZ(int x, int z)
    {
        if (x + 93287 >= z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void XTimesCGreaterEqualThanZ(int x, int z)
    {
        if (x * 74 >= z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void XMinusYGreaterEqualThanZ(int x, int y, int z)
    {
        if (x - y >= z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void XMinusCGreaterEqualThanZ(int x, int z)
    {
        if (x - 93287 >= z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void MinusYGreaterEqualThanZ(int y, int z)
    {
        if (-y >= z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void MinusYGreaterEqualThanC(int y)
    {
        if (-y >= 234567)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }
    #endregion

    #region LessThan
    //$goals 2
    //$benchmark
    public void XPlusYLessThanZ(int x, int y, int z)
    {
        if (x + y < z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void XPlusCLessThanZ(int x, int z)
    {
        if (x + 93287 < z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void XTimesCLessThanZ(int x, int z)
    {
        if (x * 74 < z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void XMinusYLessThanZ(int x, int y, int z)
    {
        if (x - y < z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void XMinusCLessThanZ(int x, int z)
    {
        if (x - 93287 < z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void MinusYLessThanZ(int y, int z)
    {
        if (-y < z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void MinusYLessThanC(int y)
    {
        if (-y < 234567)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }
    #endregion

    #region LessEqualThan
    //$goals 2
    //$benchmark
    public void XPlusYLessEqualThanZ(int x, int y, int z)
    {
        if (x + y <= z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void XPlusCLessEqualThanZ(int x, int z)
    {
        if (x + 93287 <= z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void XTimesCLessEqualThanZ(int x, int z)
    {
        if (x * 74 <= z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void XMinusYLessEqualThanZ(int x, int y, int z)
    {
        if (x - y <= z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void XMinusCLessEqualThanZ(int x, int z)
    {
        if (x - 93287 <= z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void MinusYLessEqualThanZ(int y, int z)
    {
        if (-y <= z)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void MinusYLessEqualThanC(int y)
    {
        if (-y <= 234567)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }
    #endregion

    #region Combinations
    //$goals 2
    //$benchmark
    public void TestLinearEquations1(int i, int j)
    {
        if (i + j == 31 &&
            i - j == 5)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void TestLinearEquations2(int i, int j, int k)
    {
        if (2 * i + j == 31 &&
            i - 3 * j == k)
            { /*$goal 0*/}
        else
            { /*$goal 1*/}
    }
    #endregion
}
    
//$endcategory roops.core.bv32.linear.noex.gods
