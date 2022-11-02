using aspnetcorewebapi1.data;
using aspnetcorewebapi1.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace aspnetcorewebapi1.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ContactController : Controller
    {
        private readonly DbContextApicontact dbContextApicontact;
        public ContactController(DbContextApicontact dbContextApicontact)
        {
            this.dbContextApicontact = dbContextApicontact;
        }
        [HttpGet]
        public async Task<IActionResult> GetContact()
        {
            var result = await dbContextApicontact.Contacts.ToListAsync();
            return Ok(result);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact = await dbContextApicontact.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);



        }
        [HttpPost]
        public async Task<IActionResult> CreateContact(AddContactRequest newcontact)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                FullName = newcontact.FullName,
                Address = newcontact.Address,
                Email = newcontact.Email,
                PhoneNumber = newcontact.PhoneNumber
            };
            var result = await dbContextApicontact.Contacts.AddAsync(contact);
            object value = await dbContextApicontact.SaveChangesAsync();
            return Ok(contact);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequest updatecontact)
        {
            var contact = await dbContextApicontact.Contacts.FindAsync(id);
            if (contact != null)
            {
                //contact.Id = Guid.NewGuid();
                contact.FullName = updatecontact.FullName;
                contact.Address = updatecontact.Address;
                contact.Email = updatecontact.Email;
                contact.PhoneNumber = updatecontact.PhoneNumber;
                await dbContextApicontact.SaveChangesAsync();
                return Ok(contact);
            }

            return NotFound();
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await dbContextApicontact.Contacts.FindAsync(id);
            if (contact != null)
            {
                dbContextApicontact.Remove(contact);
                return Ok(contact);
            }
            return NotFound();
        }
    }
}