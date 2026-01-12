using System;
using System.Collections.Generic;
using System.Text;

namespace DapperTest
{
    public interface IOwnerRepository
    {
        void Create(Owner owner);
        Owner? GetById(int id);
        Owner GetByIdIncludePets(int id);
        List<Owner> GetAll();
    }
}
