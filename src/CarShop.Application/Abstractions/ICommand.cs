﻿using CarShop.Application.Common;
using MediatR;

namespace CarShop.Application.Abstractions;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
