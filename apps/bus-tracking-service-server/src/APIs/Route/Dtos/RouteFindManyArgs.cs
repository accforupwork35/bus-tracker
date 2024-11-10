using BusTrackingService.APIs.Common;
using BusTrackingService.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace BusTrackingService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class RouteFindManyArgs : FindManyInput<Route, RouteWhereInput> { }
