package roops.shoppingcart.coupons;

import roops.shoppingcart.cart.Cart;
import roops.shoppingcart.products.Product;

/**
 * This class represents a {@link Coupon} that provides a discount on some
 * {@link Product} objects that can be added to a {@link Cart}.
 * 
 * @author <a href="http://www.vilasjagannath.com">Vilas Jagannath</a>
 */
public interface Coupon {

	/**
	 * Returns a brief description of this {@link Coupon}.
	 * 
	 * @return a brief description of this {@link Coupon}.
	 */
	String getDescription();

	/**
	 * Returns the discount value of this {@link Coupon} for the given
	 * {@link Cart}; returns zero if this {@link Coupon} is not applicable for
	 * the given {@link Cart}.
	 * 
	 * @param cart
	 *            the {@link Cart} for which the discount value of this
	 *            {@link Coupon} is to be computed.
	 * 
	 * @return the discount value of this {@link Coupon} for the given
	 *         {@link Cart}; returns zero if this {@link Coupon} is not
	 *         applicable for the given {@link Cart}.
	 */
	double getValue(Cart cart);

}
