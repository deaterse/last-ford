public static class ServiceLocator
{
    private static ResourceManager _resourceManager;
    
    public static void ProvideResourceManager(ResourceManager manager)
    {
        _resourceManager = manager;
    }
    
    public static ResourceManager GetResourceManager()
    {
        if (_resourceManager == null)
            throw new System.Exception("ResourceManager not provided!");
        return _resourceManager;
    }
}