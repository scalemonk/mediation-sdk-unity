namespace ScaleMonk.Ads.Android
{
    public interface IBridge
    {
        void CallNativeMethodWithActivity(string methodName, params object[] args);
        void CallNativeMethod(string methodName, params object[] args);
        bool CallBooleanNativeMethod(string methodName, params object[] args);
    }
}
