using UnityEngine;

/*
 https://discussions.unity.com/t/re-map-a-number-from-one-range-to-another/465623/12
*/

public class MathUtil: MonoBehaviour
{
    // Shorter version, no clamping
    public static float RemapRange(float val, float in1, float in2, float out1, float out2)
    {
        return RemapRange(val, in1, in2, out1, out2, false, false, false, false);
    }
    // Full version, clamping settable on all 4 range elements (in1, in2, out1, out2)
    public static float RemapRange(float val, float in1, float in2, float out1, float out2,
        bool in1Clamped, bool in2Clamped, bool out1Clamped, bool out2Clamped)
    {
        if (in1Clamped == true && val < in1) val = in1;
        if (in2Clamped == true && val > in2) val = in2;

        float result = out1 + (val - in1) * (out2 - out1) / (in2 - in1);

        if (out1Clamped == true && result < out1) result = out1;
        if (out2Clamped == true && result > out2) result = out2;

        return result;
    }
}
