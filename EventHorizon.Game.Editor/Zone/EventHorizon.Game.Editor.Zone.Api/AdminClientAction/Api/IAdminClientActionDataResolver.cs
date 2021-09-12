namespace EventHorizon.Game.Editor.Zone.AdminClientAction.Api
{
    public interface IAdminClientActionDataResolver
    {
        T Resolve<T>(
            string argumentName
        );
        T? ResolveNullable<T>(
            string argumentName
        );
    }
}
