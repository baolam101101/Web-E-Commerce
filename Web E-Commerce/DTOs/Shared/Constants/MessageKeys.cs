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

        // ==============================
        // Category Related Keys
        // ==============================
        public const string CATEGORY_NOT_FOUND = "CATEGORY_NOT_FOUND";
        public const string GET_ALL_CATEGORIES_SUCCESS = "GET_ALL_CATEGORIES_SUCCESS";
        public const string GET_CATEGORY_DETAILS_SUCCESS = "GET_CATEGORY_DETAILS_SUCCESS";
        public const string CREATE_CATEGORY_SUCCESS = "CREATE_CATEGORY_SUCCESS";
        public const string UPDATE_CATEGORY_SUCCESS = "UPDATE_CATEGORY_SUCCESS";
        public const string DELETE_CATEGORY_SUCCESS = "DELETE_CATEGORY_SUCCESS";

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
    }
}