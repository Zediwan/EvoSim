namespace EvoSim.Test;

public class ReleaseTest
{
#if DEBUG
    public static bool Debugging => true;
#else
    public static bool Debugging => false;
#endif

    public ReleaseTest()
    {
        Skip.If(Debugging, "Skipping because this test is only for Release mode.");
    }
}
