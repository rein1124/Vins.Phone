using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class Rectanle2RegionExtractor : RegionExtractor
    {
        protected override HRegion GetRegion(HImage image)
        {
            var rect = new HRegion();
            rect.GenRectangle2(HalfHeight, HalfWidth, Angle, HalfWidth, HalfHeight);
            return rect;
        }
    }
}