using System.Threading;
using Emi.Employees.Application.Abstraction.Request;
using Emi.Employees.Application.Common;
using Emi.Employees.Application.Modules.GetEmployees;
using Emi.Employees.Application.Modules.ManageEmployees;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Emi.Employees.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController(
        ISender sender
        ) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IResult> Get(int? id, CancellationToken cancellationToken)
        {
            var result = await sender.Send(
                new GetEmployeesCommand
                { 
                    EmployeeId = id
                }, cancellationToken);
            return result.Match(Results.Ok, _ => Results.BadRequest(result.Errors));
        }

        [HttpPost]
        public async Task<IResult> Insert([FromBody] EmployeeRequest employeeRequest, CancellationToken cancellationToken)
        {
            var result = await sender.Send(
                new ManageEmployeesCommand
                {
                    Employee = employeeRequest,
                    OperationType = OperationType.Insert
                }, cancellationToken);
            return result.Match(Results.Ok, _ => Results.BadRequest(result.Errors));
        }

        [HttpPut("{id}")]
        public async Task<IResult> Update(int id, [FromBody] EmployeeRequest employeeRequest, CancellationToken cancellationToken)
        {
            var result = await sender.Send(
                new ManageEmployeesCommand
                {
                    Employee = employeeRequest,
                    EmployeeId = id,
                    OperationType = OperationType.Update
                }, cancellationToken);
            return result.Match(Results.Ok, _ => Results.BadRequest(result.Errors));
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await sender.Send(
                new ManageEmployeesCommand
                {
                    EmployeeId = id,
                    OperationType = OperationType.Delete
                }, cancellationToken);
            return result.Match(Results.Ok, _ => Results.BadRequest(result.Errors));
        }
    }
}
