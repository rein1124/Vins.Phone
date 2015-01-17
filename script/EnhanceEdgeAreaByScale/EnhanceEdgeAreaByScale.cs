//
//  File generated by HDevelop for HALCON/DOTNET (C#) Version 12.0
//

using HalconDotNet;

public partial class HDevelopExport
{
#if !(NO_EXPORT_MAIN || NO_EXPORT_APP_MAIN)
#endif

  // Procedures 
  public void EnhanceEdgeAreaByScale (HObject ho_InputImage, out HObject ho_EnhancedImage, 
      out HObject ho_EnhancedEdge, HTuple hv_MeanMaskWidth, HTuple hv_MeanMaskHeight, 
      HTuple hv_EdgeAreaLightDark, HTuple hv_ScaleAdd, HTuple hv_OpeningWidth, HTuple hv_OpeningHeight, 
      HTuple hv_ClosingWidth, HTuple hv_ClosingHeight)
  {




    // Local iconic variables 

    HObject ho_Domain, ho_ImageScaleMax3, ho_ImageMean;
    HObject ho_Region, ho_ImageReduced, ho_ImageScaled=null;
    HObject ho_Region1, ho_RegionOpening, ho_RegionFillUp;

    // Local control variables 

    HTuple hv_Width = null, hv_Height = null, hv_UsedThreshold = null;
    HTuple hv_Value = null, hv_UsedThreshold1 = null;
    // Initialize local and output iconic variables 
    HOperatorSet.GenEmptyObj(out ho_EnhancedImage);
    HOperatorSet.GenEmptyObj(out ho_EnhancedEdge);
    HOperatorSet.GenEmptyObj(out ho_Domain);
    HOperatorSet.GenEmptyObj(out ho_ImageScaleMax3);
    HOperatorSet.GenEmptyObj(out ho_ImageMean);
    HOperatorSet.GenEmptyObj(out ho_Region);
    HOperatorSet.GenEmptyObj(out ho_ImageReduced);
    HOperatorSet.GenEmptyObj(out ho_ImageScaled);
    HOperatorSet.GenEmptyObj(out ho_Region1);
    HOperatorSet.GenEmptyObj(out ho_RegionOpening);
    HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
    ho_Domain.Dispose();
    HOperatorSet.GetDomain(ho_InputImage, out ho_Domain);
    HOperatorSet.RegionFeatures(ho_Domain, "width", out hv_Width);
    HOperatorSet.RegionFeatures(ho_Domain, "height", out hv_Height);

    ho_ImageScaleMax3.Dispose();
    HOperatorSet.ScaleImageMax(ho_InputImage, out ho_ImageScaleMax3);
    ho_ImageMean.Dispose();
    HOperatorSet.MeanImage(ho_ImageScaleMax3, out ho_ImageMean, hv_MeanMaskWidth, 
        hv_MeanMaskHeight);

    ho_Region.Dispose();
    HOperatorSet.BinaryThreshold(ho_ImageMean, out ho_Region, "max_separability", 
        hv_EdgeAreaLightDark, out hv_UsedThreshold);
    ho_ImageReduced.Dispose();
    HOperatorSet.ReduceDomain(ho_ImageMean, ho_Region, out ho_ImageReduced);
    HOperatorSet.GrayFeatures(ho_Region, ho_ImageReduced, "mean", out hv_Value);

    if ((int)(new HTuple(hv_EdgeAreaLightDark.TupleEqual("dark"))) != 0)
    {
      ho_ImageScaled.Dispose();
      HOperatorSet.ScaleImage(ho_ImageReduced, out ho_ImageScaled, 1, (-hv_Value)+hv_ScaleAdd);
    }
    else if ((int)(new HTuple(hv_EdgeAreaLightDark.TupleEqual("light"))) != 0)
    {
      ho_ImageScaled.Dispose();
      HOperatorSet.ScaleImage(ho_ImageReduced, out ho_ImageScaled, 1, (255-hv_Value)+hv_ScaleAdd);
    }

    ho_Region1.Dispose();
    HOperatorSet.BinaryThreshold(ho_ImageScaled, out ho_Region1, "max_separability", 
        hv_EdgeAreaLightDark, out hv_UsedThreshold1);
    ho_RegionOpening.Dispose();
    HOperatorSet.OpeningRectangle1(ho_Region1, out ho_RegionOpening, hv_OpeningWidth, 
        hv_MeanMaskHeight);
    ho_EnhancedEdge.Dispose();
    HOperatorSet.ClosingRectangle1(ho_RegionOpening, out ho_EnhancedEdge, hv_ClosingWidth, 
        hv_ClosingHeight);
    ho_RegionFillUp.Dispose();
    HOperatorSet.FillUp(ho_EnhancedEdge, out ho_RegionFillUp);

    ho_EnhancedImage.Dispose();
    HOperatorSet.RegionToBin(ho_RegionFillUp, out ho_EnhancedImage, 255, 0, hv_Width, 
        hv_Height);

    ho_Domain.Dispose();
    ho_ImageScaleMax3.Dispose();
    ho_ImageMean.Dispose();
    ho_Region.Dispose();
    ho_ImageReduced.Dispose();
    ho_ImageScaled.Dispose();
    ho_Region1.Dispose();
    ho_RegionOpening.Dispose();
    ho_RegionFillUp.Dispose();

    return;
  }


}
#if !(NO_EXPORT_MAIN || NO_EXPORT_APP_MAIN)
public class HDevelopExportApp
{
  static void Main(string[] args)
  {
    new HDevelopExport();
  }
}
#endif

