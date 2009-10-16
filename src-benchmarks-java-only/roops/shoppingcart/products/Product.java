package roops.shoppingcart.products;

@roops.util.BenchmarkClass
public class Product {

    private String description;
    private double price;

    /**
     * Creates a new {@link Product} with the given description and price.
     * 
     * @param description
     *            the description for this product
     * @param price
     *            the price for this product. Should be positive.
     */
    @roops.util.NrOfGoals(2)
    public Product(String description, double price) {
        if (price <= 0) {
            roops.util.Goals.reached(0);
            throw new IllegalArgumentException("Price should be positive.");
        }
        roops.util.Goals.reached(1);
        this.price = price;
        this.description = description;
    }

    /**
     * Returns the price of this {@link Product}.
     * 
     * @return the price of this {@link Product}.
     */
    public double getPrice() {
        return price;
    }

    /**
     * Returns the description of this {@link Product}.
     * 
     * @return the description of this {@link Product}.
     */
    public String getDescription() {
        return description;
    }

    public int hashCode() {
        final int prime = 31;
        int result = 1;
        result = prime * result
                + ((description == null) ? 0 : description.hashCode());
        long temp;
        temp = Double.doubleToLongBits(price);
        result = prime * result + (int) (temp ^ (temp >>> 32));
        return result;
    }

    @roops.util.NrOfGoals(8)
    @roops.util.BenchmarkMethod
    public boolean equals(Object obj) {
        if (this == obj) {
            roops.util.Goals.reached(0);
            return true;
        }
        if (obj == null) {
            roops.util.Goals.reached(1);
            return false;
        }
        if (getClass() != obj.getClass()) {
            roops.util.Goals.reached(2);
            return false;
        }
        Product other = (Product) obj;
        if (description == null) {
            if (other.description != null) {
                roops.util.Goals.reached(3);
                return false;
            } else {
                roops.util.Goals.reached(4);
            }
        } else if (!description.equals(other.description)) {
            roops.util.Goals.reached(5);
            return false;
        }
        if (Double.doubleToLongBits(price) != Double
                .doubleToLongBits(other.price)) {
            roops.util.Goals.reached(6);
            return false;
        }
        roops.util.Goals.reached(7);
        return true;
    }

}

/* end edu.uiuc.products */
