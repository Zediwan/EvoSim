using EvoSim.ECS.Core;

namespace EvoSim.ECS.Entities;

public class Entity(int id)
{
    #region Variables and Properties
    public int Id { get; } = id;
    private readonly Dictionary<Type, IComponent> _components = new();
    #endregion

    #region Methods

    public void AddComponent<T>(T component) where T : IComponent
    {
        _components[typeof(T)] = component;
    }

    public T GetComponent<T>() where T : IComponent
    {
        return (T)_components[typeof(T)];
    }

    public bool HasComponent<T>() where T : IComponent
    {
        return _components.ContainsKey(typeof(T));
    }

    public bool HasComponents(params Type[] componentTypes)
    {
        return componentTypes.All(type => _components.ContainsKey(type));
    }

    #endregion
}

