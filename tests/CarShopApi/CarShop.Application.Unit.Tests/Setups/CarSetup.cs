﻿using CarShop.Core.Presistence;
using CarShop.Shared.Repositories.Interfaces;
using Moq;

namespace CarShop.Application.Unit.Tests.Setups;

public class CarSetup
{
    public readonly Mock<IUnitOfWork> MockUnitOfWork;
    public readonly Mock<ICarRepository> MockCarRepository;

    public CarSetup()
    {
        MockUnitOfWork = new Mock<IUnitOfWork>();
        MockCarRepository = new Mock<ICarRepository>();
        MockUnitOfWork.Setup(u => u.Cars).Returns(MockCarRepository.Object);
    }
}
