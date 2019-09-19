using System;
using System.Collections.Generic;
using CIM.Model;
using FTN.Common;
using FTN.ESI.SIMES.CIM.CIMAdapter.Manager;

namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	/// <summary>
	/// PowerTransformerImporter
	/// </summary>
	public class PowerTransformerImporter
	{
		/// <summary> Singleton </summary>
		private static PowerTransformerImporter ptImporter = null;
		private static object singletoneLock = new object();

		private ConcreteModel concreteModel;
		private Delta delta;
		private ImportHelper importHelper;
		private TransformAndLoadReport report;


		#region Properties
		public static PowerTransformerImporter Instance
		{
			get
			{
				if (ptImporter == null)
				{
					lock (singletoneLock)
					{
						if (ptImporter == null)
						{
							ptImporter = new PowerTransformerImporter();
							ptImporter.Reset();
						}
					}
				}
				return ptImporter;
			}
		}

		public Delta NMSDelta
		{
			get 
			{
				return delta;
			}
		}
		#endregion Properties


		public void Reset()
		{
			concreteModel = null;
			delta = new Delta();
			importHelper = new ImportHelper();
			report = null;
		}

		public TransformAndLoadReport CreateNMSDelta(ConcreteModel cimConcreteModel)
		{
			LogManager.Log("Importing PowerTransformer Elements...", LogLevel.Info);
			report = new TransformAndLoadReport();
			concreteModel = cimConcreteModel;
			delta.ClearDeltaOperations();

			if ((concreteModel != null) && (concreteModel.ModelMap != null))
			{
				try
				{
					// convert into DMS elements
					ConvertModelAndPopulateDelta();
				}
				catch (Exception ex)
				{
					string message = string.Format("{0} - ERROR in data import - {1}", DateTime.Now, ex.Message);
					LogManager.Log(message);
					report.Report.AppendLine(ex.Message);
					report.Success = false;
				}
			}
			LogManager.Log("Importing PowerTransformer Elements - END.", LogLevel.Info);
			return report;
		}

		/// <summary>
		/// Method performs conversion of network elements from CIM based concrete model into DMS model.
		/// </summary>
		private void ConvertModelAndPopulateDelta()
		{
			LogManager.Log("Loading elements and creating delta...", LogLevel.Info);

            //// import all concrete model types (DMSType enum)
            ImportCurve();
            ImportCurveData();
            ImportOutageShedule();
            ImportIrregularTimePoint();
            ImportSwitchingOperation();
            ImportGroundDisconector();
            

            LogManager.Log("Loading elements and creating delta completed.", LogLevel.Info);
		}

		#region Import
		private void ImportGroundDisconector()
		{
			SortedDictionary<string, object> cimGroundDisconectors = concreteModel.GetAllObjectsOfType("FTN.GroundDisconnector");
			if (cimGroundDisconectors != null)
			{
				foreach (KeyValuePair<string, object> cimGroundDisconectorPair in cimGroundDisconectors)
				{
					FTN.GroundDisconnector cimGroundDisconnector = cimGroundDisconectorPair.Value as FTN.GroundDisconnector;

					ResourceDescription rd = CreateGroundDisconnectorResourceDescription(cimGroundDisconnector);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("GroundDisconnector ID = ").Append(cimGroundDisconnector.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("GroundDisconnector ID = ").Append(cimGroundDisconnector.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateGroundDisconnectorResourceDescription(FTN.GroundDisconnector cimGroundDisconnector)
		{
			ResourceDescription rd = null;
			if (cimGroundDisconnector != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.GROUNDDDISCONNECTOR, importHelper.CheckOutIndexForDMSType(DMSType.GROUNDDDISCONNECTOR));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimGroundDisconnector.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateGroundDisconnectorProperties(cimGroundDisconnector, rd, importHelper, report);
			}
			return rd;
		}
		
		private void ImportSwitchingOperation()
		{
			SortedDictionary<string, object> cimSwitchingOperations = concreteModel.GetAllObjectsOfType("FTN.SwitchingOperation");
			if (cimSwitchingOperations != null)
			{
				foreach (KeyValuePair<string, object> cimSwitchingOperationPair in cimSwitchingOperations)
				{
					FTN.SwitchingOperation cimcimSwitchingOperation = cimSwitchingOperationPair.Value as FTN.SwitchingOperation;

					ResourceDescription rd = CreatecimSwitchingOperationResourceDescription(cimcimSwitchingOperation);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("SwitchingOperation ID = ").Append(cimcimSwitchingOperation.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("SwitchingOperation ID = ").Append(cimcimSwitchingOperation.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreatecimSwitchingOperationResourceDescription(FTN.SwitchingOperation cimSwitchingOperation)
		{
			ResourceDescription rd = null;
			if (cimSwitchingOperation != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.SWITCHINGOPERATION, importHelper.CheckOutIndexForDMSType(DMSType.SWITCHINGOPERATION));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimSwitchingOperation.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateSwitchingOperationProperties(cimSwitchingOperation, rd, importHelper, report);
			}
			return rd;
		}

		private void ImportOutageShedule()
		{
			SortedDictionary<string, object> cimOutageSchedules = concreteModel.GetAllObjectsOfType("FTN.OutageSchedule");
			if (cimOutageSchedules != null)
			{
				foreach (KeyValuePair<string, object> cimOutageSchedulePair in cimOutageSchedules)
				{
					FTN.OutageSchedule cimOutageSchedule = cimOutageSchedulePair.Value as FTN.OutageSchedule;

					ResourceDescription rd = CreateOutageScheduleResourceDescription(cimOutageSchedule);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("OutageSchedule ID = ").Append(cimOutageSchedule.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("OutageSchedule ID = ").Append(cimOutageSchedule.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateOutageScheduleResourceDescription(FTN.OutageSchedule cimOutageSchedule)
		{
			ResourceDescription rd = null;
			if (cimOutageSchedule != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.OUTAGESCHEDULE, importHelper.CheckOutIndexForDMSType(DMSType.OUTAGESCHEDULE));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimOutageSchedule.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateOutageScheduleProperties(cimOutageSchedule, rd, importHelper, report);
			}
			return rd;
		}

		private void ImportCurve()
		{
			SortedDictionary<string, object> cimCurves = concreteModel.GetAllObjectsOfType("FTN.Curve");
			if (cimCurves != null)
			{
				foreach (KeyValuePair<string, object> cimCurvePair in cimCurves)
				{
					FTN.Curve cimCurve = cimCurvePair.Value as FTN.Curve;

					ResourceDescription rd = CreateCurveResourceDescription(cimCurve);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("Curve ID = ").Append(cimCurve.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("Curve ID = ").Append(cimCurve.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateCurveResourceDescription(FTN.Curve cimCurve)
		{
			ResourceDescription rd = null;
			if (cimCurve != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.CURVE, importHelper.CheckOutIndexForDMSType(DMSType.CURVE));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimCurve.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateCurveProperties(cimCurve, rd);
			}
			return rd;
		}

		private void ImportCurveData()
		{
			SortedDictionary<string, object> cimCurveDatas = concreteModel.GetAllObjectsOfType("FTN.CurveData");
			if (cimCurveDatas != null)
			{
				foreach (KeyValuePair<string, object> cimCurveDataPair in cimCurveDatas)
				{
					FTN.CurveData cimCurveData = cimCurveDataPair.Value as FTN.CurveData;

					ResourceDescription rd = CreateCurveDataResourceDescription(cimCurveData);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("CurveData ID = ").Append(cimCurveData.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("CurveData ID = ").Append(cimCurveData.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateCurveDataResourceDescription(FTN.CurveData cimCurveData)
		{
			ResourceDescription rd = null;
			if (cimCurveData != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.CURVEDATA, importHelper.CheckOutIndexForDMSType(DMSType.CURVEDATA));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimCurveData.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateCurveDataProperties(cimCurveData, rd, importHelper, report);
			}
			return rd;
		}

        private void ImportIrregularTimePoint()
        {
            SortedDictionary<string, object> cimIrregularTimePoints = concreteModel.GetAllObjectsOfType("FTN.IrregularTimePoint");
            if (cimIrregularTimePoints != null)
            {
                foreach (KeyValuePair<string, object> cimIrregularTimePointPair in cimIrregularTimePoints)
                {
                    FTN.IrregularTimePoint cimIrregularTimePoint = cimIrregularTimePointPair.Value as FTN.IrregularTimePoint;

                    ResourceDescription rd = CreateIrregularTimePointResourceDescription(cimIrregularTimePoint);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("IrregularTimePoint ID = ").Append(cimIrregularTimePoint.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("IrregularTimePoint ID = ").Append(cimIrregularTimePoint.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateIrregularTimePointResourceDescription(FTN.IrregularTimePoint cimIrregularTimePoint)
        {
            ResourceDescription rd = null;
            if (cimIrregularTimePoint != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.IRREGULARTIMEPOINT, importHelper.CheckOutIndexForDMSType(DMSType.IRREGULARTIMEPOINT));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimIrregularTimePoint.ID, gid);

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateIrregularTimePointProperties(cimIrregularTimePoint, rd, importHelper, report);
            }
            return rd;
        }
        #endregion Import
    }
}

