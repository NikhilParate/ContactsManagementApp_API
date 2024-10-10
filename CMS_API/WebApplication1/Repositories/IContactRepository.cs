using ContactsManagement.Models;
using System.Collections.Generic;

namespace ContactsManagement.Repositories
{
    public interface IContactRepository
    {
        IEnumerable<Contact> GetAll();
        Contact GetById(int id);
        void Add(Contact contact);
        bool Update(Contact contact);
        bool Delete(int id);
    }
}
