namespace Contonso.SampleApi.Web.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;

public class BaseController : Controller
{
    private ISender mediator = null!;

    protected ISender Mediator =>
        this.mediator ??= this.HttpContext.RequestServices.GetRequiredService<ISender>();
}