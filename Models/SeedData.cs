using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Student_CRUD.Data;
using System;
using System.Linq;
using Student_CRUD.Models;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using NuGet.Packaging.Signing;

namespace Student_CRUD.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new Student_CRUDContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<Student_CRUDContext>>()))
        {
            // Look for any movies.
            if (context.User.Any())
            {
                return;   // DB has been seeded
            }
            var Users = new User[]
            {
                new User
                {
                    Rollnumber = "TH2210002",
                    Image = "https://img.meta.com.vn/Data/image/2021/09/22/anh-meo-cute-de-thuong-dang-yeu-42.jpg",
                    Comment = "phutam@gmail.com",
                    Group = "T2210A",
                    isPresent = true,
                    FirstName = "Tam",
                    LastName = "Phu",
                },
                new User
                {
                    Rollnumber = "TH2302021",
                    Image = "https://img.meta.com.vn/Data/image/2021/09/22/anh-meo-cute-de-thuong-dang-yeu-42.jpg",
                    Comment = "tranthuy@gmail.com",
                    Group = "T2210A",
                    isPresent = true,
                    FirstName = "Thuy",
                    LastName = "Tran",
                },
                new User
                {
                    Rollnumber = "TH2102229",
                    Image = "https://img.meta.com.vn/Data/image/2021/09/22/anh-meo-cute-de-thuong-dang-yeu-42.jpg",
                    Comment = "peter@gmail.com",
                    Group = "T2210A",
                    isPresent = true,
                    FirstName = "Peter",
                    LastName = "Parker",
                },

            };
            foreach (User s in Users)
            {
                context.User.Add(s);
            }
            context.SaveChanges();
        }
    }
}