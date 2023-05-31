using System.Collections.Generic;

namespace XRMDataManager.Library.Internal.DatatAccess
{
    public interface ISqlDataAccess
    {
        List<T> LoadData<T, U>(string storeProcedure, U parameters);
        void SaveData<T>(string storeProcedure, T parameters);
    }
}