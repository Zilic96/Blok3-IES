using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Outage
{
    public class OutageShedule : IrregularIntervalSchedule
    {
        public OutageShedule(long globalId) : base(globalId)
        {
        }

        public List<long> SwitchingOperations { get; set; } = new List<long>();

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                OutageShedule outageShedule = (OutageShedule)obj;
                return (CompareHelper.CompareLists(outageShedule.SwitchingOperations, this.SwitchingOperations));
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
                case ModelCode.OUTAGESCHEDULE_SWITCHINGOPERATIONS:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.OUTAGESCHEDULE_SWITCHINGOPERATIONS:
                    property.SetValue(SwitchingOperations);
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
                return (SwitchingOperations.Count > 0) || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {

            if (SwitchingOperations != null && SwitchingOperations.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.OUTAGESCHEDULE_SWITCHINGOPERATIONS] = SwitchingOperations.GetRange(0, SwitchingOperations.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.SWITCHINGOPERATION_OUTAGESCHEDULE:
                    SwitchingOperations.Add(globalId);
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
                case ModelCode.SWITCHINGOPERATION_OUTAGESCHEDULE:

                    if (SwitchingOperations.Contains(globalId))
                    {
                        SwitchingOperations.Remove(globalId);
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
