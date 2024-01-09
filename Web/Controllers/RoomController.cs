using Application.Common.Exceptions;
using Application.DTOs.RoomsDtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoomController(IRoomService roomService) : ControllerBase
{
    private readonly IRoomService _roomService = roomService;

    [HttpGet("Get-all")]
    public async Task<IActionResult> Get()
    {
        try
        {
            var rooms = await _roomService.GetAllAsync();
            return Ok(rooms);
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
            var room = await _roomService.GetByIdAsync(id);
            return Ok(room);
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
    public async Task<IActionResult> Add(AddRoomDto roomDto)
    {
        try
        {
            await _roomService.AddAsync(roomDto);
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
            var rooms = await _roomService.GetAllPagedAsync(pageSize, pageNumber);
            return Ok(rooms);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateRoomDto dto)
    {
        try
        {
            await _roomService.UpdateAsync(dto);
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
            await _roomService.DeleteAsync(id);
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

