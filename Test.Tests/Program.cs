﻿using DataLayer;
using Microsoft.EntityFrameworkCore;

var db = new Context();

//CreateCategory(db);
//DeleteCategory(db);
//UpdateCategory(db);

foreach (var entity in db.OrderDetails.Include(x=>x.Product).ThenInclude(x=>x.Category).Include(x=>x.Order)) Console.WriteLine(entity);
//foreach (var entity in db.Products) Console.WriteLine(entity);



static void CreateCategory(Context db)
{
    var category = new Category
    {
        Id = 101,
        Name = "flksdflæsfj",
        Description = "dfsdfdfs"
    };

    db.Add(category);

    db.SaveChanges();
}

static void DeleteCategory(Context db)
{
    var category = db.Categories.FirstOrDefault(x => x.Id == 101);
    if (category != null)
    {
        db.Categories.Remove(category);
        db.SaveChanges();
    }
}

static void UpdateCategory(Context db)
{
    var category = db.Categories.FirstOrDefault(x => x.Id == 101);
    if (category != null)
    {
        category.Name = "Updated";
        db.SaveChanges();
    }
}