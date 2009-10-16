package roops.shoppingcart.coupons;

import java.util.Set;

import roops.shoppingcart.cart.Cart;
import roops.shoppingcart.products.BlackPen;


/**
 * This class represents a {@link Coupon} for {@link BlackPen}s using which two
 * {@link BlackPen}s can be bought for the price of one.
 * 
 * @author <a href="http://www.vilasjagannath.com">Vilas Jagannath</a>
 */
@roops.util.BenchmarkClass
public class AnniversaryCoupon implements Coupon {

    public String getDescription() {
        return "Anniversary coupon: Buy one get one free for black pens only!";
    }

    /**
     * Checks that no other coupons are being used. If so, for every two
     * {@link BlackPen}s a discount value equal to the price of one
     * {@link BlackPen} is added.
     * 
     * @see Coupon#getValue(Cart)
     */
    @roops.util.NrOfGoals(3)
    @roops.util.BenchmarkMethod
    public double getValue(Cart cart) {

        Set<Coupon> coupons = cart.getCoupons();
        if (coupons.size() > 1 || !coupons.contains(this)) {
            roops.util.Goals.reached(0);
            return 0;
        }

        BlackPen blackPen = new BlackPen();
        Integer blackPensQuantity = cart.getProductQuantities().get(blackPen);
        if (blackPensQuantity == null) {
            blackPensQuantity = 0;
        }

        double value = (blackPensQuantity / 2) * blackPen.getPrice();
        if (value <= 0) {
            roops.util.Goals.reached(1);
        } else {
            roops.util.Goals.reached(2);
        }
        return value;
    }

}
/* end edu.uiuc.coupons */
