using Application.Common;
using Application.DTOs.StaffDtos;

namespace Application.Interfaces;
public interface IStaffService
{
    Task<List<StaffDto>> GetAllAsync();
    Task<PagedList<StaffDto>> GetAllPagedAsync(int pageSize, int pageNumber);
    Task<StaffDto> GetByIdAsync(int id);
    Task AddAsync(AddStaffDto staffDtoDto);
    Task UpdateAsync(UpdateStaffDto updatedStaffDtoDto);
    Task DeleteAsync(int id);
}
