using Application.Common.Exceptions;
using Application.Common.Helpers;
using Application.DTOs.RoomsDtos.RoomTypeDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Rooms;
using Infrastructure.Interfaces;

namespace Application.Services;

public class RoomTypeService(IUnitOfWork unitOfWork, IMapper mapper) : IRoomTypeService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task AddRoomType(AddRoomTypeDto addRoomTypeDto)
    {
        if (addRoomTypeDto == null)
        {
            throw new ArgumentNullException(nameof(addRoomTypeDto));
        }
        var roomtype = _mapper.Map<RoomType>(addRoomTypeDto);
        var roomtypes = await _unitOfWork.RoomTypeInterface.GetAllAsync();
        if (roomtypes == null)
        {
            throw new ArgumentNullException(nameof(addRoomTypeDto));
        }
        if (roomtype.IsExist(roomtypes))
        {
            throw new CustomException($"{roomtype.Name} - room already added");
        }
        try
        {
            await _unitOfWork.RoomTypeInterface.AddAsync(roomtype);
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

    public async Task DeleteRoomType(int id)
    {
        var roomTask = _unitOfWork.RoomTypeInterface.GetByIdAsync(id);
        var roomtype = await roomTask;
        //if (!roomtype.IsValid())
        //{
        //    throw new CustomException($"{nameof(roomtype.Id)} cannot be deleted");
        //}
        await _unitOfWork.RoomTypeInterface.DeleteAsync(roomtype);
        await _unitOfWork.SaveAsync();
    }

    public async Task<List<RoomTypeDto>> GetAll()
    {
        var roomtypes = await _unitOfWork.RoomTypeInterface.GetAllAsync();
        return roomtypes.Select(r => _mapper.Map<RoomTypeDto>(r)).ToList();
    }

    public async Task<RoomTypeDto> GetById(int id)
    {
        var roomtype = await _unitOfWork.RoomTypeInterface.GetByIdAsync(id);
        if (roomtype is null)
        {
            throw new ArgumentNullException(nameof(roomtype));
        }
        return _mapper.Map<RoomTypeDto>(roomtype);
    }

    public async Task UpdateRoomType(UpdateRoomTypeDto updateRoomTypeDto)
    {
        if (updateRoomTypeDto is null)
        {
            throw new ArgumentNullException(nameof(updateRoomTypeDto));
        }
        var roomtype = await _unitOfWork.RoomTypeInterface.GetByIdAsync(updateRoomTypeDto.Id);
        if (roomtype is null)
        {
            throw new ArgumentNullException(nameof(roomtype));
        }
        _mapper.Map(updateRoomTypeDto, roomtype);
        //if (!roomtype.IsValid())
        //{
        //    throw new CustomException("Guest information is invalid!");
        //}
        await _unitOfWork.RoomTypeInterface.UpdateAsync(roomtype);
        await _unitOfWork.SaveAsync();
    }
}
