using Microsoft.AspNetCore.Mvc;
using MovementHomeAssignment.DTOs;
using MovementHomeAssignment.Interfaces;

namespace MovementHomeAssignment.Controllers;

[ApiController]
[Route("[controller]")]
public class DataController : ControllerBase
{
    private readonly IDataService _dataService;

    public DataController(IDataService dataService)
    {
        _dataService = dataService;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DataDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(string id)
    {
        var data = await _dataService.GetDataByIdAsync(id);
        return data != null ? Ok(data) : NotFound();
    }

    [HttpPost]
    [ProducesResponseType(typeof(DataDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] CreateDataDto createDataDto)
    {
        var newData = await _dataService.CreateDataAsync(createDataDto);
        return CreatedAtAction(nameof(Get), new { id = newData.Id }, newData);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(DataDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(string id, [FromBody] UpdateDataDto updateDataDto)
    {
        var updatedData = await _dataService.UpdateDataAsync(id, updateDataDto);
        return updatedData != null ? Ok(updatedData) : NotFound();
    }
}
