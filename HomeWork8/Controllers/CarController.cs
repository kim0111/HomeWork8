using HomeWork8.Data;
using HomeWork8.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace HomeWork8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
       
        private readonly CarContext _dbConext;

        public CarController( CarContext car)
        {
            _dbConext = car;
            
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _dbConext.Cars.ToListAsync();

            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _dbConext.Cars.FindAsync(id);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Car model)
        {
            if (model == null)
                return BadRequest();

            _dbConext.Cars.Add(model);

            await _dbConext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _dbConext.Cars.FindAsync(id);

            if (response == null)
                return NotFound();

            _dbConext.Cars.Remove(response);

            await _dbConext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("Id")]
        public async Task<IActionResult> Put(int Id, Car car)
        {
            if (Id == car.Id)
            {

                var carObject = _dbConext.Cars.FindAsync(Id);

                try
                {
                    if (await carObject != null)
                    {
                        _dbConext.Entry(carObject).State = EntityState.Modified;
                        await _dbConext.SaveChangesAsync();
                    }
                    else
                    {
                        _dbConext.Cars.Add(car);
                    }

                    await _dbConext.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (Id != car.Id)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }

                }

            }
            else
            {
                return BadRequest();
            }

            return NoContent();
        }


        [HttpPatch]
        [Route("Id")]
        public async Task<IActionResult> Patch(int Id, Car car)
        {
            var cId = await _dbConext.Cars.SingleAsync(x => x.Id == Id);

            
            cId.Name = car.Name;
            cId.Color = car.Color;
            cId.Age = car.Age;
            cId.Price = car.Price;
            cId.Type = car.Type;
            cId.Engine = car.Engine;
            cId.IsKz = car.IsKz;

            await _dbConext.SaveChangesAsync();

            return Ok();
        }


    }

}

