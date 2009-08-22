//$category roops.core.objects

// Authors: Nikolai Tillmann and Christoph Csallner
//$benchmarkclass
public class Objects
{
    #region Basic
    //$goals 2
    //$benchmark
    public void NullOrNot(Object value)
    {
        if (value == null)
        { /*$goal 0*/}
        else
        { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void EqualOrNot(Object value1, Object value2)
    {
        if (value1 == value2)
        { /*$goal 0*/}
        else
        { /*$goal 1*/}
    }

    //$goals 2
    //$benchmark
    public void NotNullAndEqual(Object value1, Object value2)
    {
        if (value1 != null && value1 == value2)
        { /*$goal 0*/}
        else
        { /*$goal 1*/}
    }
    #endregion
}

//$endcategory roops.core.objects