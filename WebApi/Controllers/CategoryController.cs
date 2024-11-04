using Domain.Dtos;
using Domain.Filters;
using Infrastructure.PatternResultExtensions;
using Infrastructure.Services.Category;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/categorys")]
public sealed class CategoryController(ICategoryService categoryService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetCategories([FromQuery] CategoryFilter filter)
        => (categoryService.GetAllCategories(filter)).ToActionResult();

    [HttpGet("{id:int}")]
    public IActionResult GetCategoryById(int id)
        => (categoryService.GetCategoryById(id)).ToActionResult();
    
    [HttpPost]
    public IActionResult CreateCategory([FromBody] CategoryCreateDto dto)
        => (categoryService.CreateCategory(dto)).ToActionResult();

    [HttpPut("{id:int}")]
    public IActionResult UpdateCategory(int id,[FromBody] CategoryUpdateDto dto)
        => (categoryService.UpdateCategory(dto)).ToActionResult();

    [HttpDelete("{id:int}")]
    public IActionResult DeleteCategory(int id)
        => (categoryService.DeleteCategory(id)).ToActionResult();
}