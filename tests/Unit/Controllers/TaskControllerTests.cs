using Xunit;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Models;
using TaskTracker.Data;
using TaskTracker.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace TaskTracker.Tests.Unit.Controllers;

public class TasksControllerTests : IDisposable
{
    private readonly TaskContext _context;
    private readonly TasksController _controller;

    public void Dispose()
    {
        _context.Dispose();
    }

    public TasksControllerTests()
    {
        var options = new DbContextOptionsBuilder<TaskContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new TaskContext(options);
        _controller = new TasksController(_context);

        _context.Tasks.AddRange(
            new TaskItem { Id = 1, Name = "Task 1", IsCompleted = false },
            new TaskItem { Id = 2, Name = "Task 2", IsCompleted = true }
        );
        _context.SaveChanges();
    }

    [Fact]
    public async Task GetTasks_ReturnsAllTasks()
    {
        var result = await _controller.GetTasks();

        Assert.Equal(2, result.Value.Count());
        Assert.Equal("Task 1", result.Value.First().Name);
    }

    [Fact]
    public async Task GetTaskItem_ReturnsTaskItem_WhenItExists()
    {
        var result = await _controller.GetTaskItem(1);

        Assert.Equal("Task 1", result.Value.Name);
    }

    [Fact]
    public async Task GetTaskItem_ReturnsNotFound_WhenItDoesNotExist()
    {
        var actionResult = await _controller.GetTaskItem(3);

        if (actionResult.Result is NotFoundResult)
            Assert.True(true);
        else
            Assert.Fail($"Unexpected result type: {actionResult.Result?.GetType().Name ?? "null"}");
    }

    [Fact]
    public async Task PutTaskItem_ReturnsBadRequest_WhenIdsDoNotMatch()
    {
        var result = await _controller.PutTaskItem(1, new TaskItem { Id = 2, Name = "Task 2", IsCompleted = true });

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task PutTaskItem_ReturnsNotFound_WhenTaskItemDoesNotExist()
    {
        var result = await _controller.PutTaskItem(4, new TaskItem { Id = 4, Name = "Task 4", IsCompleted = false });

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task PutTaskItem_ReturnsNoContent_WhenSuccessful()
    {
        var taskToUpdate = await _context.Tasks.FindAsync(1);
        taskToUpdate.Name = "Updated Task 1";
        taskToUpdate.IsCompleted = true;

        var result = await _controller.PutTaskItem(1, taskToUpdate);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task PostTaskItem_ReturnsCreatedResponse_WhenSuccessful()
    {
        var result = await _controller.PostTaskItem(new TaskItem { Id = 3, Name = "Task 3", IsCompleted = false });

        Assert.IsType<CreatedAtActionResult>(result.Result);
    }

    [Fact]
    public async Task PostTaskItem_ReturnsBadRequest_WhenTaskItemIsNull()
    {
        var result = await _controller.PostTaskItem(null);

        if (result.Result is BadRequestObjectResult badRequestObjectResult)
            Assert.True(true, "Correctly received BadRequestObjectResult");
        else if (result.Result is ObjectResult objectResult)
            Assert.Fail(
                $"Expected BadRequestObjectResult, but received {objectResult.GetType().Name} with message: {objectResult.Value}");
        else
            Assert.Fail($"Unexpected result type: {result.Result?.GetType().Name ?? "null"}");
    }


    [Fact]
    public async Task DeleteTaskItem_ReturnsNotFound_WhenTaskItemDoesNotExist()
    {
        var actionResult = await _controller.DeleteTaskItem(0);

        if (actionResult is NotFoundResult)
            Assert.True(true);
        else
            Assert.Fail("Expected NotFoundResult");
    }

    [Fact]
    public async Task DeleteTaskItem_ReturnsNoContent_WhenSuccessful()
    {
        var actionResult = await _controller.DeleteTaskItem(1);

        if (actionResult is NoContentResult)
            Assert.True(true);
        else
            Assert.Fail("Expected NoContentResult");
    }

    [Fact]
    public async Task DeleteTaskItem_RemovesOneItem_WhenSuccessful()
    {
        await _controller.DeleteTaskItem(1);

        Assert.Equal(1, _context.Tasks.Count());
    }

    [Fact]
    public async Task DeleteTaskItem_RemovesCorrectItem_WhenSuccessful()
    {
        await _controller.DeleteTaskItem(1);

        Assert.Equal(2, _context.Tasks.First().Id);
    }
}