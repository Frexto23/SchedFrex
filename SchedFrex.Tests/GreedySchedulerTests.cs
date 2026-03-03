using AutoFixture;
using SchedFrex.Application.Logic.SchedulerStrategies;
using SchedFrex.Core.Models;

namespace SchedFrex.Tests;

public class GreedySchedulerTests
{
    private readonly Fixture _fixture = new();
    
    [Fact]
    public void
        Schedule_Two_Problems_In_Calendar_Which_Contains_Copies_Of_This_Problems_Should_Return_One_Entry_With_No_Intersections()
    {
        // Arrange
        var calendarEntries = new List<Entry>
        {
            new Entry(Guid.NewGuid(), "Отчет по проекту", 
                            new TimeInterval(DateTime.Today.AddHours(10), DateTime.Today.AddHours(12))),
            new Entry(Guid.NewGuid(), "Погулять с собачкой!", 
                new TimeInterval(
                    DateTime.Today.AddHours(18),
                    DateTime.Today.AddHours(18).AddMinutes(45))),
        };
        
        var calendar = new Calendar(Guid.NewGuid(), calendarEntries);

        var problems = new List<Problem>
        {
            new Problem(
                Guid.NewGuid(),
                "Погулять с собачкой!",
                1,
                new TimeSpan(0, 45, 0),
                DateTime.Today.AddHours(100),
                Guid.NewGuid(),
                [
                    new TimeInterval(DateTime.Today.AddHours(18), DateTime.Today.AddHours(18).AddMinutes(45))
                ]
            ),
            new Problem(
                Guid.NewGuid(),
                "Отчет по проекту",
                2,
                new TimeSpan(2, 0, 0),
                DateTime.Today.AddHours(100),
                Guid.NewGuid(),
                [
                    new TimeInterval(DateTime.Today.AddHours(10), DateTime.Today.AddHours(12)),
                    new TimeInterval(DateTime.Today.AddHours(14), DateTime.Today.AddHours(16)),
                    new TimeInterval(DateTime.Today.AddHours(18), DateTime.Today.AddHours(20))
                ]
            )
        };
        
        var scheduler = new GreedyScheduler();

        // Act
        var schedule = scheduler.Schedule(problems, calendar, new TimeInterval(DateTime.Today.AddHours(10), DateTime.Today.AddHours(100)));

        // Assert
        Assert.Single(schedule);
        Assert.Equal("Отчет по проекту", schedule[0].Title);
        Assert.Equal(new TimeInterval(DateTime.Today.AddHours(12), DateTime.Today.AddHours(14)), schedule[0].Slot);
    }
}