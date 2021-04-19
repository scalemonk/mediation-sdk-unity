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
        public static string ToSnakeCaseString(this BannerPosition position)
        {
            if (position == BannerPosition.TopLeft)
            {
                return "top_left";
            }

            if (position == BannerPosition.TopCenter)
            {
                return "top_center";
            }

            if (position == BannerPosition.TopRight)
            {
                return "top_right";
            }

            if (position == BannerPosition.Centered)
            {
                return "centered";
            }

            if (position == BannerPosition.CenterLeft)
            {
                return "center_left";
            }

            if (position == BannerPosition.CenterRight)
            {
                return "center_right";
            }

            if (position == BannerPosition.BottomLeft)
            {
                return "bottom_left";
            }

            if (position == BannerPosition.BottomCenter)
            {
                return "bottom_center";
            }
            
            // position == BannerPosition.BottomRight
            return "bottom_right";
        }
    }
}