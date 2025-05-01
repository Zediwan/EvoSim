namespace EvoSim.Test;

public abstract class DebugTest
{
#if DEBUG
    public static bool Debugging => true;
#else
    public static bool Debugging => false;
#endif

    protected DebugTest()
    {
        Skip.IfNot(Debugging, "Skipping because this test is only for Debug mode.");
    }
}