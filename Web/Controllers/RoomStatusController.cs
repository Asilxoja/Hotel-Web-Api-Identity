using Application.Common.Exceptions;
using Application.DTOs.RoomDtos.RoomStatusDtos;
using Application.DTOs.RoomsDtos.RoomStatusDtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoomStatusController(IRoomStatusService roomStatusService) : ControllerBase
{
    private readonly IRoomStatusService _roomStatusService = roomStatusService;
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var roomstatus = await _roomStatusService.GetAllRoomStatus();
            return Ok(roomstatus);
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
            var roomstatus = await _roomStatusService.GetRoomStatusById(id);
            return Ok(roomstatus);
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
    public async Task<IActionResult> Add(AddRoomStatusDto addRoomStatusDto)
    {
        try
        {
            await _roomStatusService.AddRoomStatus(addRoomStatusDto);
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
    public async Task<IActionResult> Update(UpdateRoomStatusDto updateRoomStatusDto)
    {
        try
        {
            await _roomStatusService.UpdateRoomStatus(updateRoomStatusDto);
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
            await _roomStatusService.DeleteRoomStatus(id);
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