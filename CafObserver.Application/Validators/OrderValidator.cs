using CafObserver.Application.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafObserver.Application.Validators
{
    public class OrderCreationModelValidator : AbstractValidator<OrderCreationModel>
    {
        public OrderCreationModelValidator()
        {
            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("Customer name is required")
                .MaximumLength(100).WithMessage("Customer name cannot exceed 100 characters");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Order must have at least one item");

            RuleForEach(x => x.Items).SetValidator(new OrderItemCreationModelValidator());
        }
    }

    public class OrderItemCreationModelValidator : AbstractValidator<OrderItemCreationModel>
    {
        public OrderItemCreationModelValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("Product ID must be greater than 0");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0")
                .LessThanOrEqualTo(10).WithMessage("Quantity cannot exceed 10");
        }
    }
}
