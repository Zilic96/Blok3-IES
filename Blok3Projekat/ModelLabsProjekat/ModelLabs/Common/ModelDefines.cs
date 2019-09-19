using System;
using System.Collections.Generic;
using System.Text;

namespace FTN.Common
{
	
	public enum DMSType : short
	{		
		MASK_TYPE							= unchecked((short)0xFFFF),

        GROUNDDDISCONNECTOR                 = 0x0001,
        SWITCHINGOPERATION                  = 0x0002,
        OUTAGESCHEDULE                      = 0x0003,
		CURVE       						= 0x0004,
		CURVEDATA							= 0x0005,
        IRREGULARTIMEPOINT                  = 0X0006,

    }

    [Flags]
	public enum ModelCode : long
	{
		IDOBJ								    = 0x1000000000000000,
		IDOBJ_GID							    = 0x1000000000000104,
		IDOBJ_ALIASNAME 					    = 0x1000000000000207,
		IDOBJ_MRID							    = 0x1000000000000307,
		IDOBJ_NAME							    = 0x1000000000000407,	

		PSR									    = 0x1100000000000000,

        EQUIPMENT                               = 0x1110000000000000,

        CONDEQ                                  = 0x1111000000000000,

        SWITCH                                  = 0x1111100000000000,
        SWITCH_SWITCHINGOPERATION               = 0x1111100000000109,

        GROUNDDDISCONNECTOR                     = 0x1111110000010000,

        BASICINTERVALSCHEDULE                   = 0x1200000000000000,
        BASICINTERVALSCHEDULE_STARTTIME         = 0x1200000000000108,
        BASICINTERVALSCHEDULE_VALUE1MULTIPLIER  = 0x120000000000020a,
        BASICINTERVALSCHEDULE_VALUE1UNIT        = 0x120000000000030a,
        BASICINTERVALSCHEDULE_VALUE2MULTIPLIER  = 0x120000000000040a,
        BASICINTERVALSCHEDULE_VALUE2UNIT        = 0x120000000000050a,

        IRREGULARIINTERVALSCHEDULE              = 0x1210000000000000,
        IRREGULARIINTERVALSCHEDULE_IRREGULARTIMEPOINTS = 0x1210000000000119,

        IRREGULARTIMEPOINT                      = 0x1600000000060000,
        IRREGULARTIMEPOINT_TIME                 = 0x1600000000060105,
        IRREGULARTIMEPOINT_VALUE1               = 0x1600000000060205,
        IRREGULARTIMEPOINT_VALUE2               = 0x1600000000060305,
        IRREGULARTIMEPOINT_IRREGULARIINTERVALSCHEDULE = 0x1600000000060409,

        OUTAGESCHEDULE                          = 0x1211000000030000,
        OUTAGESCHEDULE_SWITCHINGOPERATIONS      = 0x1211000000030119,

        SWITCHINGOPERATION                       = 0x1300000000020000,
        SWITCHINGOPERATION_NEWSTATE              = 0x130000000002010a,
        SWITCHINGOPERATION_OPERATIONTIME         = 0x1300000000020208,
        SWITCHINGOPERATION_SWITCHS               = 0x1300000000020319,
        SWITCHINGOPERATION_OUTAGESCHEDULE        = 0x1300000000020409,

        CURVE                                   = 0x1400000000040000,
        CURVE_STYLE                             = 0x140000000004010a,
        CURVE_XMULTIPLIER                       = 0x140000000004020a,
        CURVE_XUNIT                             = 0x140000000004030a,
        CURVE_Y1MULTIPLIER                      = 0x140000000004040a,
        CURVE_Y1UNIT                            = 0x140000000004050a,
        CURVE_Y2MULTIPLIER                      = 0x140000000004060a,
        CURVE_Y2UNIT                            = 0x140000000004070a,
        CURVE_Y3MULTIPLIER                      = 0x140000000004080a,
        CURVE_Y3UNIT                            = 0x140000000004090a,
        CURVE_CURVEDATAS                        = 0x1400000000040a19,

        CURVEDATA                              = 0x1500000000050000,
        CURVEDATA_XVALUE                       = 0x1500000000050105,
        CURVEDATA_Y1VALUE                      = 0x1500000000050205,
        CURVEDATA_Y2VALUE                      = 0x1500000000050305,
        CURVEDATA_Y3VALUE                      = 0x1500000000050405,
        CURVEDATA_CURVE                        = 0x1500000000050509,

	}

    [Flags]
	public enum ModelCodeMask : long
	{
		MASK_TYPE			 = 0x00000000ffff0000,
		MASK_ATTRIBUTE_INDEX = 0x000000000000ff00,
		MASK_ATTRIBUTE_TYPE	 = 0x00000000000000ff,

		MASK_INHERITANCE_ONLY = unchecked((long)0xffffffff00000000),
		MASK_FIRSTNBL		  = unchecked((long)0xf000000000000000),
		MASK_DELFROMNBL8	  = unchecked((long)0xfffffff000000000),		
	}																		
}


