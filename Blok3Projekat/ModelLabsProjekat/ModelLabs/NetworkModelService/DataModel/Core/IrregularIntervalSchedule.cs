using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class IrregularIntervalSchedule : BasicIntervalSchedule
    {
        public IrregularIntervalSchedule(long globalId) : base(globalId)
        {
        }

        public List<long> TimePoints { get; set; } = new List<long>();

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                IrregularIntervalSchedule iis = (IrregularIntervalSchedule)obj;
                return (CompareHelper.CompareLists(iis.TimePoints, this.TimePoints));
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
                case ModelCode.IRREGULARIINTERVALSCHEDULE_IRREGULARTIMEPOINTS:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.IRREGULARIINTERVALSCHEDULE_IRREGULARTIMEPOINTS:
                    property.SetValue(TimePoints);
                    break;

                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            base.SetProperty(property);
        }

        #endregion IAccess implementation

        #region IReference implementation

        public override bool IsReferenced
        {
            get
            {
                return (TimePoints.Count > 0) || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {

            if (TimePoints != null && TimePoints.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.IRREGULARIINTERVALSCHEDULE_IRREGULARTIMEPOINTS] = TimePoints.GetRange(0, TimePoints.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.IRREGULARTIMEPOINT_IRREGULARIINTERVALSCHEDULE:
                    TimePoints.Add(globalId);
                    break;

                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }

        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.IRREGULARTIMEPOINT_IRREGULARIINTERVALSCHEDULE:

                    if (TimePoints.Contains(globalId))
                    {
                        TimePoints.Remove(globalId);
                    }
                    else
                    {
                        CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
                    }

                    break;

                default:
                    base.RemoveReference(referenceId, globalId);
                    break;
            }
        }

        #endregion IReference implementation
    }
}
