package shop;

public class ShopTransactionException extends Exception {
    ShopTransactionException(String message) {
        super(message);
    }
    
    ShopTransactionException() {
    }
}
