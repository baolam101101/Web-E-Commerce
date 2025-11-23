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

        // ==============================
        // Category Related Messages
        // ==============================
        public const string CATEGORY_NOT_FOUND = "The specified category does not exist.";
        public const string GET_ALL_CATEGORIES_SUCCESS = "Get all categories successfully.";
        public const string GET_CATEGORY_DETAILS_SUCCESS = "Get category details successfully.";
        public const string CREATE_CATEGORY_SUCCESS = "Category has been created successfully.";
        public const string UPDATE_CATEGORY_SUCCESS = "Category has been updated successfully.";
        public const string DELETE_CATEGORY_SUCCESS = "Category has been deleted successfully.";

        // ==============================
        // Seller Request Related Messages
        // ==============================
        public const string SELLER_REQUEST_PENDING = "There is already a pending seller role request for this user.";
        public const string SELLER_REQUEST_SENT = "Seller role request has been sent and is pending approval.";
    }
}
