﻿using ShippingService.Email.Core.Common;

namespace ShippingService.Email.Core.Extensions;

public static class OperationResultExtensions
{
    public static void EnsureSuccess(this OperationResult operationResult)
    {
        if (operationResult.IsFail)
            throw new Exception($"Expected Ok result, but was: {operationResult.Error}");
    }
}