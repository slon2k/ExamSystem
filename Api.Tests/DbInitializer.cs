using ApplicationCore.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Api.Tests
{
    public static class DbInitializer
    {
        public static void SeedData(DataContext context)
        {
            var categories = context.Categories.ToList();
            if (categories.Count == 0)
            {
                try
                {
                    categories.AddRange(GetCategories());
                    context.SaveChanges();
                    var count = categories.Count();
                }
                catch (Exception)
                {
                    throw;
                }
                
            }

        }

        public static List<Category> GetCategories()
        {
            var categories = new List<Category>();
            for (int i = 1; i <= 6; i++)
            {
                categories.Add(new Category {Id = Guid.NewGuid(), Title = @$"Title {i}", Description = $@"Description {i}" });
            }
            return categories;
        }
            

    }
}
