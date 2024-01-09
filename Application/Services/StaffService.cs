using Application.Common.Exceptions;
using Application.Common;
using Application.DTOs.StaffDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Staffs;
using Infrastructure.Interfaces;
using Application.Common.Helpers;

namespace Application.Services;

public class StaffService(IUnitOfWork unitOfWork, IMapper mapper) : IStaffService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task AddAsync(AddStaffDto staffDtoDto)
    {
        if (staffDtoDto is null)
        {
            throw new ArgumentNullException("Staf is null\n\n", nameof(staffDtoDto));
        }
        var staff = _mapper.Map<Staff>(staffDtoDto);

        try
        {
            await _unitOfWork.StaffInterface.AddAsync(staff);
            await _unitOfWork.SaveAsync();
        }
        catch (CustomException ex)
        {
            throw new Exception(ex.Message);
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task DeleteAsync(int id)
    {
        var staff = await _unitOfWork.StaffInterface.GetByIdAsync(id);
        if (!staff.IsValid())
        {
            throw new CustomException($"{nameof(Staff)} cannot be deleted");
        }
        await _unitOfWork.StaffInterface.DeleteAsync(staff);
        await _unitOfWork.SaveAsync();
    }

    public async Task<List<StaffDto>> GetAllAsync()
    {
        var staffs = await _unitOfWork.StaffInterface.GetAllAsync();
        return staffs.Select(e => _mapper.Map<StaffDto>(e))
                         .ToList();
    }

    public async Task<PagedList<StaffDto>> GetAllPagedAsync(int pageSize, int pageNumber)
    {
        var staffs = await GetAllAsync();
        PagedList<StaffDto> pagedList = new(staffs, staffs.Count, pageNumber, pageSize);
        return pagedList.ToPagedList(staffs,
                                      pageSize,
                                      pageNumber);
    }

    public async Task<StaffDto> GetByIdAsync(int id)
    {
        var staff = await _unitOfWork.StaffInterface.GetByIdAsync(id);
        if (staff is null)
        {
            throw new ArgumentException("staff is not");
        }
        return _mapper.Map<StaffDto>(staff);
    }

    public async Task UpdateAsync(UpdateStaffDto updatedStaffDto)
    {
        if (updatedStaffDto is null)
        {
            throw new ArgumentNullException(nameof(updatedStaffDto), "Updated staff information is null");
        }

        var staff = await _unitOfWork.StaffInterface.GetByIdAsync(updatedStaffDto.Id);

        if (staff is null)
        {
            throw new ArgumentException($"No staff found with ID {updatedStaffDto.Id}", nameof(updatedStaffDto.Id));
        }

        _mapper.Map(updatedStaffDto, staff);

        if (!staff.IsValid())
        {
            throw new CustomException("Staff information is invalid!");
        }

        await _unitOfWork.StaffInterface.UpdateAsync(staff);
        await _unitOfWork.SaveAsync();
    }
}
