using System;
namespace BaseIdentity.BusinessLayer.Abstract
{
    public interface IGenericService<T>
    {
        void TInsert(T t);
        void TUpdate(T t);
        void TDelete(T t);
        List<T> TGetList();

        T TGetByID(int id);
    }
}

