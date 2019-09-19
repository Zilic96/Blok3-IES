using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Outage
{
    public class SwitchingOperation : IdentifiedObject
    {
        public SwitchingOperation(long globalId) : base(globalId)
        {
        }

        public SwichState NewState { get; set; }
        public DateTime OperationTime { get; set; }
        public long OutageSchedule { get; set; } = 0;
        public List<long> Switches { get; set; } = new List<long>();

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                SwitchingOperation so = (SwitchingOperation)obj;
                return (CompareHelper.CompareLists(so.Switches, this.Switches) && (so.OutageSchedule == this.OutageSchedule) && (so.OperationTime == this.OperationTime) &&(so.NewState == this.NewState));
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
                case ModelCode.SWITCHINGOPERATION_NEWSTATE:
                case ModelCode.SWITCHINGOPERATION_OPERATIONTIME:
                case ModelCode.SWITCHINGOPERATION_OUTAGESCHEDULE:
                case ModelCode.SWITCHINGOPERATION_SWITCHS:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.SWITCHINGOPERATION_NEWSTATE:
                    property.SetValue((short)NewState);
                    break;
                case ModelCode.SWITCHINGOPERATION_OPERATIONTIME:
                    property.SetValue(OperationTime);
                    break;
                case ModelCode.SWITCHINGOPERATION_OUTAGESCHEDULE:
                    property.SetValue(OutageSchedule);
                    break;
                case ModelCode.SWITCHINGOPERATION_SWITCHS:
                    property.SetValue(Switches);
                    break;

                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.SWITCHINGOPERATION_NEWSTATE:
                    NewState = (SwichState)property.AsEnum();
                    break;
                case ModelCode.SWITCHINGOPERATION_OPERATIONTIME:
                    OperationTime = property.AsDateTime();
                    break;
                case ModelCode.SWITCHINGOPERATION_OUTAGESCHEDULE:
                    OutageSchedule = property.AsReference();
                    break;
                


                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

        #region IReference implementation

        public override bool IsReferenced
        {
            get
            {
                return (Switches.Count > 0) || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {

            if (Switches != null && Switches.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.SWITCHINGOPERATION_SWITCHS] = Switches.GetRange(0, Switches.Count);
            }
            if (OutageSchedule != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.SWITCHINGOPERATION_OUTAGESCHEDULE] = new List<long>();
                references[ModelCode.SWITCHINGOPERATION_OUTAGESCHEDULE].Add(OutageSchedule);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.SWITCH_SWITCHINGOPERATION:
                    Switches.Add(globalId);
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
                case ModelCode.SWITCH_SWITCHINGOPERATION:

                    if (Switches.Contains(globalId))
                    {
                        Switches.Remove(globalId);
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
