namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	using FTN.Common;
    using System;

    /// <summary>
    /// PowerTransformerConverter has methods for populating
    /// ResourceDescription objects using PowerTransformerCIMProfile_Labs objects.
    /// </summary>
    public static class PowerTransformerConverter
	{

		#region Populate ResourceDescription
		public static void PopulateIdentifiedObjectProperties(FTN.IdentifiedObject cimIdentifiedObject, ResourceDescription rd)
		{
			if ((cimIdentifiedObject != null) && (rd != null))
			{
				if (cimIdentifiedObject.MRIDHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_MRID, cimIdentifiedObject.MRID));
				}
				if (cimIdentifiedObject.NameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_NAME, cimIdentifiedObject.Name));
				}
				if (cimIdentifiedObject.AliasNameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_ALIASNAME, cimIdentifiedObject.AliasName));
				}
			}
		}


		public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPowerSystemResource, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimPowerSystemResource != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimPowerSystemResource, rd);
				
			}
		}

		

		public static void PopulateEquipmentProperties(FTN.Equipment cimEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimEquipment != null) && (rd != null))
			{
				PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimEquipment, rd, importHelper, report);
			}
		}

		public static void PopulateConductingEquipmentProperties(FTN.ConductingEquipment cimConductingEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimConductingEquipment != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateEquipmentProperties(cimConductingEquipment, rd, importHelper, report);

			}
		}

        public static void PopulateSwitchProperties(FTN.Switch cimSwitch, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimSwitch != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateConductingEquipmentProperties(cimSwitch, rd, importHelper, report);

                if (cimSwitch.SwitchingOperationsHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimSwitch.SwitchingOperations.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimSwitch.GetType().ToString()).Append(" rdfID = \"").Append(cimSwitch.ID);
                        report.Report.Append("\" - Failed to set reference to SwitchingOperation: rdfID \"").Append(cimSwitch.SwitchingOperations.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.SWITCH_SWITCHINGOPERATION, gid));
                }
            }
        }

        public static void PopulateGroundDisconnectorProperties(FTN.GroundDisconnector cimGroundDisconnector, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimGroundDisconnector != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateSwitchProperties(cimGroundDisconnector, rd, importHelper, report);

            }
        }

        public static void PopulateCurveProperties(FTN.Curve cimCurve, ResourceDescription rd)
        {
            if ((cimCurve != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimCurve, rd);

                if (cimCurve.CurveStyleHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVE_STYLE, (short)GetDMSCurveStyle(cimCurve.CurveStyle)));
                }
                if (cimCurve.XMultiplierHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVE_XMULTIPLIER, (short)GetDMSUnitMultiplier(cimCurve.XMultiplier)));
                }
                if (cimCurve.XUnitHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVE_XUNIT, (short)GetDMSUnitSymbol(cimCurve.XUnit)));
                }
                if (cimCurve.Y1MultiplierHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVE_Y1MULTIPLIER, (short)GetDMSUnitMultiplier(cimCurve.Y1Multiplier)));
                }
                if (cimCurve.Y1UnitHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVE_Y1UNIT, (short)GetDMSUnitSymbol(cimCurve.Y1Unit)));
                }
                if (cimCurve.Y2MultiplierHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVE_Y2MULTIPLIER, (short)GetDMSUnitMultiplier(cimCurve.Y2Multiplier)));
                }
                if (cimCurve.Y2UnitHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVE_Y2UNIT, (short)GetDMSUnitSymbol(cimCurve.Y2Unit)));
                }
                if (cimCurve.Y3MultiplierHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVE_Y3MULTIPLIER, (short)GetDMSUnitMultiplier(cimCurve.Y3Multiplier)));
                }
                if (cimCurve.Y3UnitHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVE_Y3UNIT, (short)GetDMSUnitSymbol(cimCurve.Y3Unit)));
                }

            }
        }

        public static void PopulateCurveDataProperties(FTN.CurveData cimCurveData, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimCurveData != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimCurveData , rd);

                if (cimCurveData.XvalueHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVEDATA_XVALUE, cimCurveData.Xvalue));
                }
                if (cimCurveData.Y1valueHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVEDATA_Y1VALUE, cimCurveData.Y1value));
                }
                if (cimCurveData.Y2valueHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVEDATA_Y2VALUE, cimCurveData.Y2value));
                }
                if (cimCurveData.Y3valueHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVEDATA_Y3VALUE, cimCurveData.Y3value));
                }

                if (cimCurveData.CurveHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimCurveData.Curve.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimCurveData.GetType().ToString()).Append(" rdfID = \"").Append(cimCurveData.ID);
                        report.Report.Append("\" - Failed to set reference to Curve: rdfID \"").Append(cimCurveData.Curve.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.CURVEDATA_CURVE, gid));
                }

            }
        }

        public static void PopulateBasicIntervalScheduleProperties(FTN.BasicIntervalSchedule cimBasicIntervalSchedule, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimBasicIntervalSchedule != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimBasicIntervalSchedule, rd);

				if (cimBasicIntervalSchedule.StartTimeHasValue)
				{
					rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_STARTTIME, cimBasicIntervalSchedule.StartTime));
				}
				if (cimBasicIntervalSchedule.Value1MultiplierHasValue)
				{
					rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_VALUE1MULTIPLIER, (short)GetDMSUnitMultiplier(cimBasicIntervalSchedule.Value1Multiplier)));
				}
                if (cimBasicIntervalSchedule.Value1UnitHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_VALUE1UNIT, (short)GetDMSUnitSymbol(cimBasicIntervalSchedule.Value1Unit)));
                }
                if (cimBasicIntervalSchedule.Value2MultiplierHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_VALUE2MULTIPLIER, (short)GetDMSUnitMultiplier(cimBasicIntervalSchedule.Value2Multiplier)));
                }
                if (cimBasicIntervalSchedule.Value2UnitHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_VALUE2UNIT, (short)GetDMSUnitSymbol(cimBasicIntervalSchedule.Value2Unit)));
                }
            }
		}



		public static void PopulateIrregularIntervalScheduleProperties(FTN.IrregularIntervalSchedule cimIrregularIntervalSchedule, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimIrregularIntervalSchedule != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateBasicIntervalScheduleProperties(cimIrregularIntervalSchedule, rd, importHelper, report);
			}
		}

        public static void PopulateIrregularTimePointProperties(FTN.IrregularTimePoint cimIrregularTimePoint, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimIrregularTimePoint != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimIrregularTimePoint, rd);

                if (cimIrregularTimePoint.TimeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IRREGULARTIMEPOINT_TIME, cimIrregularTimePoint.Time));
                }
                if (cimIrregularTimePoint.Value1HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IRREGULARTIMEPOINT_VALUE1, cimIrregularTimePoint.Value1));
                }
                if (cimIrregularTimePoint.Value2HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IRREGULARTIMEPOINT_VALUE2, cimIrregularTimePoint.Value2));
                }

                if (cimIrregularTimePoint.IntervalScheduleHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimIrregularTimePoint.IntervalSchedule.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimIrregularTimePoint.GetType().ToString()).Append(" rdfID = \"").Append(cimIrregularTimePoint.ID);
                        report.Report.Append("\" - Failed to set reference to IntervalSchedule: rdfID \"").Append(cimIrregularTimePoint.IntervalSchedule.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.IRREGULARTIMEPOINT_IRREGULARIINTERVALSCHEDULE, gid));
                }
            }
        }

        public static void PopulateOutageScheduleProperties(FTN.OutageSchedule cimOutageSchedule, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimOutageSchedule != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIrregularIntervalScheduleProperties(cimOutageSchedule, rd, importHelper, report);
            }
        }

        public static void PopulateSwitchingOperationProperties(FTN.SwitchingOperation cimSwitchingOperation, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimSwitchingOperation != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimSwitchingOperation, rd);

                if (cimSwitchingOperation.NewStateHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SWITCHINGOPERATION_NEWSTATE, (short)GetDMSSwichState(cimSwitchingOperation.NewState)));
                }
                if (cimSwitchingOperation.OperationTimeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SWITCHINGOPERATION_OPERATIONTIME, cimSwitchingOperation.OperationTime));
                }

                if (cimSwitchingOperation.OutageScheduleHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimSwitchingOperation.OutageSchedule.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimSwitchingOperation.GetType().ToString()).Append(" rdfID = \"").Append(cimSwitchingOperation.ID);
                        report.Report.Append("\" - Failed to set reference to OutageSchedule: rdfID \"").Append(cimSwitchingOperation.OutageSchedule.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.SWITCHINGOPERATION_OUTAGESCHEDULE, gid));
                }
            }
        }
        
		#endregion Populate ResourceDescription

		#region Enums convert
		public static CurveStyle GetDMSCurveStyle(FTN.CurveStyle curveStyle)
		{
			switch (curveStyle)
			{
				case FTN.CurveStyle.constantYValue:
					return CurveStyle.constantYValue;
                case FTN.CurveStyle.formula:
                    return CurveStyle.formula;
                case FTN.CurveStyle.rampYValue:
                    return CurveStyle.rampYValue;
                case FTN.CurveStyle.straightLineYValues:
                    return CurveStyle.straightLineYValue;

                default:
                    return CurveStyle.constantYValue;
			}
		}

		public static SwichState GetDMSSwichState(FTN.SwitchState swichState)
		{
			switch (swichState)
			{
				case FTN.SwitchState.close:
					return SwichState.close;
                case FTN.SwitchState.open:
                    return SwichState.open;

                default:
					return SwichState.close;
			}
		}

		public static UnitMultiplier GetDMSUnitMultiplier(FTN.UnitMultiplier unitMultiplier)
		{
			switch (unitMultiplier)
			{
                
				case FTN.UnitMultiplier.c:
					return UnitMultiplier.c;
                case FTN.UnitMultiplier.d:
                    return UnitMultiplier.d;
                case FTN.UnitMultiplier.G:
                    return UnitMultiplier.G;
                case FTN.UnitMultiplier.k:
                    return UnitMultiplier.k;
                case FTN.UnitMultiplier.m:
                    return UnitMultiplier.m;
                case FTN.UnitMultiplier.M:
                    return UnitMultiplier.M;
                case FTN.UnitMultiplier.micro:
                    return UnitMultiplier.micro;
                case FTN.UnitMultiplier.n:
                    return UnitMultiplier.n;
                case FTN.UnitMultiplier.p:
                    return UnitMultiplier.p;
                case FTN.UnitMultiplier.T:
                    return UnitMultiplier.T;
                default:
					return UnitMultiplier.none;
			}
		}

		public static UnitSymbol GetDMSUnitSymbol(FTN.UnitSymbol unitSymbol)
		{
			switch (unitSymbol)
			{
				case FTN.UnitSymbol.A:
					return UnitSymbol.A;
                case FTN.UnitSymbol.deg:
                    return UnitSymbol.deg;
                case FTN.UnitSymbol.degC:
                    return UnitSymbol.degC;
                case FTN.UnitSymbol.F:
                    return UnitSymbol.F;
                case FTN.UnitSymbol.g:
                    return UnitSymbol.g;
                case FTN.UnitSymbol.h:
                    return UnitSymbol.h;
                case FTN.UnitSymbol.H:
                    return UnitSymbol.H;
                case FTN.UnitSymbol.Hz:
                    return UnitSymbol.Hz;
                case FTN.UnitSymbol.J:
                    return UnitSymbol.J;
                case FTN.UnitSymbol.m:
                    return UnitSymbol.m;
                case FTN.UnitSymbol.m2:
                    return UnitSymbol.m2;
                case FTN.UnitSymbol.m3:
                    return UnitSymbol.m3;
                case FTN.UnitSymbol.min:
                    return UnitSymbol.min;
                case FTN.UnitSymbol.N:
                    return UnitSymbol.N;
                case FTN.UnitSymbol.ohm:
                    return UnitSymbol.ohm;
                case FTN.UnitSymbol.Pa:
                    return UnitSymbol.Pa;
                case FTN.UnitSymbol.rad:
                    return UnitSymbol.rad;
                case FTN.UnitSymbol.s:
                    return UnitSymbol.s;
                case FTN.UnitSymbol.S:
                    return UnitSymbol.S;
                case FTN.UnitSymbol.V:
                    return UnitSymbol.V;
                case FTN.UnitSymbol.VA:
                    return UnitSymbol.VA;
                case FTN.UnitSymbol.VAh:
                    return UnitSymbol.VAh;
                case FTN.UnitSymbol.VAr:
                    return UnitSymbol.VAr;
                case FTN.UnitSymbol.VArh:
                    return UnitSymbol.VArh;
                case FTN.UnitSymbol.W:
                    return UnitSymbol.W;
                case FTN.UnitSymbol.Wh:
                    return UnitSymbol.Wh;

                default:
					return UnitSymbol.none;
			}
		}
		#endregion Enums convert
	}
}
