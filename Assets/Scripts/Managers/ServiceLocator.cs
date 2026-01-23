public static class ServiceLocator
{
    private static ResourceManager _resourceManager;
    private static Pathfinder _pathfinder;
    
    public static void ProvideResourceManager(ResourceManager manager)
    {
        _resourceManager = manager;
    }

    public static void ProvidePathfinder(Pathfinder pathfinder)
    {
        _pathfinder = pathfinder;
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
}