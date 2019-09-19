using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTN.Common;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class BasicIntervalSchedule : IdentifiedObject
    {
        public BasicIntervalSchedule(long globalId) : base(globalId)
        {
        }

        public DateTime StartTime { get; set; }
        public UnitMultiplier Value1Multiplier { get; set; }
        public UnitSymbol Value1Unit { get; set; }
        public UnitMultiplier Value2Multiplier { get; set; }
        public UnitSymbol Value2Unit { get; set; }

        public override bool Equals(object x)
        {
            if (base.Equals(x))
            {
                BasicIntervalSchedule bis = (BasicIntervalSchedule)x;
                return ((bis.StartTime == this.StartTime) && (bis.Value1Multiplier == this.Value1Multiplier) && (bis.Value1Unit == this.Value1Unit) && (bis.Value2Multiplier == this.Value2Multiplier) && (bis.Value2Unit == this.Value2Unit));
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IAccess implementation

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.BASICINTERVALSCHEDULE_STARTTIME:
                    property.SetValue(StartTime);
                    break;

                case ModelCode.BASICINTERVALSCHEDULE_VALUE1MULTIPLIER:
                    property.SetValue((short)Value1Multiplier);
                    break;

                case ModelCode.BASICINTERVALSCHEDULE_VALUE1UNIT:
                    property.SetValue((short)Value1Unit);
                    break;

                case ModelCode.BASICINTERVALSCHEDULE_VALUE2MULTIPLIER:
                    property.SetValue((short)Value2Multiplier);
                    break;

                case ModelCode.BASICINTERVALSCHEDULE_VALUE2UNIT:
                    property.SetValue((short)Value2Unit);
                    break;

                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.BASICINTERVALSCHEDULE_STARTTIME:
                case ModelCode.BASICINTERVALSCHEDULE_VALUE1MULTIPLIER:
                case ModelCode.BASICINTERVALSCHEDULE_VALUE1UNIT:
                case ModelCode.BASICINTERVALSCHEDULE_VALUE2MULTIPLIER:
                case ModelCode.BASICINTERVALSCHEDULE_VALUE2UNIT:

                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.BASICINTERVALSCHEDULE_STARTTIME:
                    StartTime = property.AsDateTime();
                    break;

                case ModelCode.BASICINTERVALSCHEDULE_VALUE1MULTIPLIER:
                    Value1Multiplier = (UnitMultiplier)property.AsEnum();
                    break;

                case ModelCode.BASICINTERVALSCHEDULE_VALUE1UNIT:
                    Value1Unit = (UnitSymbol)property.AsEnum();
                    break;

                case ModelCode.BASICINTERVALSCHEDULE_VALUE2MULTIPLIER:
                    Value2Multiplier = (UnitMultiplier)property.AsEnum();
                    break;

                case ModelCode.BASICINTERVALSCHEDULE_VALUE2UNIT:
                    Value2Unit = (UnitSymbol)property.AsEnum();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation
    }
}
