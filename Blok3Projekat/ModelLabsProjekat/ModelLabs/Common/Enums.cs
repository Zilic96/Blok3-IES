using System;

namespace FTN.Common
{	
    public enum UnitMultiplier : short
    {
        G = 0,
        M = 1,
        T = 2,
        c = 3,
        d = 4,
        k = 5,
        m = 6,
        micro = 7,
        n = 8,
        none = 9,
        p = 10
    }

    public enum UnitSymbol : short
    {
        A = 0,
        F = 1,
        H = 2,
        Hz = 3,
        J = 4,
        N = 5,
        Pa = 6,
        S = 7,
        V = 8,
        VA = 9,
        VAh = 10,
        VAr = 11,
        VArh = 12,
        W = 13,
        Wh = 14,
        deg = 15,
        degC = 16,
        g = 17,
        h = 18,
        m = 19,
        m2 = 20,
        m3 = 21,
        min = 22,
        none = 23,
        ohm = 24,
        rad = 25,
        s = 26
    }

    public enum SwichState : short
    {
        open = 0,
        close = 1
    }

    public enum CurveStyle : short
    {
        constantYValue = 0,
        formula = 1,
        rampYValue = 2,
        straightLineYValue = 3
    }
}
