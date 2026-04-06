namespace Web_E_Commerce.DTOs.Shared.Constants
{
    public static class MessageKeys
    {
        // ==============================
        // User Role Management Keys
        // ==============================
        public const string ROLE_ASSIGN_SUCCESS = "ROLE_ASSIGN_SUCCESS";
        public const string ROLE_REMOVE_SUCCESS = "ROLE_REMOVE_SUCCESS";
        public const string USER_NOT_FOUND = "USER_NOT_FOUND";
        public const string ROLE_NOT_FOUND = "ROLE_NOT_FOUND";
        public const string USER_ALREADY_HAS_ROLE = "USER_ALREADY_HAS_ROLE";
        public const string USER_DOES_NOT_HAVE_ROLE = "USER_DOES_NOT_HAVE_ROLE";
        public const string INVALID_ROLE = "INVALID_ROLE";
        public const string GET_ALL_ROLES_SUCCESS = "GET_ALL_ROLES_SUCCESS";
        public const string GET_USER_ROLES_SUCCESS = "GET_USER_ROLES_SUCCESS";
        public const string GET_ALL_USERS_WITH_ROLES_SUCCESS = "GET_ALL_USERS_WITH_ROLES_SUCCESS";
        public const string INVALID_PAGINATION_PARAMETERS = "INVALID_PAGINATION_PARAMETERS";

        // ==============================
        // Product Related Keys
        // ==============================
        public const string GET_PRODUCTS_SUCCESS = "GET_PRODUCTS_SUCCESS";
        public const string GET_ALL_PRODUCTS_SUCCESS = "GET_ALL_PRODUCTS_SUCCESS";
        public const string CREATE_PRODUCT_SUCCESS = "CREATE_PRODUCT_SUCCESS";
        public const string UPDATE_PRODUCT_SUCCESS = "UPDATE_PRODUCT_SUCCESS";
        public const string DELETE_PRODUCT_SUCCESS = "DELETE_PRODUCT_SUCCESS";
        public const string DELETE_PRODUCT_FAILURE = "DELETE_PRODUCT_FAILURE";
        public const string PRODUCT_NOT_FOUND = "PRODUCT_NOT_FOUND";
        public const string PRODUCT_ALREADY_EXISTS = "PRODUCT_ALREADY_EXISTS";
        public const string GET_SLUG_PRODUCT_SUCCESS = "GET_SLUG_PRODUCT_SUCCESS";
        public const string GET_RELATED_PRODUCT_SUCCESS = "GET_RELATED_PRODUCT_SUCCESS";
        public const string GET_PRODUCT_VIEW_SUCCESS = "GET_PRODUCT_VIEW_SUCCESS";

        // ==============================
        // Category Related Keys
        // ==============================
        public const string CATEGORY_NOT_FOUND = "CATEGORY_NOT_FOUND";
        public const string GET_ALL_CATEGORIES_SUCCESS = "GET_ALL_CATEGORIES_SUCCESS";
        public const string GET_CATEGORY_DETAILS_SUCCESS = "GET_CATEGORY_DETAILS_SUCCESS";
        public const string CREATE_CATEGORY_SUCCESS = "CREATE_CATEGORY_SUCCESS";
        public const string UPDATE_CATEGORY_SUCCESS = "UPDATE_CATEGORY_SUCCESS";
        public const string DELETE_CATEGORY_SUCCESS = "DELETE_CATEGORY_SUCCESS";
        public const string CATEGORY_ALREADY_INACTIVE = "CATEGORY_ALREADY_INACTIVE";
        public const string CATEGORY_ALREADY_ACTIVE = "CATEGORY_ALREADY_ACTIVE";
        public const string DEACTIVATE_CATEGORY_SUCCESS = "DEACTIVATE_CATEGORY_SUCCESS";
        public const string ACTIVATE_CATEGORY_SUCCESS = "ACTIVATE_CATEGORY_SUCCESS";
        public const string CATEGORY_ALREADY_EXISTS = "CATEGORY_ALREADY_EXISTS";


        // ==============================
        // Seller Request Related Keys
        // ==============================
        public const string SELLER_REQUEST_PENDING = "SELLER_REQUEST_PENDING";
        public const string SELLER_REQUEST_SENT = "SELLER_REQUEST_SENT";

        // ===== Validation =====
        public const string VALIDATION_ERROR = "VALIDATION_ERROR";

        // ===== Bad Request =====
        public const string BAD_REQUEST_ERROR = "BAD_REQUEST_ERROR";

        // ===== Auth / Security =====
        public const string UNAUTHORIZED = "UNAUTHORIZED";
        public const string FORBIDDEN = "FORBIDDEN";

        // ===== Not Found =====
        public const string NOT_FOUND = "NOT_FOUND";

        // ===== Server =====
        public const string INTERNAL_SERVER_ERROR = "INTERNAL_SERVER_ERROR";

        // ===== Seller Request Management =====
        public const string INVALID_SELLER_REQUEST = "INVALID_SELLER_REQUEST";
        public const string SELLER_REQUEST_APPROVED = "SELLER_REQUEST_APPROVED";
        public const string SELLER_REQUEST_REJECTED = "SELLER_REQUEST_REJECTED";
        public const string GET_PENDING_SELLER_REQUESTS_SUCCESS = "GET_PENDING_SELLER_REQUESTS_SUCCESS";
        public const string GET_ALL_SELLER_REQUESTS_SUCCESS = "GET_ALL_SELLER_REQUESTS_SUCCESS";

        // ===== Auth =====
        public const string REGISTER_SUCCESS = "REGISTER_SUCCESS";
        public const string LOGIN_SUCCESS = "LOGIN_SUCCESS";

        // ===== Cart =====
        public const string CART_NOT_FOUND = "CART_NOT_FOUND";
        public const string GET_CART_SUCCESS = "GET_CART_SUCCESS";
        public const string ADD_TO_CART_SUCCESS = "ADD_TO_CART_SUCCESS";
        public const string UPDATE_CART_SUCCESS = "UPDATE_CART_SUCCESS";
        public const string REMOVE_CART_ITEM_SUCCESS = "REMOVE_CART_ITEM_SUCCESS";
        public const string CART_EMPTY = "CART_EMPTY";

        // ===== Cart Item =====
        public const string CART_ITEM_NOT_FOUND = "CART_ITEM_NOT_FOUND";

        // ===== Stock =====
        public const string NOT_ENOUGH_STOCK = "NOT_ENOUGH_STOCK";

        // ===== Order =====
        public const string ORDER_NOT_FOUND = "ORDER_NOT_FOUND";
        public const string GET_ORDER_SUCCESS = "GET_ORDER_SUCCESS";
        public const string GET_ORDERS_SUCCESS = "GET_ORDERS_SUCCESS";
        public const string CREATE_ORDER_SUCCESS = "CREATE_ORDER_SUCCESS";
        public const string ORDER_ALREADY_FINALIZED = "ORDER_ALREADY_FINALIZED";
        public const string INVALID_ORDER_STATUS_TRANSITION = "INVALID_ORDER_STATUS_TRANSITION";
        public const string UPDATE_ORDER_STATUS_SUCCESS = "UPDATE_ORDER_STATUS_SUCCESS";

        // ===== Payment =====
        public const string PAYMENT_NOT_FOUND = "PAYMENT_NOT_FOUND";
    }
}