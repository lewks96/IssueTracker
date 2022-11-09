using System.Diagnostics.CodeAnalysis;

namespace IssueTracker_Testing.Harness
{
    [ExcludeFromCodeCoverage]
    class EntityFactory
    {
        public static object GetRandomValueForType(Random random, Type propertyType)
        {
            if(propertyType == typeof(int))
                return random.Next();
            if(propertyType == typeof(string))
                return random.Next();
            return null!;
        }

        public static TEntity GenerateNew<TEntity>(Random random)
            where TEntity : class, new()
        {
            var entity = new TEntity();
            var idInfo = entity.GetType().GetProperty("Id");
            if (idInfo == null)
                throw new TypeAccessException(); // find more suitable exception 
            idInfo.SetValue(entity, GetRandomValueForType(random, idInfo.PropertyType) , null);
            return entity;
        }
    }
}
