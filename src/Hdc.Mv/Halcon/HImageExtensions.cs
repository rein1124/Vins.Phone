﻿using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Imaging;
using HalconDotNet;
using Hdc.Windows;
using Hdc.Windows.Media.Imaging;

namespace Hdc.Mv.Halcon
{
    public static class HImageExtensions
    {
        public static int GetWidth(this HImage image)
        {
            int w, h;
            image.GetImageSize(out w, out h);
            return w;
        }

        public static int GetHeight(this HImage image)
        {
            int w, h;
            image.GetImageSize(out w, out h);
            return h;
        }

        public static Int32Size GetSize(this HImage image)
        {
            int w, h;
            image.GetImageSize(out w, out h);
            return new Int32Size(w, h);
        }

        public static HImage ReduceDomainForRing(this HImage hImage, double centerX, double centerY, double innerRadius,
                                          double outerRadius)
        {
            var innerCircle = new HRegion();
            innerCircle.GenCircle(centerY, centerX, innerRadius);

            var outerCircle = new HRegion();
            outerCircle.GenCircle(centerY, centerX, outerRadius);

            var ring = outerCircle.Difference(innerCircle);
            var reducedImage = hImage.ChangeDomain(ring);

            innerCircle.Dispose();
            outerCircle.Dispose();
            ring.Dispose();

//            reducedImage.CropDomain()
//                      .ToBitmapSource()
//                      .SaveToJpeg("_EnhanceEdgeArea_Domain.jpg");

            return reducedImage;
        }

        public static HImage PaintGrayOffset(this HImage imageSource, HImage imageDestination,
                                    int offsetRow, int offsetColumn)
        {
            HObject image;
            HDevelopExport.Singletone.PaintGrayOffset(imageSource, imageDestination, out image, offsetRow, offsetColumn);
            return new HImage(image);
        }

        public static HImage ToHImage(this BitmapSource bitmapSource)
        {
            var stride = bitmapSource.PixelWidth;

            int bufferSize = stride * bitmapSource.PixelHeight;
            IntPtr bufferPtr = Marshal.AllocHGlobal(bufferSize);

            bitmapSource.CopyPixels(Int32Rect.Empty, bufferPtr, bufferSize, bitmapSource.PixelWidth);


            var hImage = new HImage("byte", bitmapSource.PixelWidth, bitmapSource.PixelHeight, bufferPtr);

            Marshal.FreeHGlobal(bufferPtr);

            return hImage;
        }

        [Obsolete]
        public static HImage AddImagesWithFullDomain(this HImage image1, HImage image2)
        {
            var imageFull1 = image1.FullDomain();
            var imageFull2 = image2.FullDomain();
            return imageFull1.AddImage(imageFull2, new HTuple(1), new HTuple(0));
        }

        public static void SaveTiffWithPaintRegion(this HObject imageHObject, HObject regionHObject, double foreground,
                                                   double background, string fileName)
        {
            SaveTiffWithPaintRegion(new HImage(imageHObject), new HRegion(regionHObject), foreground, background, fileName);
        }

        public static void SaveTiffWithPaintRegion(this HImage image, HRegion region, double foreground, double background, string fileName)
        {
            var imagePainted = image.PaintRegion(region, foreground, "fill");
            imagePainted.WriteImage("tiff", background, fileName);
            imagePainted.Dispose();
        }

        public static void SaveTiff(this HObject imageHObject, double background, string fileName)
        {
            var image = new HImage(imageHObject);
            image.WriteImage("tiff", background, fileName);
        }

        public static void SaveTiff(this HImage image, double background, string fileName)
        {
            image.WriteImage("tiff", background, fileName);
        }
    }
}