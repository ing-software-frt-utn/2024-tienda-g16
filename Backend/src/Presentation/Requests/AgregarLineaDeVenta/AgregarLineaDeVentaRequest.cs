namespace Presentation.Requests.AgregarLineaDeVenta;

public sealed record AgregarLineaDeVentaRequest(
    string CodigoArticulo,
    string StockId, 
    int Cantidad);
