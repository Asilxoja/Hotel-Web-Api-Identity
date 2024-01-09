using Application.Common;
using Application.DTOs.GuestDtos;

namespace Application.Interfaces;

public interface IGuestService
{
    Task<List<GuestDto>> GetAllAsync();
    Task<GuestDto> GetByIdAsync(int id);
    Task<PagedList<GuestDto>> GetAllPagedAsync(int pageSize, int pageNumber);
    Task AddAsync(AddGuestDto GuestDto);
    Task UpdateAsync(UpdateGuestDto updatedGuestDto);
    Task DeleteAsync(int id);
}

