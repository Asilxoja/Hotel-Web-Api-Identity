using Application.Common.Exceptions;
using Application.Common;
using Application.DTOs.RoomsDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Rooms;
using Infrastructure.Interfaces;
using Application.Common.Helpers;

namespace Application.Services;

public class RoomService(IUnitOfWork unitOfWork, IMapper mapper) : IRoomService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task AddAsync(AddRoomDto roomDto)
    {
        if (roomDto is null)
        {
            throw new ArgumentNullException(nameof(roomDto));
        }
        var room = _mapper.Map<Room>(roomDto);
        var rooms = await _unitOfWork.RoomInterface.GetAllAsync();

        if (room.IsExist(rooms))
        {
            throw new CustomException($"{room.Number} - room already added");
        }
        try
        {
            await _unitOfWork.RoomInterface.AddAsync(room);
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
        var roomTask = _unitOfWork.RoomInterface.GetByIdAsync(id);
        var room = await roomTask;

        if (!room.IsValid())
        {
            throw new CustomException($"{nameof(room.Id)} cannot be deleted");
        }
        await _unitOfWork.RoomInterface.DeleteAsync(room);
        await _unitOfWork.SaveAsync();
    }

    public async Task<List<RoomDto>> GetAllAsync()
    {
        var rooms = await _unitOfWork.RoomInterface.GetAllAsync();
        return rooms.Select(g => _mapper.Map<RoomDto>(g))
                         .ToList();
    }

    public async Task<PagedList<RoomDto>> GetAllPagedAsync(int pageSize, int pageNumber)
    {
        var rooms = await GetAllAsync();
        PagedList<RoomDto> pagedList = new(rooms, rooms.Count, pageNumber, pageSize);
        return pagedList.ToPagedList(rooms,
                                      pageSize,
                                      pageNumber);
    }

    public async Task<RoomDto> GetByIdAsync(int id)
    {
        var room = await _unitOfWork.RoomInterface.GetByIdAsync(id);
        if (room is null)
        {
            throw new ArgumentException("room is null");
        }
        return _mapper.Map<RoomDto>(room);
    }

    public async Task UpdateAsync(UpdateRoomDto updateRoomDto)
    {
        if (updateRoomDto is null)
        {
            throw new ArgumentNullException(nameof(updateRoomDto), "Updated guest information is null");
        }

        var room = await _unitOfWork.RoomInterface.GetByIdAsync(updateRoomDto.Id);

        if (room is null)
        {
            throw new ArgumentException($"No guest found with ID {updateRoomDto.Id}", nameof(updateRoomDto.Id));
        }

        _mapper.Map(updateRoomDto, room);

        if (!room.IsValid())
        {
            throw new CustomException("Guest information is invalid!");
        }

        await _unitOfWork.RoomInterface.UpdateAsync(room);
        await _unitOfWork.SaveAsync();
    }
}