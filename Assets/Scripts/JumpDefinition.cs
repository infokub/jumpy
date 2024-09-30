public static class JumpDefinition
{
    public static float Gravity(float maxHeight, float maxLength, float baseWalkSpeed) // v0 = -2 * h / t_h^2 || -2 * h * v^2 / L_h^2
    {
        return -2 * maxHeight * baseWalkSpeed * baseWalkSpeed / ((maxLength * 0.5f) * (maxLength * 0.5f));
    }

    public static float InitSpeed(float maxHeight, float maxLength, float baseWalkSpeed) // v0 = 2 * h / t_h || 2 * h * v / L_h
    {
        return 2 * maxHeight * baseWalkSpeed / ((maxLength * 0.5f));
    }
}

