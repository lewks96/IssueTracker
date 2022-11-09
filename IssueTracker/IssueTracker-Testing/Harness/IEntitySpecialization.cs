namespace IssueTracker_Testing.Harness
{
    public interface IEntitySpecialization<TEntity>
        where TEntity : class, new()
    {
        TEntity ModifyEntity(TEntity e);
        bool CompareEntity(TEntity entity1, TEntity entity2);
    }
}
