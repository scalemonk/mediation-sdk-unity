namespace ScaleMonk.Ads
{
    public enum UserType
    {
        NON_PAYING_USER,
        PAYING_USER
    }

    public static class UserTypeExtensions
    {
        public static string ToStringUserType(this UserType usertype)
        {
            return usertype == UserType.PAYING_USER ? "paying_user" : "non_paying_user";
        }
    }
}