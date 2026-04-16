using System.Data.Common;

namespace Web_E_Commerce.DTOs.Shared.Constants
{
    public static class MessageDescriptions
    {
        // ==============================
        // User Role Management
        // ==============================
        public const string ROLE_ASSIGN_SUCCESS = "The role has been assigned to the user successfully.";
        public const string ROLE_REMOVE_SUCCESS = "The role has been removed from the user successfully.";
        public const string USER_NOT_FOUND = "The user does not exist in the system.";
        public const string ROLE_NOT_FOUND = "The specified role does not exist.";
        public const string USER_ALREADY_HAS_ROLE = "User already has this role.";
        public const string USER_DOES_NOT_HAVE_ROLE = "User does not have this role.";
        public const string INVALID_ROLE = "The role provided is invalid.";
        public const string GET_ALL_ROLES_SUCCESS = "Get all roles successfully.";
        public const string GET_USER_ROLES_SUCCESS = "Get user roles successfully.";
        public const string GET_ALL_USERS_WITH_ROLES_SUCCESS = "Get all users with their roles successfully.";
        public const string INVALID_PAGINATION_PARAMETERS = "Page and PageSize must be greater than 0.";

        // ==============================
        // Product Related Messages
        // ==============================
        public const string GET_PRODUCTS_SUCCESS = "Get product successfully.";
        public const string GET_ALL_PRODUCTS_SUCCESS = "Get all products successfully.";
        public const string PRODUCT_NOT_FOUND = "The specified product does not exist.";
        public const string CREATE_PRODUCT_SUCCESS = "Product has been created successfully.";
        public const string UPDATE_PRODUCT_SUCCESS = "Product has been updated successfully.";
        public const string DELETE_PRODUCT_SUCCESS = "Product has been deleted successfully.";
        public const string DELETE_PRODUCT_FAILURE = "Failed to delete the product.";
        public const string PRODUCT_ALREADY_EXISTS = "A product with the same name already exists in this category.";
        public const string GET_SLUG_PRODUCT_SUCCESS = "Get product by slug successfully.";
        public const string GET_RELATED_PRODUCT_SUCCESS = "Get related products successfully.";
        public const string GET_PRODUCT_VIEW_SUCCESS = "Get product views successfully.";

        // ==============================
        // Category Related Messages
        // ==============================
        public const string CATEGORY_NOT_FOUND = "The specified category does not exist.";
        public const string GET_ALL_CATEGORIES_SUCCESS = "Get all categories successfully.";
        public const string GET_CATEGORY_DETAILS_SUCCESS = "Get category details successfully.";
        public const string CREATE_CATEGORY_SUCCESS = "Category has been created successfully.";
        public const string UPDATE_CATEGORY_SUCCESS = "Category has been updated successfully.";
        public const string CATEGORY_ALREADY_INACTIVE = "The category is already inactive.";
        public const string CATEGORY_ALREADY_ACTIVE = "The category is already active.";
        public const string DEACTIVATE_CATEGORY_SUCCESS = "Category has been deactivated successfully.";
        public const string ACTIVATE_CATEGORY_SUCCESS = "Category has been activated successfully.";
        public const string CATEGORY_ALREADY_EXISTS = "A category with the same name already exists.";

        // ==============================
        // Seller Request Related Messages
        // ==============================
        public const string SELLER_REQUEST_PENDING = "There is already a pending seller role request for this user.";
        public const string SELLER_REQUEST_SENT = "Seller role request has been sent and is pending approval.";

        // ===== Validation =====
        public const string VALIDATION_ERROR =
            "One or more validation errors occurred.";

        // ===== Bad Request =====
        public const string BAD_REQUEST_ERROR =
            "The request is invalid or cannot be processed.";

        // ===== Auth / Security =====
        public const string UNAUTHORIZED =
            "Authentication is required to access this resource.";

        public const string FORBIDDEN =
            "You do not have permission to access this resource.";

        // ===== Not Found =====
        public const string NOT_FOUND =
            "The requested resource could not be found.";

        // ===== Server =====
        public const string INTERNAL_SERVER_ERROR =
            "An unexpected error occurred on the server.";

        // ===== Seller Request Specific =====
        public const string INVALID_SELLER_REQUEST = "The seller request is invalid.";
        public const string SELLER_REQUEST_APPROVED = "The seller request has been approved successfully.";
        public const string SELLER_REQUEST_REJECTED = "The seller request has been rejected successfully.";
        public const string GET_PENDING_SELLER_REQUESTS_SUCCESS = "Pending seller requests retrieved successfully.";
        public const string GET_ALL_SELLER_REQUESTS_SUCCESS = "All seller requests retrieved successfully.";

        // ===== Auth =====
        public const string USER_REGISTRATION_SUCCESS = "User registration successful.";
        public const string USER_LOGIN_SUCCESS = "User login successful.";
        public const string LOGOUT_SUCCESS = "User logout successful.";

        // ===== Cart =====
        public const string CART_NOT_FOUND = "The specified cart does not exist.";
        public const string GET_CART_SUCCESS = "Cart retrieved successfully.";
        public const string ADD_TO_CART_SUCCESS = "Product added to cart successfully.";
        public const string UPDATE_CART_SUCCESS = "Cart updated successfully.";
        public const string REMOVE_CART_ITEM_SUCCESS = "Cart item removed successfully.";
        public const string CART_EMPTY = "The cart is empty.";

        // ===== Cart Item =====
        public const string CART_ITEM_NOT_FOUND = "The specified cart item does not exist.";

        // ===== Stock =====
        public const string NOT_ENOUGH_STOCK = "Not enough stock available for the requested quantity.";

        // ===== Order =====
        public const string CREATE_ORDER_SUCCESS = "Order has been created successfully.";
        public const string ORDER_NOT_FOUND = "The specified order does not exist.";
        public const string GET_ORDER_SUCCESS = "Order retrieved successfully.";
        public const string GET_ORDERS_SUCCESS = "Orders retrieved successfully.";
        public const string ORDER_ALREADY_FINALIZED = "Order has already been finalized.";
        public const string UPDATE_ORDER_STATUS_SUCCESS = "Order status updated successfully.";

        // ===== Payment =====
        public const string PAYMENT_NOT_FOUND = "The specified payment does not exist.";

        // ===== Review =====
        public const string GET_REVIEWS_SUCCESS = "Reviews retrieved successfully.";
        public const string CREATE_REVIEW_SUCCESS = "Review submitted successfully.";
        public const string UPDATE_REVIEW_SUCCESS = "Review updated successfully.";
        public const string DELETE_REVIEW_SUCCESS = "Review deleted successfully.";
        public const string REVIEW_NOT_FOUND = "The specified review does not exist.";
        public const string REVIEW_ALREADY_EXISTS = "You have already reviewed this product.";
        public const string REVIEW_PURCHASE_REQUIRED = "You can only review products you have purchased and received.";
        public const string INVALID_REVIEW_RATING = "Rating must be between 1 and 5.";

        // ===== User Profile =====
        public const string GET_PROFILE_SUCCESS = "Profile retrieved successfully.";
        public const string UPDATE_PROFILE_SUCCESS = "Profile updated successfully.";
        public const string CHANGE_PASSWORD_SUCCESS = "Password changed successfully.";
        public const string EMAIL_ALREADY_TAKEN = "This email is already in use by another account.";
        public const string PASSWORD_CONFIRM_MISMATCH = "New password and confirmation password do not match.";
        public const string PASSWORD_TOO_SHORT = "Password must be at least 6 characters.";
        public const string CURRENT_PASSWORD_INCORRECT = "Current password is incorrect.";
        public const string PASSWORD_SAME_AS_OLD = "New password must be different from the current password.";
    }
}
