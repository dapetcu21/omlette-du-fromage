using UnityEngine;

public class MathUtil
{
    public static float AngleShortDiff (float diff) {
      float d = diff * (1.0f / 360.0f);
      return 360.0f * (d - Mathf.Round(d));
    }
    
    public static float LowPassFilter(float x, float newX, float dt, float cutoff)
    {
        float RC = 1.0f / (cutoff * 2.0f * Mathf.PI);
        float alpha = dt / (dt + RC);
        return x + alpha * (newX - x);
    }
}
