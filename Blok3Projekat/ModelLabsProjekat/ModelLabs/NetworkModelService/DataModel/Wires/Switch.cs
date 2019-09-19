using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class Switch : ConductingEquipment
    {
        public Switch(long globalId) : base(globalId)
        {
        }

        public long SwitchingOperations { get; set; } = 0;

        public override bool Equals(object x)
        {
            if (base.Equals(x))
            {
                Switch s = (Switch)x;
                return (s.SwitchingOperations == this.SwitchingOperations);
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
                case ModelCode.SWITCH_SWITCHINGOPERATION:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.SWITCH_SWITCHINGOPERATION:
                    prop.SetValue(SwitchingOperations);
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
                case ModelCode.SWITCH_SWITCHINGOPERATION:
                    SwitchingOperations = property.AsReference();
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
            if (SwitchingOperations != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.SWITCH_SWITCHINGOPERATION] = new List<long>();
                references[ModelCode.SWITCH_SWITCHINGOPERATION].Add(SwitchingOperations);
            }

            base.GetReferences(references, refType);
        }

        #endregion IReference implementation
    }
}
