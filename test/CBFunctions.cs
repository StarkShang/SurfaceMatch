using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;
using NXOpen;
using NXOpen.Utilities;
using NXOpen.UF;

namespace test
{
    class CBFunctions
    {
        public static UFSession theUFSession { get; set; }
        public static ListingWindow lw { get; set; }
        public static void customizedCBFunction()
        {
            // Step 1选择两个曲面
            // define mask
            var mask = new UFUi.Mask[1];
            mask[0].object_type = UFTypes.FaceType.UF_face_type;
            mask[0].object_subtype = 0;
            mask[0].solid_type = 0;
            // define selectionOption
            var option = new UFUi.SelectionOption();
            option.other_options = 0;
            option.reserved = new IntPtr();
            option.num_mask_triples = 1;
            option.mask_triples = mask;
            option.scope = 3;
            // define other parameters
            int response;
            double[] pos = new double[3];
            Tag dSurf, rSurf;
            Tag view = new Tag();
            // Step 1.1 : 选择设计曲面
            do
            {
                theUFSession.Ui.LockUgAccess(UFConstants.UF_UI_FROM_CUSTOM);
                theUFSession.Ui.SelectSingle("Please Select the design surface", ref option, out response, out dSurf, pos, out view);
                theUFSession.Ui.LockUgAccess(0);
            } while (5 != response);
            theUFSession.Disp.SetHighlight(dSurf, 1);
            // Step 1.2 : 选择实际曲面
            do
            {
                theUFSession.Ui.LockUgAccess(UFConstants.UF_UI_FROM_CUSTOM);
                theUFSession.Ui.SelectSingle("Please Select the real surface", ref option, out response, out rSurf, pos, out view);
                theUFSession.Ui.LockUgAccess(0);
            } while (5 != response);
            theUFSession.Disp.SetHighlight(rSurf, 1);
            // Step 2 : 获取曲面参数域
            IntPtr evaluator;
            double[] dUVMinMax = new double[4];
            double[] rUVMinMax = new double[4];
            theUFSession.Evalsf.Initialize(dSurf,out evaluator);
            theUFSession.Evalsf.AskFaceUvMinmax(evaluator, dUVMinMax);
            theUFSession.Evalsf.Initialize(rSurf, out evaluator);
            theUFSession.Evalsf.AskFaceUvMinmax(evaluator, rUVMinMax);
            // Step 3 : 获得初始采样点集
            const int uDNum = 501, vDNum = 501;
            var dUVDomain = new UVDomain(dUVMinMax);
            var rUVDomain = new UVDomain(rUVMinMax);
            var dUVPoints = dUVDomain.discretizeUVDomain(uDNum, vDNum);
            var rUVPoints = rUVDomain.discretizeUVDomain(uDNum, vDNum);
            // Step 3.1 获得曲面上的采样点
            BinaryWriter dBW = new BinaryWriter(new FileStream("designPoints", FileMode.Create));
            BinaryWriter rBW = new BinaryWriter(new FileStream("realPoints", FileMode.Create));

            for (int i = 0; i < uDNum; i++)
            {
                for (int j = 0; j < vDNum; j++)
                {
                    var dParam = dUVPoints[i * uDNum + j];
                    var point = getPtOnSurface(dParam, dSurf);
                    Tag pt;
                    theUFSession.Curve.CreatePoint(point, out pt);
                    dBW.Write(point[0]);
                    dBW.Write(point[1]);
                    dBW.Write(point[2]);

                    var rParam = rUVPoints[i * uDNum + j];
                    point = getPtOnSurface(rParam, rSurf);
                    theUFSession.Curve.CreatePoint(point, out pt);
                    rBW.Write(point[0]);
                    rBW.Write(point[1]);
                    rBW.Write(point[2]);
                }
            }
            dBW.Close();
            rBW.Close();
        }


        static double[] getPtOnSurface(UVParam param, Tag surf)
        {
            ModlSrfValue value;
            theUFSession.Modl.EvaluateFace(surf, 33, new double[] { param.U, param.V }, out value);
            return value.srf_pos;
        }

        static Tuple<double,double,double>[,] getGeodesic(Types.Graph graph, int mapNum, int step)
        {
            var distance = new Tuple<double, double, double>[mapNum, mapNum];
            for (int i = 0; i < mapNum; i++)
            {
                for (int j = 0; j < mapNum; j++)
                {
                    distance[i, j] = graph.getGeodesics((i + 1) * step, (j + 1) * step, step);
                }
            }
            return distance;
        }

        /// <summary>
        /// 计算一组UV参数在曲面对应点的曲率
        /// </summary>
        /// <param name="uvParam">UV参数</param>
        /// <param name="surf">曲面</param>
        /// <returns></returns>
        static double evaluateCurvature(UVParam uvParam, Tag surf)
        {
            var param = new double[] { uvParam.U, uvParam.V };
            var points = new double[3];
            var u1 = new double[3];
            var v1 = new double[3];
            var u2 = new double[3];
            var v2 = new double[3];
            var unitNorm = new double[3];
            var radii = new double[2];
            theUFSession.Modl.AskFaceProps(surf, param, points, u1, v1, u2, v2, unitNorm, radii);

            return (1 / radii[0] + 1 / radii[1]) / 2;
        }

        /// <summary>
        /// 计算UV参数列表在曲面上对应点的曲率
        /// </summary>
        /// <param name="uvParams">UV参数列表</param>
        /// <param name="surf">曲面</param>
        /// <returns></returns>
        static double[] evaluateCurvature(List<UVParam> uvParams, Tag surf)
        {
            List<double> curvatures = new List<double>();
            foreach (var item in uvParams)
            {
                curvatures.Add(evaluateCurvature(item, surf));
            }
            return curvatures.ToArray();
        }

        /// <summary>
        /// 计算一组UV参数在曲面上对应点的曲率对U参数和V参数的导数
        /// </summary>
        /// <param name="uvParam">UV参数</param>
        /// <param name="surf">曲面</param>
        /// <returns>UV参数的导数</returns>
        static UVParam evaluateCurvatureDeviation(UVParam uvParam, Tag surf)
        {
            const double eps = 0.001;
            double oCvtr = evaluateCurvature(uvParam, surf);
            double uDCvtr = evaluateCurvature(new UVParam(uvParam.U + eps, uvParam.V), surf);
            double vDCvtr = evaluateCurvature(new UVParam(uvParam.U, uvParam.V + eps), surf);
            var deviation = new UVParam((oCvtr - uDCvtr) / eps, (oCvtr - vDCvtr) / eps);
            return deviation;
        }
        /// <summary>
        /// 计算UV参数列表在曲面上对应点的曲率对U参数和V参数的导数
        /// </summary>
        /// <param name="uvParams">UV参数列表</param>
        /// <param name="surf">曲面</param>
        /// <returns>导数列表</returns>
        static UVParam[] evaluateCurvatureDeviation(List<UVParam> uvParams, Tag surf)
        {
            var deviations = new List<UVParam>();
            foreach (var item in uvParams)
            {
                deviations.Add(evaluateCurvatureDeviation(item, surf));
            }
            return deviations.ToArray();
        }
        /// <summary>
        /// 目标函数
        /// </summary>
        /// <param name="dCurvature">设计曲面曲率</param>
        /// <param name="rCurvature">实际曲面曲率</param>
        /// <returns></returns>
        static double targetFunction(double[] dCurvature, double[] rCurvature)
        {
            var targetValue = 0.0;
            if (dCurvature.Length == rCurvature.Length)
            {
                for (int i = 0; i < dCurvature.Length; i++)
                {
                    targetValue += Math.Pow(dCurvature[i] - rCurvature[i], 2);
                }
            }
            return targetValue;
        }
        
        /// <summary>
        /// 求Mobius映射在一组UV参数上的导数
        /// </summary>
        /// <param name="uvParam"></param>
        /// <param name="ctrlParam"></param>
        /// <returns></returns>
        static MobiusParam[] mobiusMapDeviation(UVParam uvParam, MobiusParam ctrlParam)
        {
            var z = new Complex(uvParam.U, uvParam.V);
            var aDeviation = z / (ctrlParam.a * z + ctrlParam.d);
            var bDeviation = 1 / (ctrlParam.a * z + ctrlParam.d);
            var cDeviation = -(ctrlParam.a * z + ctrlParam.b) * z / Complex.Pow((z * ctrlParam.c + ctrlParam.d), 2);
            var dDeviation = -(ctrlParam.a * z + ctrlParam.b) / Complex.Pow((z * ctrlParam.c + ctrlParam.d), 2);
            var y1Deviation = new MobiusParam(aDeviation, bDeviation, cDeviation, dDeviation);
            var y2Deviation = new MobiusParam(Complex.ImaginaryOne * aDeviation, Complex.ImaginaryOne * bDeviation, Complex.ImaginaryOne * cDeviation, Complex.ImaginaryOne * dDeviation);
            return new MobiusParam[] { y1Deviation, y2Deviation };
        }

        /// <summary>
        /// 求Mobius变换在UV参数列表上的导数
        /// </summary>
        /// <param name="uvParams">UV参数列表</param>
        /// <param name="ctrlParam">Mobius变换参数</param>
        /// <returns>导数集合</returns>
        static MobiusParam[][] mobiusMapDeviation(List<UVParam> uvParams, MobiusParam ctrlParam)
        {
            var ctrlDeviations = new List<MobiusParam[]>();
            foreach (var item in uvParams)
            {
                ctrlDeviations.Add(mobiusMapDeviation(item, ctrlParam));
            }
            return ctrlDeviations.ToArray();
        }

        static void updateCtrlParam(MobiusMap mobiusMap,List<UVParam> rUVParams, Tag rSurf, List<UVParam> dUVParams, double[] dCurvature, double[] rCurvature)
        {
            // 计算目标函数对UV参数的导数
            var uvDeviations = evaluateCurvatureDeviation(rUVParams, rSurf);
            // 计算UV参数对控制参数的导数
            var ctrlDeviations = mobiusMapDeviation(dUVParams, mobiusMap.ControlParams);

            var deviation = new MobiusParam();
            for (int i = 0; i < uvDeviations.Length; i++)
            {
                deviation += (dCurvature[i] - rCurvature[i]) * (uvDeviations[i].U * ctrlDeviations[i][0] + uvDeviations[i].V * ctrlDeviations[i][1]);
            }
            mobiusMap.ControlParams = -2 * (mobiusMap.ControlParams + (-0.1)*deviation);
        }

        static void showMesh(UVDomain domain, Tag surf, out List<Tag> pts, out List<Tag> uCurves, out List<Tag> vCurves)
        {
            // 计算参数点在曲面上的对应点
            var ptOnSurf = new List<Tag>();
            foreach (var item in domain.DiscretePoints)
            {
                Tag ptFeatureId,ptId;
                Tag uScalar, vScalar;
                theUFSession.So.CreateScalarDouble(surf, UFSo.UpdateOption.DontUpdate, item.U, out uScalar);
                theUFSession.So.CreateScalarDouble(surf, UFSo.UpdateOption.DontUpdate, item.V, out vScalar);
                theUFSession.Point.CreateOnSurface(surf, uScalar, vScalar, out ptFeatureId);
                theUFSession.Point.AskPointOutput(ptFeatureId, out ptId);

                ptOnSurf.Add(ptId);
            }
            // 绘制U方向的曲线簇
            var rUCurves = new List<Tag>();
            for (int i = 0; i < domain.numDiscreteU; i++)
            {
                Tag curve;
                var points = ptOnSurf.GetRange(i * domain.numDiscreteU, domain.numDiscreteV).ToArray();
                var point_data = new List<UFCurve.PtSlopeCrvatr>();
                foreach (var item in points)
                {
                    double[] coordinate = new double[3] { 0, 0, 0 };
                    theUFSession.Curve.AskPointData(item, coordinate);
                    point_data.Add(new UFCurve.PtSlopeCrvatr()
                    {
                        point = coordinate,
                        slope_type = UFConstants.UF_CURVE_SLOPE_NONE,
                        crvatr_type = UFConstants.UF_CURVE_CRVATR_NONE
                    });
                }
                theUFSession.Curve.CreateSplineThruPts(3, 0, points.Count(), point_data.ToArray(), null, 0, out curve);
                rUCurves.Add(curve);
            }
            // 绘制V方向的曲线簇
            var rVCurves = new List<Tag>();
            for (int i = 0; i < domain.numDiscreteV; i++)
            {
                Tag curve;
                var points = new List<Tag>();
                for (int j = 0; j < domain.numDiscreteU; j++)
                {
                    points.Add(ptOnSurf[j * domain.numDiscreteU + i]);
                }
                var point_data = new List<UFCurve.PtSlopeCrvatr>();
                foreach (var item in points)
                {
                    double[] coordinate = new double[3] { 0, 0, 0 };
                    theUFSession.Curve.AskPointData(item, coordinate);
                    point_data.Add(new UFCurve.PtSlopeCrvatr()
                    {
                        point = coordinate,
                        slope_type = UFConstants.UF_CURVE_SLOPE_NONE,
                        crvatr_type = UFConstants.UF_CURVE_CRVATR_NONE
                    });
                }
                theUFSession.Curve.CreateSplineThruPts(3, 0, points.Count(), point_data.ToArray(), null, 0, out curve);
                rVCurves.Add(curve);
            }

            pts = ptOnSurf;
            uCurves = rUCurves;
            vCurves = rVCurves;
        }

        static void getMeshLength(List<Tag> ptOnSurf, List<Tag> uCurve, List<Tag> vCurve, out List<List<double>> uArcLength, out List<List<double>> vArcLength)
        {
            IntPtr evaluator = new IntPtr();
            int uNum = uCurve.Count;
            int vNum = vCurve.Count;
            var uCruvesLength = new List<List<double>>();
            for (int i = 0; i < uNum; i++)
            {
                double[] coord1 = new double[3] { 0, 0, 0 };
                double[] coord2 = new double[3] { 0, 0, 0 };
                double[] closePt = new double[3] { 0, 0, 0 };
                double param1 = 0, param2;
                theUFSession.Eval.Initialize(uCurve[i], out evaluator);
                var uCruveLength = new List<double>();
                for (int j = 1; j < vNum; j++)
                {
                    double arcLength;
                    theUFSession.Curve.AskPointData(ptOnSurf[i * vNum + j], coord2);
                    theUFSession.Eval.EvaluateClosestPoint(evaluator, coord2, out param2, closePt);
                    theUFSession.Curve.AskArcLength(uCurve[i], param1, param2, ModlUnits.ModlMmeter, out arcLength);
                    uCruveLength.Add(arcLength);
                    param1 = param2;
                }
                uCruvesLength.Add(uCruveLength);
            }
            var vCruvesLength = new List<List<double>>();
            for (int i = 0; i < vNum; i++)
            {
                double[] coord1 = new double[3] { 0, 0, 0 };
                double[] coord2 = new double[3] { 0, 0, 0 };
                double[] closePt = new double[3] { 0, 0, 0 };
                double param1 = 0, param2;
                theUFSession.Eval.Initialize(vCurve[i], out evaluator);
                var vCruveLength = new List<double>();
                for (int j = 1; j < uNum; j++)
                {
                    double arcLength;
                    theUFSession.Curve.AskPointData(ptOnSurf[j * vNum + i], coord2);
                    theUFSession.Eval.EvaluateClosestPoint(evaluator, coord2, out param2, closePt);
                    theUFSession.Curve.AskArcLength(vCurve[i], param1, param2, ModlUnits.ModlMmeter, out arcLength);
                    vCruveLength.Add(arcLength);
                    param1 = param2;
                }
                vCruvesLength.Add(vCruveLength);
            }
            uArcLength = uCruvesLength;
            vArcLength = vCruvesLength;
        }

        
    }
}