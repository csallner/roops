package roops.shoppingcart.coupons;

import roops.shoppingcart.cart.Cart;
import roops.shoppingcart.products.RedPen;

/**
 * This class represents an {@link Coupon} for {@link RedPen} objects using
 * which three {@link RedPen} objects can be bought for the price of one.
 * 
 * @author <a href="http://www.vilasjagannath.com">Vilas Jagannath</a>
 */
@roops.util.BenchmarkClass
public class ClearanceCoupon implements Coupon {

    public String getDescription() {
        return "Clearance coupon: Buy one red pen, get two red pens free!";
    }

    /**
     * For every three {@link RedPen}s, a discount value equal to the price of
     * two {@link RedPen}s is added. This {@link ClearanceCoupon} can be
     * applied along with any other {@link Coupon}.
     * 
     * @see Coupon#getValue(Cart)
     */
    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod
    public double getValue(Cart cart) {

        RedPen redPen = new RedPen();
        Integer redPensQuantity = cart.getProductQuantities().get(redPen);
        if (redPensQuantity == null) {
            redPensQuantity = 0;
        }

        double value = (redPensQuantity / 3) * 2 * redPen.getPrice();
        if (value <= 0) {
            roops.util.Goals.reached(0);
        } else {
            roops.util.Goals.reached(1);
        }
        return value;
    }

}

/* end edu.uiuc.coupons */
