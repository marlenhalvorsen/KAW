namespace KAW.Application.Contracts.Request
{
    public record UpdateExpressionRequest(
        int id, 
        string name, 
        string descrption
        );

}
