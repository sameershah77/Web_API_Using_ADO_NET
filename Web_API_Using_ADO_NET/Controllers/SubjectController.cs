using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Web_API_Using_ADO_NET.Model;
using Web_API_Using_ADO_NET.Service;

namespace Web_API_Using_ADO_NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService service;

        public SubjectController(ISubjectService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(service.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var subject = service.GetById(id);
            if (subject == null) return NotFound();
            return Ok(subject);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Subject subject)
        {
            bool result = service.Add(subject);
            return result ? Ok("Subject created successfully.") : BadRequest("Subject already exists. Cannot create duplicate.");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Subject subject)
        {
            subject.Id = id;
            bool result = service.Update(subject);
            return result ? Ok("Subject updated successfully.") : BadRequest("Subject not found. Cannot delete.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool result = service.Delete(id);
            return result ? Ok("Subject deleted successfully.") : BadRequest("Subject not found. Cannot delete.");
        }
    }
}
