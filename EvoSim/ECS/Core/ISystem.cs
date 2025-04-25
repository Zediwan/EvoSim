namespace EvoSim.ECS.Core;

public interface ISystem
{
    void Update(EcsEngine ecsEngine, float deltaTime);
}

