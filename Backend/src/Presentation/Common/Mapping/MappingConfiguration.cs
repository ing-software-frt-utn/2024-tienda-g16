using Application.AppServices.Authentication.Commands.Login;
using Application.AppServices.Ventas;
using Application.AppServices.Ventas.Queries.BuscarArticulo;
using Application.DTOs.Ventas;
using Domain.Aggregates.VentaAggregate;
using Mapster;
using Presentation.Responses.BuscarArticulo;
using Presentation.Responses.Login;
using Presentation.Responses.Ventas;

namespace Presentation.Common.Mapping;

public class MappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<BuscarArticuloResult, BuscarArticuloResponse>()
            .Map(dest => dest.Stocks, src => src.Stocks)
            .Map(dest => dest, src => src.Articulo);

        config.NewConfig<VentaResult, VentaResponse>()
            .Map(dest => dest, src => src.Venta);

        config.NewConfig<LoginResult, LoginResponse>()
            .Map(dest => dest.User, src => src.User)
            .Map(dest => dest.Sesion, src => src.Sesion)
            .Map(dest => dest.Token, src => src.Token);
    }
}
