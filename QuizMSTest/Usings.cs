global using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using QuizAPiService.Entities;

public class InMemoryDbContextFactory
{
    public QuizContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<QuizContext>()
                        .UseInMemoryDatabase(databaseName: "InMemoryQuizDatabase")
                        .Options;
        var dbContext = new QuizContext(options);

        return dbContext;
    }
}