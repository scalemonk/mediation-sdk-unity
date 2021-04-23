using System;
using System.Collections.Generic;

namespace ScaleMonk.Ads
{
    public enum BannerPosition
    {
        TopLeft,
        TopCenter,
        TopRight,
        Centered,
        CenterLeft,
        CenterRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }

    public static class BannerPositionExtension
    {
        private static readonly Dictionary<BannerPosition, string> Positions = new Dictionary<BannerPosition, string>()
        {
            {BannerPosition.TopCenter, "top_center"},
            {BannerPosition.TopLeft, "top_left"},
            {BannerPosition.TopRight, "top_right"},
            {BannerPosition.Centered, "centered"},
            {BannerPosition.CenterLeft, "center_left"},
            {BannerPosition.CenterRight, "center_right"},
            {BannerPosition.BottomCenter, "bottom_center"},
            {BannerPosition.BottomLeft, "bottom_left"},
            {BannerPosition.BottomRight, "bottom_right"}
        };
        
        public static string ToSnakeCaseString(this BannerPosition position)
        {
            return Positions[position];
        }
    }
}