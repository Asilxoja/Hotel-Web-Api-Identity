using Application.Common.Exceptions;
using Application.DTOs.OrderDtos.OrderStatusDtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderStatusController(IOrderStatusService orderStatus) : ControllerBase
{
    private readonly IOrderStatusService _orderStatusService = orderStatus;
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var orderstatus = await _orderStatusService.GetAllAsync();
            return Ok(orderstatus);
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
            var orderstatus = await _orderStatusService.GetByIdAsync(id);
            return Ok(orderstatus);
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
    [HttpPost]
    public async Task<IActionResult> Add(AddOrderStatusDto addOrderStatusDto)
    {
        try
        {
            await _orderStatusService.Add(addOrderStatusDto);
            return Ok("Added");
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
    [HttpPut]
    public async Task<IActionResult> Update(UpdateOrderStatusDto orderStatus)
    {
        try
        {
            await _orderStatusService.Update(orderStatus);
            return Ok("Updated");
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
            await _orderStatusService.Delete(id);
            return Ok("Deleted");
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
}
