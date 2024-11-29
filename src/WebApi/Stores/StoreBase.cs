using System.Globalization;

using static System.Reflection.BindingFlags;

namespace VirtualBookstore.WebApi.Stores;

public class StoreBase
{
    protected TModel? Load<TModel>(params object[] parameters)
    {
        return (TModel?)Activator.CreateInstance(typeof(TModel), 
            NonPublic | Instance,
            null,
            parameters,
            CultureInfo.InvariantCulture);
    }
}
