using Domain.Abstractions;
using Domain.Aggregates.ClienteAggregate;
using Domain.Aggregates.VentaAggregate;

namespace Domain.DomainEvents;

public sealed record VentaClienteAssociatedDomainEvent(
    Venta Venta, 
    Cliente Cliente) : IDomainEvent;
