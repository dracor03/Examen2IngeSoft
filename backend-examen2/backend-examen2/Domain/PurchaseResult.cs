namespace Domain;

public class PurchaseResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;

    public ChangeResult? Change { get; set; }
}
