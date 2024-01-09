using Application.Common.Exceptions;
using Application.DTOs.RoomsDtos.RoomTypeDtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoomTypeController(IRoomTypeService roomTypeService) : ControllerBase
{
    private readonly IRoomTypeService _roomTypeService = roomTypeService;
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var roomtypes = await _roomTypeService.GetAll();
            return Ok(roomtypes);
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
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var roomtype = await _roomTypeService.GetById(id);
            return Ok(roomtype);
        }
        catch (CustomException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentNullException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost]
    public async Task<IActionResult> Add(AddRoomTypeDto addRoomTypeDto)
    {
        try
        {
            await _roomTypeService.AddRoomType(addRoomTypeDto);
            return Ok("Added");
        }
        catch (CustomException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentNullException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPut]
    public async Task<IActionResult> Update(UpdateRoomTypeDto updateRoomTypeDto)
    {
        try
        {
            await _roomTypeService.UpdateRoomType(updateRoomTypeDto);
            return Ok("Updated");
        }
        catch (CustomException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentNullException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _roomTypeService.DeleteRoomType(id);
            return Ok("Deleted");
        }
        catch (CustomException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentNullException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}