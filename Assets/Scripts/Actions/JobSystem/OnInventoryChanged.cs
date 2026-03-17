public class OnInventoryChanged: ISignal
{
    public Worker worker;
    public ResourceAmount resource;

    public OnInventoryChanged(Worker _worker, ResourceAmount _resource)
    {
        worker = _worker;
        resource = _resource;
    }
}