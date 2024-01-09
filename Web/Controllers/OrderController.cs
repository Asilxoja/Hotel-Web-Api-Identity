using Application.Common.Exceptions;
using Application.DTOs.OrderDtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(IOrderService orderService) : ControllerBase
{
    private readonly IOrderService _orderService = orderService;
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var orders = await _orderService.GetAll();
            return Ok(orders);
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
    public async Task<IActionResult> GetOrderById(int id)
    {
        try
        {
            var order = await _orderService.GetById(id);
            return Ok(order);
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
    public async Task<IActionResult> Add(AddOrderDto addOrderDto)
    {

        try
        {
            await _orderService.AddOrder(addOrderDto);
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
    public async Task<IActionResult> Update(UpdateOrderDto updateOrderDto)
    {
        try
        {
            await _orderService.UpdateOrder(updateOrderDto);
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
            await _orderService.DeleteOrder(id);
            return Ok("deleted");
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
