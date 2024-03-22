using Application.Abstractions.Messaging;
using Application.DTOs.Articulos;
using Application.DTOs.Common;
using Domain.Common.Errors;
using Domain.RepositoriesContracts;
using ErrorOr;

namespace Application.AppServices.Ventas.Queries.BuscarArticulo;

public class BuscarArticuloQueryHandler : IQueryHandler<BuscarArticuloQuery, BuscarArticuloResult>
{
    private readonly IPuntoDeVentaRepository _puntoDeVentaRepository;
    private readonly IArticuloRepository _articuloRepository;
    private readonly ISesionRepository _sesionRepository;

    public BuscarArticuloQueryHandler(
        IPuntoDeVentaRepository puntoDeVentaRepository,
        IArticuloRepository articuloRepository,
        ISesionRepository sesionRepository)
    {
        _puntoDeVentaRepository = puntoDeVentaRepository;
        _articuloRepository = articuloRepository;
        _sesionRepository = sesionRepository;
    }

    public async Task<ErrorOr<BuscarArticuloResult>> Handle(
        BuscarArticuloQuery request,
        CancellationToken cancellationToken)
    {
        var sesion = await _sesionRepository.GetByIdAsync(new Guid(request.SesionId));
        if (sesion == null)
        {
            return SesionErrors.SesionInvalidada;
        }

        var articulo = await _articuloRepository.GetByCodigoArticuloAsync(request.CodigoArticulo);
        if (articulo == null)
        {
            return ArticuloErrors.ArticuloNoEncontrado;
        }

        var stocks = articulo.GetStockEnSucursal(sesion.GetSucursal());

        return new BuscarArticuloResult(
            ArticuloDTO.ToDTO(articulo),
            stocks.ConvertAll(s => StockDTO.ToDTO(s)));
    }
}
