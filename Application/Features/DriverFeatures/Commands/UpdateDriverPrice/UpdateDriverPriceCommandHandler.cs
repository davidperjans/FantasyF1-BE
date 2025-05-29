using Application.Common;
using Application.Features.DriverFeatures.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.CoreEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DriverFeatures.Commands.UpdateDriverPrice
{
    public class UpdateDriverPriceCommandHandler : IRequestHandler<UpdateDriverPriceCommand, OperationResult<DriverDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateDriverPriceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationResult<DriverDto>> Handle(UpdateDriverPriceCommand request, CancellationToken cancellationToken)
        {
            if (request.NewPrice <= 0)
                return OperationResult<DriverDto>.Failure("Price must be greater than 0.");

            var driverRepo = _unitOfWork.Repository<Driver>();
            var driver = await driverRepo.GetByIdAsync(request.DriverId);

            if (driver is null)
                return OperationResult<DriverDto>.Failure("Driver not found.");

            driver.CurrentPrice = request.NewPrice;

            driverRepo.Update(driver);
            await _unitOfWork.SaveChangesAsync();

            var dto = _mapper.Map<DriverDto>(driver);
            return OperationResult<DriverDto>.Success(dto);
        }
    }
}
