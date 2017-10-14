namespace Gluon.Relay.Contracts.Unused
{
    public static class RelayMessageExtensions
    {
        public static IRelayResponseMessage InitFromRequest(this IRelayResponseMessage response, IRelayRequestMessage request)
        {
            response.CorrelationId = request.CorrelationId;
            response.Request = request;
            return response;
        }
    }
}
