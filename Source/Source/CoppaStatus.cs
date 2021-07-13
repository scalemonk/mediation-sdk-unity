
namespace ScaleMonk.Ads
{

  /// <summary>
  /// Represents the different COPPA status handled in the Scalemonk SDK.
  /// </summary>
  public enum CoppaStatus {
    
    /// <summary>
    /// Unknown status.
    /// </summary>
    UNKNOWN,

    /// <summary>
    /// treat as child status.
    /// </summary>
    CHILD_TREATMENT_FALSE,

    /// <summary>
    /// Dont treat as child status.
    /// </summary>
    CHILD_TREATMENT_TRUE,

    /// <summary>
    /// Does not apply status.
    /// </summary>
    NOT_APPLICABLE
  }

  public static class CoppaStatusExtensions
  {
    public static int ToInt(this CoppaStatus status)
    {
      switch (status)
      {
        case CoppaStatus.UNKNOWN:
          return 0;
        case CoppaStatus.CHILD_TREATMENT_FALSE:
          return 1;
        case CoppaStatus.CHILD_TREATMENT_TRUE:
          return 2;
        case CoppaStatus.NOT_APPLICABLE:
          return 3;
        default:
          return -1;
      }
    }
  }
}
