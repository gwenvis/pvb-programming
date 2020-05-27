#ifndef GetLightPos
#define GetLightPos

void MyGetPosition_float(out float4 Out)
{
    Out = float4(1,1,1,0);
#ifdef UNIVERSAL_INPUT_INCLUDED
	Out = _MainLightPosition;
#endif
}

#endif