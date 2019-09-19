using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTN.Common;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class CurveData : IdentifiedObject
    {
        public CurveData(long globalId) : base(globalId)
        {
        }

        public float XValue { get; set; }
        public float Y1Value { get; set; }
        public float Y2Value { get; set; }
        public float Y3Value { get; set; }
        public long Curve { get; set; } = 0;

        public override bool Equals(object x)
        {
            if (base.Equals(x))
            {
                CurveData curveData = (CurveData)x;
                return ((curveData.XValue == this.XValue) && (curveData.Y1Value == this.Y1Value) && (curveData.Y2Value == this.Y2Value) && (curveData.Y3Value == this.Y3Value) && (curveData.Curve == this.Curve));
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
                case ModelCode.CURVEDATA_CURVE:
                case ModelCode.CURVEDATA_XVALUE:
                case ModelCode.CURVEDATA_Y1VALUE:
                case ModelCode.CURVEDATA_Y2VALUE:
                case ModelCode.CURVEDATA_Y3VALUE:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.CURVEDATA_CURVE:
                    prop.SetValue(Curve);
                    break;
                case ModelCode.CURVEDATA_XVALUE:
                    prop.SetValue(XValue);
                    break;
                case ModelCode.CURVEDATA_Y1VALUE:
                    prop.SetValue(Y1Value);
                    break;
                case ModelCode.CURVEDATA_Y2VALUE:
                    prop.SetValue(Y2Value);
                    break;
                case ModelCode.CURVEDATA_Y3VALUE:
                    prop.SetValue(Y3Value);
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
                case ModelCode.CURVEDATA_CURVE:
                    Curve = property.AsReference();
                    break;
                case ModelCode.CURVEDATA_XVALUE:
                    XValue = property.AsFloat();
                    break;
                case ModelCode.CURVEDATA_Y1VALUE:
                    Y1Value = property.AsFloat();
                    break;
                case ModelCode.CURVEDATA_Y2VALUE:
                    Y2Value = property.AsFloat();
                    break;
                case ModelCode.CURVEDATA_Y3VALUE:
                    Y3Value = property.AsFloat();
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
            if (Curve != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.CURVEDATA_CURVE] = new List<long>();
                references[ModelCode.CURVEDATA_CURVE].Add(Curve);
            }

            base.GetReferences(references, refType);
        }

        #endregion IReference implementation

    }
}
