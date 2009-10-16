package roops.shoppingcart.cart;

import java.text.NumberFormat;
import java.util.Collection;
import java.util.Comparator;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;
import java.util.TreeMap;
import java.util.Map.Entry;

import roops.shoppingcart.coupons.Coupon;
import roops.shoppingcart.products.Product;


/**
 * This class represents a shopping cart that contains a number of
 * {@link Product} and {@link Coupon} objects.
 * 
 * @author <a href="http://www.vilasjagannath.com">Vilas Jagannath</a>
 */

@roops.util.BenchmarkClass
public class Cart {

    private static final String MINUS = "-";
    private static final String PRICE = "Price: ";
    private static final String TOTAL = "Total: ";
    private static final String NEW_LINE = "\n";
    private static final String SUB_TOTAL = "Sub-total: ";
    private static final String COMMA_SPACE = ", ";
    private static final String QUANTITY = "Quantity: ";

    private Map<Product, Integer> productQuantities;
    private double totalWithoutCoupons;
    private Set<Coupon> coupons;

    /**
     * Creates a new, empty shopping cart.
     */
    public Cart() {
        productQuantities = new TreeMap<Product, Integer>(
                new ProductBillComparator());
        coupons = new HashSet<Coupon>();
        totalWithoutCoupons = 0;
    }

    /**
     * Adds the given {@link #quantity} of the given {@link Product} to this
     * {@link Cart}.
     * 
     * @param product
     *            the {@link Product} to be added to this {@link Cart}. Cannot
     *            be <code>null</code>.
     * 
     * @param quantity
     *            the quantity of the given {@link Product} to be added to this
     *            {@link Cart}. Has to be greater than zero.
     * 
     */
    @roops.util.NrOfGoals(3)
    @roops.util.BenchmarkMethod
    public void addProduct(Product product, int quantity) {
        if (product == null || quantity < 1) {
            roops.util.Goals.reached(0);            
            throw new IllegalArgumentException(
                    "Product cannot be null and quantity has to be greater than 0.");
        }

        Integer existingQuantity = productQuantities.get(product);
        if (existingQuantity == null) {
            roops.util.Goals.reached(1);
            existingQuantity = 0;
        } else {
            roops.util.Goals.reached(2);
        }
        productQuantities.put(product, existingQuantity + quantity);
        totalWithoutCoupons += product.getPrice() * quantity;
    }

    /**
     * Removes the given quantity of the given product from this {@link Cart}.
     * 
     * @param product
     *            the {@link Product} to be removed from this {@link Cart}.
     *            Cannot be <code>null</code>.
     * 
     * @param quantity
     *            the quantity of the given {@link Product} to be removed from
     *            this {@link Cart}. Has to be greater than zero.
     * 
     * @return <code>true</code> if the given quantity was removed;
     *         <code>false</code> if it could not have been removed because
     *         the given quantity is greater than the quantity of the given
     *         product in this {@link Cart}. (A product that is not in the cart
     *         has effectively quantity 0.)
     */
    @roops.util.NrOfGoals(4)
    @roops.util.BenchmarkMethod
    public boolean removeProduct(Product product, int quantity) {
        if (product == null || quantity < 1) {
            roops.util.Goals.reached(0);
            throw new IllegalArgumentException(
                    "Product cannot be null and quantity has to be greater than 0.");
        }

        Integer existingQuantity = productQuantities.get(product);
        if (existingQuantity == null || existingQuantity < quantity) {
            roops.util.Goals.reached(1);
            return false;
        } else if (existingQuantity == quantity) {
            roops.util.Goals.reached(2);
            productQuantities.remove(product);
        } else {
            roops.util.Goals.reached(3);
            productQuantities.put(product, existingQuantity - quantity);
        }
        totalWithoutCoupons -= product.getPrice() * quantity;
        return true;
    }

    /**
     * Adds the given {@link Coupon} to this {@link Cart} if it is not already
     * in this {@link Cart}.
     * 
     * @param coupon
     *            the {@link Coupon} to be added to this {@link Cart}.
     * @return <code>true</code> if the given {@link Coupon} was added to this
     *         {@link Cart}, <code>false</code> otherwise.
     */
    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod
    public boolean addCoupon(Coupon coupon) {
        if (coupons.add(coupon)) {
            roops.util.Goals.reached(0);
            return true;
        } else {
            roops.util.Goals.reached(1);
            return false;
        }
    }

    /**
     * Removes the given {@link Coupon} from this {@link Cart}.
     * 
     * @param coupon
     *            the {@link Coupon} to be removed from this {@link Cart}.
     * @return <code>true</code> if the given {@link Coupon} was removed from
     *         this {@link Cart}, <code>false</code> otherwise.
     */
    @roops.util.NrOfGoals(2)
    @roops.util.BenchmarkMethod
    public boolean removeCoupon(Coupon coupon) {
        if (coupons.remove(coupon)) {
            roops.util.Goals.reached(0);
            return true;
        } else {
            roops.util.Goals.reached(1);
            return false;
        }
    }

    /**
     * Returns the {@link Set} of {@link Coupon} objects in this {@link Cart}.
     * 
     * @return the {@link Set} of {@link Coupon} objects in this {@link Cart}.
     */
    public Set<Coupon> getCoupons() {
        return new HashSet<Coupon>(coupons);
    }

    /**
     * Returns a {@link Map} of the {@link Product} objects and their
     * corresponding quantities in this {@link Cart}.
     * 
     * @return a {@link Map} of the {@link Product} objects and their
     *         corresponding quantities in this {@link Cart}.
     */
    public Map<Product, Integer> getProductQuantities() {
        return new HashMap<Product, Integer>(productQuantities);
    }

    /**
     * Returns a {@link Map} of the {@link Coupon} objects and their
     * corresponding discount values in this {@link Cart}.
     * 
     * @return a {@link Map} of the {@link Coupon} objects and their
     *         corresponding discount values in this {@link Cart}.
     */
    public HashMap<Coupon, Double> getCouponValues() {
        HashMap<Coupon, Double> couponValuesMap = new HashMap<Coupon, Double>();
        for (Coupon coupon : coupons) {
            couponValuesMap.put(coupon, coupon.getValue(this));
        }
        return couponValuesMap;
    }

    /**
     * Returns the total price for this {@link Cart}, which is the sum of the
     * prices of all the {@link Product} objects minus the discount values of
     * all the {@link Coupon} objects.
     * 
     * @return the total price for this {@link Cart}, which is the sum of the
     *         prices of all the {@link Product} objects minus the discount
     *         values of all the {@link Coupon} objects.
     */
    public double getTotalPrice() {
        Collection<Double> couponValues = getCouponValues().values();
        double totalWithCoupons = totalWithoutCoupons;
        for (Double couponValue : couponValues) {
            totalWithCoupons -= couponValue;
        }
        return totalWithCoupons;
    }

    /**
     * Returns a {@link String} that represents the bill for this {@link Cart}.
     * 
     * @return a {@link String} that represents the bill for this {@link Cart}.
     *         It contains brief descriptions of all the {@link Product} objects
     *         and {@link Coupon} objects in the cart, their prices/values and
     *         the total price of this {@link Cart}. It also includes coupons
     *         that have zero value in this cart to show that the discount does
     *         not apply.
     */
    public String getBill() {

        StringBuffer bill = new StringBuffer();
        NumberFormat currencyFormat = NumberFormat.getCurrencyInstance();

        for (Entry<Product, Integer> productQuantity : productQuantities
                .entrySet()) {
            Product product = productQuantity.getKey();
            Integer quantity = productQuantity.getValue();
            bill.append(product.getDescription());
            bill.append(COMMA_SPACE);
            bill.append(PRICE);
            bill.append(currencyFormat.format(product.getPrice()));
            bill.append(COMMA_SPACE);
            bill.append(QUANTITY);
            bill.append(quantity);
            bill.append(COMMA_SPACE);
            bill.append(SUB_TOTAL);
            bill.append(currencyFormat.format(product.getPrice() * quantity));
            bill.append(NEW_LINE);
        }

        HashMap<Coupon, Double> couponValues = getCouponValues();
        for (Entry<Coupon, Double> couponValue : couponValues.entrySet()) {
            Coupon coupon = couponValue.getKey();
            Double value = couponValue.getValue();
            bill.append(coupon.getDescription());
            bill.append(COMMA_SPACE);
            bill.append(SUB_TOTAL);
            bill.append(MINUS);
            bill.append(currencyFormat.format(value));
            bill.append(NEW_LINE);
        }

        bill.append(TOTAL);
        bill.append(currencyFormat.format(getTotalPrice()));

        return bill.toString();
    }

    /**
     * Inner {@link Comparator} class used to sort the {@link Product} objects
     * for the bill.
     */
    private class ProductBillComparator implements Comparator<Product> {

        @Override
        public int compare(Product o1, Product o2) {
            int priceComparison = ((Double) o1.getPrice()).compareTo(o2
                    .getPrice());
            if (priceComparison != 0) {
                return priceComparison;
            } else {
                return o1.getDescription().compareTo(o2.getDescription());
            }
        }

    }

}

/* end edu.uiuc.cart */
