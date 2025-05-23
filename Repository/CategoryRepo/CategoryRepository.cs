﻿using AgainPBL3.Data;
using AgainPBL3.Interfaces;
using AgainPBL3.Models;
using Microsoft.EntityFrameworkCore;

namespace AgainPBL3.Repository.CategoryRepo
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Category> AddCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> GetCategoryById(int ID)
        {
            return await _context.Categories.FirstAsync(p => p.CategoryId == ID);
        }

        public async Task<List<Category>> GetListCategory()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> UpdateCategory(int ID, string categoryName)
        {
            var category = await _context.Categories.FindAsync(ID);
            if (category == null)
            {
                return null;
            }
            category.CategoryName = categoryName;
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<int?> DeleteCategory(int ID)
        {
            var category = await _context.Categories.FindAsync(ID);
            if (category == null)
            {
                return null;
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return ID;
        }
    }
}
