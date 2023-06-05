using System.Collections.Generic;

namespace XRMDataManager.Library.Internal.DatatAccess
{
    public interface ISqlDataAccess
    {
        void CommitTransaction();
        void Dispose();
        List<T> LoadData<T, U>(string storeProcedure, U parameters);
        List<T> LoadDataInTransaction<T, U>(string storeProcedure, U parameters);
        void RollbackTransaction();
        void SaveData<T>(string storeProcedure, T parameters);
        void SaveDataInTransiction<T>(string storeProcedure, T parameters);
        void StartTransaction();
    }
}