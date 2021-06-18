namespace Assets.ScaleMonk_Ads
{
    /// <summary>
    /// Represents the different GDPR consent status handled in the Scalemonk SDK.
    /// </summary>
    public enum GDPRConsent
    {
        /// <summary>
        /// The user granted it's GDPR consent.
        /// </summary>
        GRANTED,
        
        /// <summary>
        /// The user did not grant it's GDPR consent.
        /// </summary>
        NOT_GRANTED,
        
        /// <summary>
        /// No consent change has been done.
        /// </summary>
        UNKNOWN,
        
        /// <summary>
        /// For the current application domain GDPR consent is not an applicable regulation.
        /// </summary>
        NOT_APPLICABLE
    }
}