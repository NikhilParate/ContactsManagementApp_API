using ContactsManagement.Models;
using ContactsManagement.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ContactsManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;

        public ContactsController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_contactRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var contact = _contactRepository.GetById(id);
            if (contact == null) return NotFound();
            return Ok(contact);
        }

        [HttpPost]
        public IActionResult Create(Contact contact)
        {
            try
            {
                _contactRepository.Add(contact);
                return CreatedAtAction(nameof(GetById), new { id = contact.id }, contact);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Contact contact)
        {
            if (id != contact.id) return BadRequest();
            try
            {
                bool result = _contactRepository.Update(contact);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool result = _contactRepository.Delete(id);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
