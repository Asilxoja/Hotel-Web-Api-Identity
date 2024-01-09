using Application.Common.Exceptions;
using Application.Common;
using Application.DTOs.GuestDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Guests;
using Infrastructure.Interfaces;
using Application.Common.Helpers;

namespace Application.Services;

public class GuestService(IUnitOfWork unitOfWork, IMapper mapper) : IGuestService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task AddAsync(AddGuestDto guestDto)
    {
        if (guestDto == null)
        {
            throw new ArgumentNullException(nameof(guestDto));
        }

        var guest = _mapper.Map<Guest>(guestDto);
        var guests = await _unitOfWork.GuestInterface.GetAllAsync();

        if (guest.IsExist(guests))
        {
            throw new CustomException($"This {guest.FirstName} {guest.LastName} already exist.");
        }
        try
        { 
                await _unitOfWork.GuestInterface.AddAsync(guest);
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
        var guestTask = _unitOfWork.GuestInterface.GetByIdAsync(id);
        var guest = await guestTask;
        if (!guest.IsValid())
        {
            throw new CustomException($"{nameof(Guest)} cannot be deleted");
        }
        await _unitOfWork.GuestInterface.DeleteAsync(guest);
        await _unitOfWork.SaveAsync();
    }

    public async Task<List<GuestDto>> GetAllAsync()
    {
        var guests = await _unitOfWork.GuestInterface.GetAllAsync();
        return guests.Select(g => _mapper.Map<GuestDto>(g))
                         .ToList();
    }

    public async Task<PagedList<GuestDto>> GetAllPagedAsync(int pageSize, int pageNumber)
    {
        var guests = await GetAllAsync();
        PagedList<GuestDto> pagedList = new(guests, guests.Count, pageNumber, pageSize);
        return pagedList.ToPagedList(guests,
                                      pageSize,
                                      pageNumber);
    }

    public async Task<GuestDto> GetByIdAsync(int id)
    {
        var guest = await _unitOfWork.GuestInterface.GetByIdAsync(id);
        if (guest is null)
        {
            throw new ArgumentException("Education is not");
        }
        return _mapper.Map<GuestDto>(guest);
    }

    public async Task UpdateAsync(UpdateGuestDto updatedGuestDto)
    {
        if (updatedGuestDto is null)
        {
            throw new ArgumentNullException(nameof(updatedGuestDto), "Updated guest information is null");
        }

        var guest = await _unitOfWork.GuestInterface.GetByIdAsync(updatedGuestDto.Id);
        var guests = await _unitOfWork.GuestInterface.GetAllAsync();

        if (guest.IsExist(guests))
        {
            throw new CustomException($"This {guest.FirstName} {guest.LastName} already exist.");
        }

        if (guest is null)
        {
            throw new ArgumentException($"No guest found with ID {updatedGuestDto.Id}", nameof(updatedGuestDto.Id));
        }

        _mapper.Map(updatedGuestDto, guest);

        if (!guest.IsValid())
        {
            throw new CustomException("Guest information is invalid!");
        }

        await _unitOfWork.GuestInterface.UpdateAsync(guest);
        await _unitOfWork.SaveAsync();
    }
}