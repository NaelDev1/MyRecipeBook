﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using System.Net;

namespace MyRecipeBook.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is MyRecipeBookException)
            HandleProjectException(context);
        else
            ThrowUnknowException(context);
    
    }

    private static void HandleProjectException(ExceptionContext context)
    {
        if(context.Exception is ErrorOnValidationException erro)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result =  new BadRequestObjectResult( new ResponseErrorJson(erro.ErrorMessages));
        }
    }

    private static void ThrowUnknowException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessageExceptions.UNKNOW_ERROR));
    }

}
