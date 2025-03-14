using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Survivor_Api.Data;
using Survivor_Api.Entities;
using Survivor_Api.Models.Competitors;

namespace Survivor_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitorsController : ControllerBase
    {
        private readonly SurvivorDbContext _db;
        public CompetitorsController(SurvivorDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = _db.Competitors.Select(x => new CompetitorGetResponse
            {
                FullName = x.FirstName + " " + x.LastName
            }).ToList();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var competitor = _db.Competitors.Find(id);

            if(competitor is null || competitor.IsDeleted == true)
                return NotFound();

            var response = new CompetitorGetResponse
            {
                FullName = competitor.FirstName + " " + competitor.LastName
            };

            return Ok(response);
        }

        [HttpGet("Category/{categoryId}")]
        public IActionResult GetByCategory(int categoryId)
        {
            var category = _db.Categories.Find(categoryId);

            if(category is null || category.IsDeleted == true) 
                return NotFound("Category Is Not Found");

            var competitors = _db.Competitors.Where(x => x.CategoryId == categoryId);

            var response = competitors.Select(x => new CompetitorGetResponse
            {
                FullName = x.FirstName + " " + x.LastName
            });

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create(CompetitorCreateRequest request, int categoryId)
        {
            var category = _db.Categories.Find(categoryId);

            if (category is null || category.IsDeleted == true)
                return NotFound("Category Is Not Found");

            categoryId = category.Id;
            var newCompetitor = new CompetitorEntity
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
            };

            newCompetitor.CreatedDate = DateTime.UtcNow;
            newCompetitor.CategoryId = categoryId;

            _db.Competitors.Add(newCompetitor);
            _db.SaveChanges();

            return Created();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CompetitorUpdateRequest request)
        {
            var competitor = _db.Competitors.Find(id);

            if (competitor is null || competitor.IsDeleted == true)
                return NotFound();

            competitor.FirstName = request.FirstName;
            competitor.LastName = request.LastName;
            competitor.ModifiedDate = DateTime.UtcNow;

            _db.Competitors.Update(competitor);
            _db.SaveChanges();

            return Ok("Success");

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var competitor = _db.Competitors.Find(id);

            if (competitor is null || competitor.IsDeleted == true)
                return NotFound();

            competitor.IsDeleted = true;
            competitor.ModifiedDate= DateTime.UtcNow;

            _db.Competitors.Update(competitor);
            _db.SaveChanges();

            return Ok("Success");


        }
    }
}
