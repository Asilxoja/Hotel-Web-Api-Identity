using Application.Common.Exceptions;
using Application.Common.Helpers;
using Application.DTOs.RoomDtos.RoomStatusDtos;
using Application.DTOs.RoomsDtos.RoomStatusDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Rooms;
using Infrastructure.Interfaces;

namespace Application.Services;

public class RoomStatusServices(IUnitOfWork unitOfWork, IMapper mapper) : IRoomStatusService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    public async Task AddRoomStatus(AddRoomStatusDto addroomStatusDto)
    {
        if (addroomStatusDto == null)
        {
            throw new ArgumentNullException(nameof(addroomStatusDto));
        }
        var roomstatus = _mapper.Map<RoomStatus>(addroomStatusDto);
        var roomstatuss = await _unitOfWork.RoomStatusInterface.GetAllAsync();
        if (roomstatus == null)
        {
            throw new ArgumentNullException(nameof(addroomStatusDto));
        }
        if (roomstatus.IsExist(roomstatuss))
        {
            throw new CustomException($"{roomstatus.Name} - room already added");
        }
        try
        {
            await _unitOfWork.RoomStatusInterface.AddAsync(roomstatus);
            await _unitOfWork.SaveAsync();
        }
        catch (CustomException ex)
        {
            throw new Exception(ex.Message);
        }
        catch (ArgumentNullException ex)
        {
            throw new Exception(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task DeleteRoomStatus(int Id)
    {
        var roomstat = _unitOfWork.RoomStatusInterface.GetByIdAsync(Id);
        var roomstatus = await roomstat;
        if (!roomstatus.IsValid())
        {
            throw new CustomException($"{nameof(roomstatus.Id)} cannot be deleted");
        }
        await _unitOfWork.RoomStatusInterface.DeleteAsync(roomstatus);
        await _unitOfWork.SaveAsync();
    }

    public async Task<List<RoomStatusDto>> GetAllRoomStatus()
    {
        var roomstatus = await _unitOfWork.RoomStatusInterface.GetAllAsync();
        return roomstatus.Select(r => _mapper.Map<RoomStatusDto>(r)).ToList();
    }

    public async Task<RoomStatusDto> GetRoomStatusById(int Id)
    {
        var roomstatus = await _unitOfWork.RoomStatusInterface.GetByIdAsync(Id);
        if (roomstatus is null)
        {
            throw new ArgumentNullException(nameof(roomstatus));
        }
        return _mapper.Map<RoomStatusDto>(roomstatus);
    }

    public async Task UpdateRoomStatus(UpdateRoomStatusDto roomStatusDto)
    {
        if (roomStatusDto is null)
        {
            throw new ArgumentNullException(nameof(roomStatusDto));
        }
        var roomstatus = await _unitOfWork.RoomStatusInterface.GetByIdAsync(roomStatusDto.Id);
        if (roomstatus is null)
        {
            throw new ArgumentNullException(nameof(roomstatus));
        }
        _mapper.Map(roomStatusDto, roomstatus);
        if (!roomstatus.IsValid())
        {
            throw new CustomException("Guest information is invalid!");
        }
        await _unitOfWork.RoomStatusInterface.UpdateAsync(roomstatus);
        await _unitOfWork.SaveAsync();
    }
}
