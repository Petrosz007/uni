package shop;

public class Customer {
    private final String name;
    private int balance;
    
    public Customer(String name) {
        this.name = name;
        this.balance = 10000;
    }
    
    public String getName() {
        return name;
    }
    
    public int getBalance() {
        return balance;
    }
    
    public void buySomething(int price) {
        if(price > 0 && (balance-price) >= 0 ) {
            balance -= price;
        }
    }
    
    @Override
    public int hashCode() {
        return name.hashCode() + balance;
    }
    
    @Override
    public boolean equals(Object other) {
        if(this == other) {
            return true;
        }
        if(other instanceof Customer) {
            Customer that = (Customer)other;
            return this.name.equalsIgnoreCase(that.name);
        }
        else {
            return false;
        }
    }
    
    @Override
    public String toString() {
        return name + "{ $" + balance + "}";
    }
}