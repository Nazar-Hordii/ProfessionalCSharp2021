﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ColumnNames;

class Runner
{
    private readonly IDbContextFactory<MenusContext> _menusContextFactory;

    public Runner(IDbContextFactory<MenusContext> menusContextFactory)
    {
        _menusContextFactory = menusContextFactory;
    }

    public Task CreateDatabaseAsync()
    {
        using var context = _menusContextFactory.CreateDbContext();
        return context.Database.EnsureCreatedAsync();
    }

    public async Task AddRecordsAsync()
    {
        Console.WriteLine(nameof(AddRecordsAsync));
        using var context = _menusContextFactory.CreateDbContext();
        MenuCard soupCard = new("Soups");

        Menu[] soups = new[]
        {
            new Menu("Consommé Célestine (with shredded pancake)")
            {
                Price = 4.8m,
                MenuCard = soupCard
            },
            new Menu("Baked Potato Soup")
            {
                Price = 4.8m,
                MenuCard = soupCard
            },
            new Menu("Cheddar Broccoli Soup")
            {
                Price = 4.8m,
                MenuCard = soupCard
            }
        };

        foreach (var soup in soups)
        {
            soupCard.Menus.Add(soup);
        }

        context.MenuCards.Add(soupCard);

        ShowState(context);
        int records = await context.SaveChangesAsync();
        Console.WriteLine($"{records} added");
        Console.WriteLine();
    }

    private void ShowState(MenusContext context)
    {
        foreach (EntityEntry entry in context.ChangeTracker.Entries())
        {
            Console.WriteLine($"type: {entry.Entity.GetType().Name}, " +
              $"state: {entry.State}, {entry.Entity}");
        }
        Console.WriteLine();
    }

    public async Task ObjectTrackingAsync()
    {
        using var context = _menusContextFactory.CreateDbContext();
        Console.WriteLine(nameof(ObjectTrackingAsync));
        var m1 = await (from m in context.Menus
                        where m.Text.StartsWith("Con")
                        select m).FirstOrDefaultAsync();
        var m2 = await (from m in context.Menus
                        where m.Text.Contains("(")
                        select m).FirstOrDefaultAsync();
        if (object.ReferenceEquals(m1, m2))
        {
            Console.WriteLine("the same object");
        }
        else
        {
            Console.WriteLine("not the same");
        }
        ShowState(context);
        
        Console.WriteLine();
    }

    public async Task UpdateRecordsAsync()
    {
        using var context = _menusContextFactory.CreateDbContext();
        Menu menu = await context.Menus
          .Skip(1)
          .FirstOrDefaultAsync();

        ShowState(context);
        menu.Price += 0.2m;
        ShowState(context);
        int records = context.SaveChanges();
        Console.WriteLine($"{records} updated");
        ShowState(context);
    }



    public async Task UpdateRecordUntrackedAsync()
    {
        Task<Menu> GetMenuAsync()
        {
            using var context = _menusContextFactory.CreateDbContext();
            return context.Menus
                .Skip(2)
                .FirstOrDefaultAsync();
        }

        async Task UpdateMenuAsync(Menu menu)
        {
            using var context = _menusContextFactory.CreateDbContext();
            ShowState(context);
            // EntityEntry<Menu> entry = context.Menus.Attach(m);
            // entry.State = EntityState.Modified;
            context.Menus.Update(menu);
            ShowState(context);
            await context.SaveChangesAsync();
        }

        var menu = await GetMenuAsync();
        menu.Price += 0.7m;

        await UpdateMenuAsync(menu);
    }


    public async Task DeleteDatabaseAsync()
    {
        using var context = _menusContextFactory.CreateDbContext();
        Console.Write("Delete the database? (y|n) ");
        string? input = Console.ReadLine();
        if (input?.ToLower() == "y")
        {
            bool deleted = await context.Database.EnsureDeletedAsync();
            string deletionInfo = deleted ? "deleted" : "not deleted";
            Console.WriteLine($"database {deletionInfo}");
        }
    }
}

