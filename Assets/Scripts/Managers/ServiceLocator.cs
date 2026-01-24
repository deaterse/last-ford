public static class ServiceLocator
{
    // require a refactoring
    private static ResourceManager _resourceManager;
    private static Pathfinder _pathfinder;
    private static EventBus _eventBus;
    
    public static void ProvideResourceManager(ResourceManager manager)
    {
        _resourceManager = manager;
    }

    public static void ProvidePathfinder(Pathfinder pathfinder)
    {
        _pathfinder = pathfinder;
    }

    public static void ProvideEventBus(EventBus eventBus)
    {
        _eventBus = eventBus;
    }
    
    public static ResourceManager GetResourceManager()
    {
        if (_resourceManager == null)
            throw new System.Exception("ResourceManager not provided!");
        return _resourceManager;
    }

    public static Pathfinder GetPathfinder()
    {
        if (_pathfinder == null)
            throw new System.Exception("Pathfinder not provided!");
        return _pathfinder;
    }

    public static EventBus GetEventBus()
    {
        if (_eventBus == null)
            throw new System.Exception("Eventbus not provided!");
        return _eventBus;
    }
}