using ContactsManagement.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ContactsManagement.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly string _filePath;

        public ContactRepository(string filePath)
        {
            _filePath = filePath;
        }

        public IEnumerable<Contact> GetAll()
        {
            if (!File.Exists(_filePath)) return new List<Contact>();
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Contact>>(json) ?? new List<Contact>();
        }

        public Contact GetById(int id)
        {
            return GetAll().FirstOrDefault(c => c.id == id);
        }

        public void Add(Contact contact)
        {
            var contacts = GetAll().ToList();
            contact.id = contacts.Count + 1;
            contacts.Add(contact);
            SaveToFile(contacts);
        }

        public bool Update(Contact contact)
        {
            var contacts = GetAll().ToList();
            var index = contacts.FindIndex(c => c.id == contact.id);
            if (index != -1)
            {
                contacts[index] = contact;
                SaveToFile(contacts);
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            var contacts = GetAll().ToList();
            if(contacts.Exists(x => x.id == id))
            {
                contacts.RemoveAll(c => c.id == id);
                SaveToFile(contacts);
                return true;
            }
            return false;
        }

        private void SaveToFile(List<Contact> contacts)
        {
            var json = JsonSerializer.Serialize(contacts);
            File.WriteAllText(_filePath, json);
        }
    }
}
