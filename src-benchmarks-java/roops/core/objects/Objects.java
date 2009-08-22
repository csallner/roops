package roops.core.objects;

// Authors: Nikolai Tillmann and Christoph Csallner
import roops.util.RoopsArray; @roops.util.BenchmarkClass
public class Objects
{
    /* begin Basic */
    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void NullOrNot(Object value)
    {
        if (value == null)
        {roops.util.Goals.reached(0);}
        else
        {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void EqualOrNot(Object value1, Object value2)
    {
        if (value1 == value2)
        {roops.util.Goals.reached(0);}
        else
        {roops.util.Goals.reached(1);}
    }

    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod static
    public void NotNullAndEqual(Object value1, Object value2)
    {
        if (value1 != null && value1 == value2)
        {roops.util.Goals.reached(0);}
        else
        {roops.util.Goals.reached(1);}
    }
    /* end */
}

/* end roops.core.objects */
