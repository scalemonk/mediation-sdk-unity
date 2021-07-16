
namespace ScaleMonk.Ads
{

  /// <summary>
  /// Represents the different COPPA status handled in the Scalemonk SDK.
  /// </summary>
  public enum CoppaStatus {
    
    /// <summary>
    /// Unknown status.
    /// </summary>
    Unknown,

    /// <summary>
    /// treat as child status.
    /// </summary>
    ChildTreatmentFalse,

    /// <summary>
    /// Dont treat as child status.
    /// </summary>
    ChildTreatmentTrue,

    /// <summary>
    /// Does not apply status.
    /// </summary>
    NotApplicable
  }

  public static class CoppaStatusExtensions
  {
    public static int ToInt(this CoppaStatus status)
    {
      switch (status)
      {
        case CoppaStatus.Unknown:
          return 0;
        case CoppaStatus.ChildTreatmentFalse:
          return 1;
        case CoppaStatus.ChildTreatmentTrue:
          return 2;
        case CoppaStatus.NotApplicable:
          return 3;
        default:
          return -1;
      }
    }
  }
}
