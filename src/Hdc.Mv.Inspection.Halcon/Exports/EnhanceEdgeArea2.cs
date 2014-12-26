//
//  File generated by HDevelop for HALCON/DOTNET (C#) Version 11.0
//

using HalconDotNet;

public partial class HDevelopExport
{
#if !(NO_EXPORT_MAIN || NO_EXPORT_APP_MAIN)
#endif

  // Procedures 
  public void EnhanceEdgeArea2 (HObject ho_InputImage, out HObject ho_EnhancedImage, 
      HTuple hv_LineStartPoint_Row, HTuple hv_LineStartPoint_Column, HTuple hv_LineEndPoint_Row, 
      HTuple hv_LineEndPoint_Column, HTuple hv_RoiWidthLen, HTuple hv_EmpMaskWidth, 
      HTuple hv_EmpMaskHeight, HTuple hv_EmpMaskFactor, HTuple hv_MeanMaskWidth, HTuple hv_MeanMaskHeight, 
      HTuple hv_MinThresh, HTuple hv_MaxThresh)
  {



    // Local iconic variables 

    HObject ho_Rectangle, ho_RegionDilation, ho_MeanImage;


    // Local control variables 

    HTuple hv_TmpCtrl_Row = null, hv_TmpCtrl_Column = null;
    HTuple hv_TmpCtrl_Dr = null, hv_TmpCtrl_Dc = null, hv_TmpCtrl_Phi = null;
    HTuple hv_TmpCtrl_Len1 = null, hv_TmpCtrl_Len2 = null;

    // Initialize local and output iconic variables 
    HOperatorSet.GenEmptyObj(out ho_EnhancedImage);
    HOperatorSet.GenEmptyObj(out ho_Rectangle);
    HOperatorSet.GenEmptyObj(out ho_RegionDilation);
    HOperatorSet.GenEmptyObj(out ho_MeanImage);

    //
    //init
    //FilterAlpha := 6
    //Measure 01: Convert coordinates to rectangle2 type
    hv_TmpCtrl_Row = 0.5*(hv_LineStartPoint_Row+hv_LineEndPoint_Row);
    hv_TmpCtrl_Column = 0.5*(hv_LineStartPoint_Column+hv_LineEndPoint_Column);
    hv_TmpCtrl_Dr = hv_LineStartPoint_Row-hv_LineEndPoint_Row;
    hv_TmpCtrl_Dc = hv_LineEndPoint_Column-hv_LineStartPoint_Column;
    hv_TmpCtrl_Phi = hv_TmpCtrl_Dr.TupleAtan2(hv_TmpCtrl_Dc);
    hv_TmpCtrl_Len1 = 0.5*((((hv_TmpCtrl_Dr*hv_TmpCtrl_Dr)+(hv_TmpCtrl_Dc*hv_TmpCtrl_Dc))).TupleSqrt()
        );
    hv_TmpCtrl_Len2 = hv_RoiWidthLen.Clone();
    ho_Rectangle.Dispose();
    HOperatorSet.GenRectangle2(out ho_Rectangle, hv_TmpCtrl_Row, hv_TmpCtrl_Column, 
        hv_TmpCtrl_Phi, hv_TmpCtrl_Len1, hv_TmpCtrl_Len2);

    ho_RegionDilation.Dispose();
    HOperatorSet.DilationRectangle1(ho_Rectangle, out ho_RegionDilation, 10, 10);

    ho_EnhancedImage.Dispose();
    HOperatorSet.ReduceDomain(ho_InputImage, ho_RegionDilation, out ho_EnhancedImage
        );

    ho_MeanImage.Dispose();
    HOperatorSet.MeanSp(ho_EnhancedImage, out ho_MeanImage, hv_MeanMaskWidth, hv_MeanMaskHeight, 
        hv_MinThresh, hv_MaxThresh);
    ho_EnhancedImage.Dispose();
    HOperatorSet.Emphasize(ho_MeanImage, out ho_EnhancedImage, hv_EmpMaskWidth, hv_EmpMaskHeight, 
        hv_EmpMaskFactor);


    ho_Rectangle.Dispose();
    ho_RegionDilation.Dispose();
    ho_MeanImage.Dispose();

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

