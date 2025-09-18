using EvoSim.ECS.Entities;

namespace EvoSim.ECS.Core;
public class EcsEngine
{
    private List<Entity> _entities = [];
    private List<ISystem> _systems = [];
    private int _nextEntityId = 0;

    public Entity CreateEntity()
    {
        var entity = new Entity(_nextEntityId++);
        _entities.Add(entity);
        return entity;
    }

    public void AddSystem(ISystem system)
    {
        _systems.Add(system);
    }

    public IEnumerable<Entity> GetEntitiesWith<T>() where T : IComponent
    {
        return _entities.Where(e => e.HasComponent<T>());
    }

    public IEnumerable<Entity> GetEntitiesWith(params Type[] componentTypes)
    {
        return _entities.Where(entity => entity.HasComponents(componentTypes));
    }

    public void Update(float deltaTime)
    {
        foreach (var system in _systems)
        {
            system.Update(this, deltaTime);
        }
    }

    public void RemoveEntity(Entity entity)
    {
        _entities.Remove(entity);
    }
}

