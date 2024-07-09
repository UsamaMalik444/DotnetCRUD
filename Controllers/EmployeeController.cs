using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using react_crud_be.EmployeeContext;
using react_crud_be.Models;

namespace react_crud_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly MyDbContext _employeeContext;
        public EmployeeController(MyDbContext employeeContext)
        {
            _employeeContext = employeeContext;
        }


        [HttpGet]

        public async Task<ActionResult<IEnumerable<Student>>> GetEmployees()
        { 
            if(_employeeContext.Student == null)
            {
                return NotFound();
            } 
            return await _employeeContext.Student.ToListAsync();
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<Student>> GetEmployee(int id)
        {
            if (_employeeContext.Student == null)
            {
                return NotFound();
            }
            var student = await _employeeContext.Student.FindAsync(id);
            if(student == null)
            {
                return NotFound();
            } 
            return student;
        }


        [HttpPost]

        public async Task<ActionResult<Student>> PostEmployee(Student student)
        {
            _employeeContext.Student.Add(student);
            await _employeeContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new {id = student.ID}, student);


        }


        [HttpPut("{id}")]

        public async Task<ActionResult> PutEmployee(int id, Student student)
        {
            if(id != student.ID)
            {
                return BadRequest();
            }
            _employeeContext.Entry(student).State = EntityState.Modified;

            try
            {
                await _employeeContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                throw;
            }
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            if( _employeeContext.Student == null)
            {
                return NotFound();  
            }
            var student = await _employeeContext.Student.FindAsync(id);
            if(student == null)
            {
                return NotFound();
            }
            _employeeContext.Student.Remove(student);
            await _employeeContext.SaveChangesAsync();  
            return Ok();
        }
    }
}
