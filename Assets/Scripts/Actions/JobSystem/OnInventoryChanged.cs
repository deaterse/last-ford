public class OnInventoryChanged: ISignal
{
    public Worker worker;
    public ResourceType resource;

    public OnInventoryChanged(Worker _worker, ResourceType _resource)
    {
        worker = _worker;
        resource = _resource;
    }
}