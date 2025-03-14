using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Survivor_Api.Data;
using Survivor_Api.Entities;
using Survivor_Api.Models.Categories;

namespace Survivor_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly SurvivorDbContext _db;
        public CategoriesController(SurvivorDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult Create(CategoryCreateRequest request)
        {
            var newCategory = new CategoryEntity
            {
                Name = request.Name
            };

            newCategory.CreatedDate = DateTime.UtcNow;

            _db.Categories.Add(newCategory);
            _db.SaveChanges();

            return Created();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _db.Categories.Select(x => new CategoryGetResponse
            {
                Name = x.Name
            }).ToList();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = _db.Categories.Find(id);

            if(category is null || category.IsDeleted == true) 
                return NotFound();

            var response = new CategoryGetResponse
            {
                Name = category.Name
            };

            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CategoryUpdateRequest request)
        {
            var category = _db.Categories.Find(id);

            if(category is null || category.IsDeleted == true)
                return NotFound();

            category.Name = request.Name;
            category.ModifiedDate = DateTime.UtcNow;

            _db.Categories.Update(category);
            _db.SaveChanges();

            return Ok("Success");

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _db.Categories.Find(id);

            if (category is null || category.IsDeleted == true)
                return NotFound();

            category.IsDeleted = true;
            category.ModifiedDate= DateTime.UtcNow;

            _db.Categories.Update(category);
            _db.SaveChanges();

            return Ok("Success");
        }

    }
}
