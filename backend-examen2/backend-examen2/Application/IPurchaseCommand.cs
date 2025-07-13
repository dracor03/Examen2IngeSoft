using Domain;

public interface IProcessPurchaseCommand
{
    PurchaseResult Handle(PurchaseRequest request);
}
