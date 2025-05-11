﻿using System.Net;
using System.Text.Json;
using Azure;
using Domain.Exceptions;
using Shared.ErrorModels;
namespace E_Commerce.Web.CustomMiddleWares
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleWare> _logger;

        public CustomExceptionHandlerMiddleWare(RequestDelegate Next, ILogger<CustomExceptionHandlerMiddleWare> logger)
        {
            _next = Next;
            this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {

                await _next.Invoke(httpContext);

                await HandleNotFoundEndPointAsync(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something Went Wrong");
                await HandleExceptionAsync(httpContext, ex);
            }

        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            //Set Status Code for Response
            //httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            //Set Content Type For Response

            httpContext.Response.ContentType = "application/json"; // no need if used WriteAsJsonAsync
            //Response Object
            var Response = new ErrorToReturn()
            {
                ErrorMessage = ex.Message
            };

            Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                BadRequestException badRequestException => GetBadRequestErrors(badRequestException,Response),
                _ => StatusCodes.Status500InternalServerError
            };
            //Return Object as JSON

            //var ResponseToReturn = JsonSerializer.Serialize(Response);

            //await httpContext.Response.WriteAsync(ResponseToReturn);

             httpContext.Response.StatusCode = Response.StatusCode;

            await httpContext.Response.WriteAsJsonAsync(Response);
        }

        private static int GetBadRequestErrors(BadRequestException badRequestException, ErrorToReturn? response)
        {
            response.Errors = badRequestException.Errors;
            return StatusCodes.Status400BadRequest;
        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var Response = new ErrorToReturn
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"End Point {httpContext.Request.Path} is Not Found",
                };
                await httpContext.Response.WriteAsJsonAsync(Response);
            }
        }
    }
}
