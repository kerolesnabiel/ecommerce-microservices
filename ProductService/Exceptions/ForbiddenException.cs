﻿namespace ProductService.Exceptions;

public class ForbiddenException() : Exception("You do not have permission to access this resource")
{
}
