using Application.Common.Exceptions;
using Application.DTOs.StaffDtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StaffController(IStaffService staffService) : ControllerBase
{
    private readonly IStaffService _staffService = staffService;

    [HttpGet("Get-all")]
    public async Task<IActionResult> Get()
    {
        try
        {
            var staffs = await _staffService.GetAllAsync();
            return Ok(staffs);
        }
        catch (CustomException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var staff = await _staffService.GetByIdAsync(id);
            return Ok(staff);
        }
        catch (ArgumentNullException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddStaff(AddStaffDto staffDto)
    {
        try
        {
            await _staffService.AddAsync(staffDto);
            return Ok("added");
        }
        catch (ArgumentNullException ex)
        {
            return NotFound(ex.Message);
        }

        catch (CustomException ex)
        {
            return BadRequest(ex.Message);
        }

        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("paged")]
    public async Task<IActionResult> Get(int pageSize = 10, int pageNumber = 1)
    {
        try
        {
            var staffs = await _staffService.GetAllPagedAsync(pageSize, pageNumber);
            return Ok(staffs);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateStaffDto dto)
    {
        try
        {
            await _staffService.UpdateAsync(dto);
            return Ok("updated");
        }
        catch (ArgumentNullException ex)
        {
            return NotFound(ex.Message);
        }
        catch (CustomException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _staffService.DeleteAsync(id);
            return Ok("deleted");
        }
        catch (ArgumentNullException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
