﻿using AutoMapper;
using BLOGN.Data.Services.IService;
using BLOGN.Models;
using BLOGN.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BLOGN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAll();
            var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);

            return Ok(categoriesDto);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            var category = await _categoryService.Get(Id);
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> Save(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            var newCategory = await _categoryService.Add(category);
            return Created(String.Empty, null); // _mapper.Map<CategoryDto>(newCategory));
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(int Id, CategoryDto categoryDto)
        {
            if (Id == 0)
            {
                return BadRequest();
            }

            var category = _mapper.Map<Category>(categoryDto);
            _categoryService.Update(category);
            return NoContent();
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var entity = _categoryService.Delete(Id);
            return NoContent();
        }


    }
}