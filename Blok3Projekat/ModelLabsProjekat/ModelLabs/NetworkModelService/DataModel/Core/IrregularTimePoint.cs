using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class IrregularTimePoint : IdentifiedObject
    {
        public IrregularTimePoint(long globalId) : base(globalId)
        {
        }

        public float Time { get; set; }
        public float Value1 { get; set; }
        public float Value2 { get; set; }
        public long IntervalSchedule { get; set; } = 0;

        public override bool Equals(object x)
        {
            if (base.Equals(x))
            {
                IrregularTimePoint itp = (IrregularTimePoint)x;
                return ((itp.Time == this.Time) && (itp.Value1 == this.Value1) && (itp.Value2 == this.Value2) && (itp.IntervalSchedule == this.IntervalSchedule));
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

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.IRREGULARTIMEPOINT_IRREGULARIINTERVALSCHEDULE:
                case ModelCode.IRREGULARTIMEPOINT_TIME:
                case ModelCode.IRREGULARTIMEPOINT_VALUE1:
                case ModelCode.IRREGULARTIMEPOINT_VALUE2:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.IRREGULARTIMEPOINT_IRREGULARIINTERVALSCHEDULE:
                    prop.SetValue(IntervalSchedule);
                    break;
                case ModelCode.IRREGULARTIMEPOINT_TIME:
                    prop.SetValue(Time);
                    break;
                case ModelCode.IRREGULARTIMEPOINT_VALUE1:
                    prop.SetValue(Value1);
                    break;
                case ModelCode.IRREGULARTIMEPOINT_VALUE2:
                    prop.SetValue(Value2);
                    break;

                default:
                    base.GetProperty(prop);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.IRREGULARTIMEPOINT_IRREGULARIINTERVALSCHEDULE:
                    IntervalSchedule = property.AsReference();
                    break;
                case ModelCode.IRREGULARTIMEPOINT_TIME:
                    Time = property.AsFloat();
                    break;
                case ModelCode.IRREGULARTIMEPOINT_VALUE1:
                    Value1 = property.AsFloat();
                    break;
                case ModelCode.IRREGULARTIMEPOINT_VALUE2:
                    Value2 = property.AsFloat();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

        #region IReference implementation

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (IntervalSchedule != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.IRREGULARTIMEPOINT_IRREGULARIINTERVALSCHEDULE] = new List<long>();
                references[ModelCode.IRREGULARTIMEPOINT_IRREGULARIINTERVALSCHEDULE].Add(IntervalSchedule);
            }

            base.GetReferences(references, refType);
        }

        #endregion IReference implementation
    }
}
