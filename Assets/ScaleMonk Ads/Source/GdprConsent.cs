namespace Assets.ScaleMonk_Ads
{
    /// <summary>
    /// Represents the different GDPR consent status handled in the Scalemonk SDK.
    /// </summary>
    public enum GdprConsent
    {
        /// <summary>
        /// The user granted it's GDPR consent.
        /// </summary>
        Granted,
        
        /// <summary>
        /// The user did not grant it's GDPR consent.
        /// </summary>
        NotGranted,
        
        /// <summary>
        /// No consent change has been done.
        /// </summary>
        NotSet,
        
        /// <summary>
        /// For the current application domain GDPR consent is not an applicable regulation.
        /// </summary>
        NotApplicable
    }
}