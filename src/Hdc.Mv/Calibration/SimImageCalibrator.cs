﻿namespace Hdc.Mv.Inspection
{
    public class SimImageCalibrator : IImageCalibrator
    {
        public void Init(string cammerParamsFileName, string cameraPoseFileName, string calibImageFileName, string calibImageDirName)
        {
        }

        public ImageInfo CalibrateImage(ImageInfo originalImageInfo)
        {
            return originalImageInfo;
        }
    }
}