using EvoSim.ECS.Core;

namespace EvoSim.ECS.Entities;

public class Entity
{
    #region Constants and Statics

    #endregion

    #region Variables and Properties
    public int Id { get; }
    private Dictionary<Type, IComponent> _Components = new ();
    #endregion

    #region Constructors and Destructors

    public Entity(int id)
    {
        Id = id;
    }

    #endregion

    #region Methods

    public void AddComponent<T>(T component) where T : IComponent
    {
        _Components[typeof(T)] = component;
    }

    public T GetComponent<T>() where T : IComponent
    {
        return (T)_Components[typeof(T)];
    }

    public bool HasComponent<T>() where T : IComponent
    {
        return _Components.ContainsKey(typeof(T));
    }

    #endregion
}

