//$category roops.extended.bv32.floats

//Authors: Marcelo Frias
//$benchmarkclass
public class FloatArithmetic {

	//$goals 2
	//$benchmark
	public void Method1(float i) {

		if (i == i) {
			{ /*$goal 0 reachable*/}
		} else {
			{ /*$goal 1 reachable*/}
		}
	}

	//$goals 2
	//$benchmark
	public void Method2(float i, float j) {

		if ((i * j) <= (i * j)) {
			{ /*$goal 0 reachable*/}
		} else {
			{ /*$goal 1 reachable*/}
		}
	}

	//$goals 2
	//$benchmark
	public void Method3(float i, float j) {

		if ((i / j) <= (i / j)) {
			{ /*$goal 0 reachable*/}
		} else {
			{ /*$goal 1 reachable*/}
		}
	}

	//$goals 2
	//$benchmark
	public void Method4(float i) {

		if (((i + 1.0f) == i) == true && (i <= 3.4028235E38f) == true) {
			{ /*$goal 0 reachable*/}
		} else {
			{ /*$goal 1 reachable*/}
		}
	}

	//$goals 2
	//$benchmark
	public void Method5(float i) {

		if (((2f * i) == i) && (i > 0f)) {
			{ /*$goal 0 reachable*/}
		} else {
			{ /*$goal 1 reachable*/}
		}
	}

	//$goals 2
	//$benchmark
	public void Method6(float x1) {

		if (0.0f == (3.4028235E38f - x1)) {
			{ /*$goal 0 reachable*/}
		} else {
			{ /*$goal 1 reachable*/}
		}
	}

	//$goals 2
	//$benchmark
	public void Method7(float x1, float x2, float x3, float x4) {

		if ((1.5235646E32f >= (x1 - x2)) || ((((x3 / 3.82573E28f) - 19.839f) * (x4 + 237.76f)) >= 1.92164E33f)) {
			{ /*$goal 0 reachable*/}
		} else {
			{ /*$goal 1 reachable*/}
		}
	}

	//$goals 2
	//$benchmark
	public void Method8(float x1, float x2, float x3, float x4, float x5, float x6, float x7) {

		if ((((-114.51E19f * x1) * (x2 / (x3 - x4))) < x5) && (x6 <= x7)) {
			{ /*$goal 0 reachable*/}
		} else {
			{ /*$goal 1 reachable*/}
		}
	}
	
	
	
	//$goals 2
	//$benchmark
	public void Method9(float x1, float x2, float x3, float x4, float x5, float x6, float x7, float x8, float x9, float x10)
	{
		
		if (((x1 +(x2 /(((x3 *(x4 /((x5 - x6) / x7))) + 269.98f) *((62.81f - 16.08f) *(((x8 * 227.86f) / 12.221f) + 2.3981f))))) < x9) && (x10 <= 2.545f)) {
			{ /*$goal 0 reachable*/}
		} else {
			{ /*$goal 1 reachable*/}
		}
	}
	

	
	//$goals 2
	//$benchmark
	public void Method10(float x1, float x2, float x3, float x4, float x5, float x6, float x7, float x8, float x9, float x10, float x11, float x12, float x13)
	{

		if ((((x1 /((2.2826E17f / x2) / 10.630E22f)) * x3) <= 122.43f) ||((x4 * x5) ==((((((x6 - 122.20f) * (2.3659f *((25.01E13f * 474.3f) - 224.5f))) * x7) /((x8 * x9) /(x10 + x11))) / x12) - x13))) {
			{ /*$goal 0 reachable*/}
		} else {
			{ /*$goal 1 reachable*/}
		}
	}
	
	
	
	//$goals 2
	//$benchmark
	public void Method11(float x1, float x2, float x3, float x4, float x5, float x6, float x7, float x8, float x9, float x10, float x11, float x12, float x13, float x14, float x15, float x16)
	{

		if (((((2.2421f / x1) -(x2 - 81.95f)) ==(((x3 - x4) + x5) -(((314.80f + 8.158f) +(20.928f -(244.19f * 1626.5E18f))) / x6))) && ((((((x7 +(x8 + x9)) + x10) + x11) / 2.5671f) >= 20.301f) || (411.8f >((x12 -1388.4749f) +(x13 - x14))))) || (((1102.5f + x15) * x16) == 2.7724E32f)) {
			{ /*$goal 0 unknown*/}
		} else {
			{ /*$goal 1 unknown*/}
		}
	}
	
	
	
	//$goals 2
	//$benchmark
	public void Method12(float x1, float x2, float x3, float x4, float x5, float x6, float x7, float x8, float x9, float x10, float x11, float x12, float x13, float x14, float x15, float x16, float x17, float x18, float x19, float x20, float x21)
	{
	
	if( x1 != ((x2 + 6.510E19f) * (20.104E18f - ((2.704E25f *(x3 /((73.71E12f * x4) *(x5 / x6)))) - (((x7 - (((419.9E29f /(x8 *((x9 * x10) * 1.7475f))) * x11) * (((30.04E14f - (x12 * x13)) * 953.1f) * 2.182f))) - (x14 * (66.41E11f + (x15 - (((130.77E17f / ((x16 / x17) * (16.796E27f / ((x18 * x19) / ((1.1274E15f - x20) / 295.77E23f))))) * x21) * 17.982f))))) - 224.50E30f))))) {
			{ /*$goal 0 unknown*/}
		} else {
			{ /*$goal 1 unknown*/}
        }

    }
	
	
	
	//$goals 2
	//$benchmark
	public void Method13(float x1, float x2, float x3, float x4, float x5, float x6, float x7, float x8, float x9, float x10, float x11, float x12, float x13, float x14, float x15, float x16, float x17, float x18, float x19, float x20, float x21, float x22, float x23, float x24, float x25, float x26, float x27, float x28, float x29, float x30, float x31, float x32, float x33, float x34, float x35, float x36, float x37, float x38, float x39, float x40, float x41)
	{
		
		if (((x1 + x2 + x3 + x4 + x5 + x6 + x7 + x8 + x9 + x10 + x11 + x12 + x13 + x14 + x15 + x16 + x17 + x18 + x19 + x20 + x21 + x22 + x23 + x24 + x25 + x26 + x27 + x28 + x29 + x30 + x31 + x32 + x33 + x34 + x35 + x36 + x37 + x38 + x39 + x40 + x41) * 1.25437E-10f) <= 1.3E34f) {
			{ /*$goal 0 unknown*/}
		} else {
			{ /*$goal 1 unknown*/}
                }
	}
	
	
}
//$endcategory roops.extended.bv32.floats
