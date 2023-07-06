using Microsoft.EntityFrameworkCore;
using CardComSite1.Data;
using System.Text.RegularExpressions;
using System;

namespace CardComSite1;

public static class PersonEndpoints
{
    public static void MapPersonEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Person", async (CardComSite1Context db) =>
        {
            return await db.Person!.ToListAsync();
        })
        .WithName("GetAllPersons")
        .Produces<List<Person>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Person/{id}", async (int Id, CardComSite1Context db) =>
        {
            return await db.Person!.FindAsync(Id)
                is Person model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetPersonById")
        .Produces<Person>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Person/{id}", async (int Id, Person person, CardComSite1Context db) =>
        {
            var r1 = new Regex(@"[0-9]+");
            if (!r1.IsMatch(person.citizenId))
            {
                return Results.BadRequest(person.citizenId);
            }

            if (!String.IsNullOrEmpty(person.phone) && !r1.IsMatch(person.phone))
            {
                return Results.BadRequest(person.phone);
            }

            var r2 = new Regex(@"([a-z0-9._%+\-]+@[a-z0-9.\-]+\.[a-z]{2,}$)?");
            if (person.email != null && !r2.IsMatch(person.email))
            {
                return Results.BadRequest(person.email);
            }

            var foundModel = await db.Person!.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Entry(foundModel).State = EntityState.Detached;
            db.Set<Person>().Update(person);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdatePerson")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Person/", async (Person person, CardComSite1Context db) =>
        {
            var r1 = new Regex(@"[0-9]+");
            if (!r1.IsMatch(person.citizenId))
            {
                return Results.BadRequest(person.citizenId);
            }

            if (!String.IsNullOrEmpty(person.phone) && !r1.IsMatch(person.phone))
            {
                return Results.BadRequest(person.phone);
            }

            var r2 = new Regex(@"([a-z0-9._%+\-]+@[a-z0-9.\-]+\.[a-z]{2,}$)?");
            if (person.email != null && !r2.IsMatch(person.email))
            {
                return Results.BadRequest(person.email);
            }

            db.Person!.Add(person);
            await db.SaveChangesAsync();
            return Results.Created($"/Persons/{person.Id}", person);
        })
        .WithName("CreatePerson")
        .Produces<Person>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Person/{id}", async (int Id, CardComSite1Context db) =>
        {
            if (await db.Person!.FindAsync(Id) is Person person)
            {
                db.Person.Remove(person);
                await db.SaveChangesAsync();
                return Results.Ok(person);
            }

            return Results.NotFound();
        })
        .WithName("DeletePerson")
        .Produces<Person>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
