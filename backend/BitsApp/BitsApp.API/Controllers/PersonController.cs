using BitsApp.API.Contracts;
using BitsApp.Core.Interfaces;
using BitsApp.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BitsApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PersonResponse>>> GetPersons()
        {
            var persons = await _personService.GetAllPersons();

            var response = persons.Select(p => new PersonResponse(p.Id, p.Name, p.DateOfBirth, p.isMarried,p.Phone, p.Salary));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<List<PersonFileResponse>>> CreatePerson(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty");

            using var stream = file.OpenReadStream();
            var persons = await _personService.CreatePerson(stream);

            var personDtos = persons.Select(p => new PersonFileResponse(p.Name, p.DateOfBirth, p.isMarried, p.Phone, p.Salary)).ToList();
            return Ok(personDtos);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<int>> UpdatePerson(int id, [FromBody] PersonRequest request)
        {
            var personId = await _personService.UpdatePerson(id, request.Name!, request.DateOfBirth, request.isMarried, request.Phone!, request.Salary);

            return Ok(personId);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<int>> DeletePerson(int id)
        {
            return Ok(await _personService.DeletePerson(id));
        }
    }
}
